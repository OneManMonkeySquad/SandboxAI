using SandboxAI.HTN;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SandboxAI {
    public class HTNAgent {
        public HTNGraph graph {
            get;
            internal set;
        }
        public IAgentNavigation navigation;

        AI _ai = new AI();
        Queue<IAgentOperand> _ops = new Queue<IAgentOperand>();
        IAgentOperand _currentOp;

        public HTNAgent(IAgentNavigation navigation, HTNGraph graph) {
            if (graph == null)
                throw new ArgumentNullException("graph");

            this.navigation = navigation;
            this.graph = graph;
        }

        public void QueueOperand(IAgentOperand operand) {
            _ops.Enqueue(operand);
        }
        
        public void UpdateAgent(IState state) {
            _ai.Update(state, graph.mainTask);
            
            switch (UpdateOperands()) {
                case AgentOperandUpdateResult.Pending:
                    break;

                case AgentOperandUpdateResult.Success:
                    _ai.CompleteCurrentTask(state);
                    break;

                case AgentOperandUpdateResult.Failed:
                    _ai.AbortCurrentTask(state);
                    break;
            }
        }

        AgentOperandUpdateResult UpdateOperands() {
            if (_currentOp == null) {
                if (_ops.Count == 0)
                    return AgentOperandUpdateResult.Success;

                _currentOp = _ops.Dequeue();
                _currentOp.Start(this);
            }

            var result = _currentOp.Update(this);
            if (result == AgentOperandUpdateResult.Failed) {
                Debug.Log("OP failed " + _currentOp);
                _currentOp = null;
                _ops.Clear();
                return AgentOperandUpdateResult.Failed;
            }

            if (result == AgentOperandUpdateResult.Success) {
                _currentOp = null;
            }

            return AgentOperandUpdateResult.Pending;
        }
    }
}