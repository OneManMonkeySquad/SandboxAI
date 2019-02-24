using SandboxAI.HTN;
using System.Collections.Generic;
using UnityEngine;

namespace SandboxAI {
    [AddComponentMenu("AI/Sandbox/Agent")]
    public class Agent : MonoBehaviour {
        public IAgentNavigation navigation {
            get { return GetComponent<IAgentNavigation>(); }
        }

        public Animator animator {
            get { return GetComponentInChildren<Animator>(); }
        }

        public Graph graph;

        AI ai;
        Queue<IAgentOperand> ops = new Queue<IAgentOperand>();
        IAgentOperand _currentOp;

        public void QueueOperand(IAgentOperand operand) {
            ops.Enqueue(operand);
        }

        public void StartAgent() {
            ai = new AI();
        }

        public void UpdateAgent(IState state) {
            ai.Update(state, graph.mainTask);
            
            switch (UpdateOperands()) {
                case AgentOperandUpdateResult.Pending:
                    break;

                case AgentOperandUpdateResult.Success:
                    ai.CompleteCurrentTask(state);
                    break;

                case AgentOperandUpdateResult.Failed:
                    ai.AbortCurrentTask(state);
                    break;
            }
        }

        AgentOperandUpdateResult UpdateOperands() {
            if (_currentOp == null) {
                if (ops.Count == 0)
                    return AgentOperandUpdateResult.Success;

                _currentOp = ops.Dequeue();
                _currentOp.Start(this);
            }

            var result = _currentOp.Update(this);
            if (result == AgentOperandUpdateResult.Failed) {
                Debug.Log("OP failed " + _currentOp);
                _currentOp = null;
                ops.Clear();
                return AgentOperandUpdateResult.Failed;
            }

            if (result == AgentOperandUpdateResult.Success) {
                _currentOp = null;
            }

            return AgentOperandUpdateResult.Pending;
        }
    }
}