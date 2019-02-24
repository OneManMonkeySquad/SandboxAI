﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace SandboxAI.HTN {
    public enum CompleteCurrentTaskResult {
        ContinuePlan,
        NoPlan,
        PlanFinished,
        PlanFailed
    }

    public class AI {
        struct PlannerState {
            public Stack<TaskBase> tasksToProcess;
            public List<TaskBase> finalPlan;
            public IState workingWS;
            public int nextMethodIdx;
            public CompoundTask nextMethodPtr;
        }

        Stack<TaskBase> _currentPlan;
        TaskBase _currentTask;
        float _minNextPlanTime;

        public void Update(IState state, TaskBase rootTask) {
            if (_currentPlan == null) {
                if (Time.time >= _minNextPlanTime) {
                    _minNextPlanTime = Time.time + 0.25f;

                    if (Plan(state, rootTask, out _currentPlan)) {
                        Debug.Log("Plan: " + _currentPlan.Aggregate("", (acc, task) => acc + task.name + ","));

                        CompleteCurrentTask(state);
                    }
                    else {
                        Debug.Log("No plan found");
                    }
                }
            }
        }

        public void AbortCurrentTask(IState state) {
            if (_currentTask != null) {
                _currentTask.Terminate(state, false);
            }
            _currentPlan = null;
            _currentTask = null;
        }

        public CompleteCurrentTaskResult CompleteCurrentTask(IState state) {
            if (_currentTask != null) {
                if (!_currentTask.Terminate(state, true)) {
                    _currentPlan = null;
                    _currentTask = null;
                    return CompleteCurrentTaskResult.PlanFailed;
                }

                _currentTask.Apply(state);
                _currentTask = null;
            }

            if (_currentPlan == null)
                return CompleteCurrentTaskResult.NoPlan;

            if (_currentTask == null && _currentPlan.Count == 0) {
                _currentPlan = null;
                return CompleteCurrentTaskResult.PlanFinished;
            }

            _currentTask = _currentPlan.Pop();

            if (!_currentTask.Check(state)) {
                _currentPlan = null;
                _currentTask = null;
                Debug.LogError("Task check failed");
                return CompleteCurrentTaskResult.PlanFailed;
            }

            if (!_currentTask.Execute(state)) {
                _currentPlan = null;
                _currentTask = null;
                return CompleteCurrentTaskResult.PlanFailed;
            }

            return CompleteCurrentTaskResult.ContinuePlan;
        }
        
        static bool Plan(IState state, TaskBase rootTask, out Stack<TaskBase> finalPlan) {
            Assert.IsNotNull(rootTask);

            var plannerState = new PlannerState() {
                tasksToProcess = new Stack<TaskBase>(),
                finalPlan = new List<TaskBase>(),
                workingWS = state.Clone()
            };
            plannerState.tasksToProcess.Push(rootTask);
            plannerState.nextMethodIdx = 0;

            var decompHistory = new Stack<PlannerState>();
            while (plannerState.tasksToProcess.Count > 0) {
                var currentTask = plannerState.tasksToProcess.Pop();
                if (currentTask is CompoundTask CurrentTaskCompound) {
                    Debug.Log(Padd(decompHistory) + "CT " + currentTask.name);

                    if (plannerState.nextMethodPtr != CurrentTaskCompound) {
                        plannerState.nextMethodPtr = CurrentTaskCompound;
                        plannerState.nextMethodIdx = 0;
                    }

                    var satisfiedMethod = FindSatisfiedMethod(CurrentTaskCompound, plannerState.workingWS, ref plannerState.nextMethodIdx);
                    if (satisfiedMethod != null) {
                        RecordDecompositionOfTask(plannerState, CurrentTaskCompound, decompHistory);

                        for (int i = satisfiedMethod.tasks.Length - 1; i >= 0; --i) {
                            var t = satisfiedMethod.tasks[i];
                            plannerState.tasksToProcess.Push(t);
                        }
                    }
                    else {
                        RestoreToLastDecomposedTask(ref plannerState, decompHistory, CurrentTaskCompound.name + " no satisfied method");
                    }
                }
                else { //Primitive Task
                    Debug.Log(Padd(decompHistory) + "PT " + currentTask.name);

                    if (currentTask.Check(plannerState.workingWS)) {
                        currentTask.Apply(plannerState.workingWS);
                        plannerState.finalPlan.Add(currentTask);
                    }
                    else {
                        RestoreToLastDecomposedTask(ref plannerState, decompHistory, currentTask + " check failed");
                    }
                }
            }

            plannerState.finalPlan.Reverse();
            finalPlan = new Stack<TaskBase>(plannerState.finalPlan);
            //Debug.Log("=== DONE ===");
            return true;
        }

        static Method FindSatisfiedMethod(CompoundTask CurrentTaskCompound, IState state, ref int nextMethodIdx) {
            var methodState = state.Clone();
            
            var methods = CurrentTaskCompound.methods.OrderByDescending(m => m.Score(state)).ToList();
            for (; nextMethodIdx < methods.Count; ++nextMethodIdx) {
                var method = methods[nextMethodIdx];

                if (!method.Check(methodState))
                    continue;

                var methodValid = true;
                foreach (var task in method.tasks) {
                    if (!task.Check(methodState)) {
                        Debug.Log(CurrentTaskCompound + " not satisfied because " + task + " check failed");

                        methodValid = false;
                        break;
                    }

                    task.Apply(methodState);
                }

                if (methodValid) {
                    ++nextMethodIdx;
                    return method;
                }
            }
            nextMethodIdx = 0;
            return null;
        }

        static void RecordDecompositionOfTask(PlannerState plannerState, CompoundTask CurrentTaskCompound, Stack<PlannerState> DecompHistory) {
            var copy = new PlannerState {
                finalPlan = new List<TaskBase>(plannerState.finalPlan),
                tasksToProcess = new Stack<TaskBase>(plannerState.tasksToProcess),
                workingWS = plannerState.workingWS.Clone(),
                nextMethodIdx = plannerState.nextMethodIdx,
                nextMethodPtr = plannerState.nextMethodPtr
            };
            copy.tasksToProcess.Push(CurrentTaskCompound);
            DecompHistory.Push(copy);
        }

        static void RestoreToLastDecomposedTask(ref PlannerState plannerState, Stack<PlannerState> DecompHistory, string reason) {
            //Debug.Log(Padd(DecompHistory) + "[RestoreToLastDecomposedTask:" + reason + "]");

            if (DecompHistory.Count == 0) {
                plannerState.tasksToProcess.Clear();
                return;
            }

            plannerState = DecompHistory.Pop();
        }

        static string Padd(Stack<PlannerState> DecompHistory) {
            return "".PadRight(DecompHistory.Count * 4);
        }
    }
}