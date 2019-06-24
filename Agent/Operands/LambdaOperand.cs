using System;

namespace SandboxAI {
    public class LambdaOperand : IAgentOperand {
        Func<AgentOperandUpdateResult> _lambda;

        public LambdaOperand(Func<AgentOperandUpdateResult> lambda) {
            _lambda = lambda;
        }

        public void Start(HTNAgent agent) {
        }

        public AgentOperandUpdateResult Update(HTNAgent agent) {
            return _lambda();
        }
    }
}