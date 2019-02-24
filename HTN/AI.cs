using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SandboxAI.HTN {
    public enum CompleteCurrentTaskResult {
        ContinuePlan,
        NoPlan,
        PlanSuccess,
        PlanFailed
    }

    public class AI {
        Stack<TaskBase> _currentPlan;
        TaskBase _currentTask;
        float _minNextPlanTime;

        public void Update(IState state, TaskBase rootTask) {
            if (_currentPlan == null) {
                if (Time.time >= _minNextPlanTime) {
                    _minNextPlanTime = Time.time + 0.25f;

                    if (Planner.Plan(state, rootTask, out _currentPlan)) {
                        Debug.Log("Plan: " + _currentPlan.Aggregate("", (acc, task) => acc + task.name + ","));

                        CompleteCurrentTask(state);
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
                return CompleteCurrentTaskResult.PlanSuccess;
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
    }
}