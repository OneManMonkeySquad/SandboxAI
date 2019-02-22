using UnityEngine;
using UnityEngine.Assertions;

namespace SandboxAI {
    public class WaitOperand : IAgentOperand {
        float _duration;
        float _endTime;

        public WaitOperand(float duration) {
            Assert.IsTrue(duration > 0.1f);

            _duration = duration;
        }

        public void Start(Agent agent) {
            _endTime = Time.time + _duration;
        }

        public AgentOperandUpdateResult Update(Agent agent) {
            if (Time.time >= _endTime)
                return AgentOperandUpdateResult.Success;

            return AgentOperandUpdateResult.Pending;
        }
    }
}