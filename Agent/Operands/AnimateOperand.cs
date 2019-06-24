using UnityEngine;
using UnityEngine.Assertions;

namespace SandboxAI {
    public class AnimateOperand : IAgentOperand {
        string _boolParam;
        float _duration;
        float _endTime;

        public AnimateOperand(string boolParam, float duration) {
            Assert.IsTrue(duration > 0.1f);

            _boolParam = boolParam;
            _duration = duration;
        }

        public void Start(HTNAgent agent) {
            agent.navigation.Disable();
            //agent.animator?.SetBool(_boolParam, true);
            _endTime = Time.time + _duration;
        }

        public AgentOperandUpdateResult Update(HTNAgent agent) {
            if (Time.time >= _endTime) {
                //agent.animator?.SetBool(_boolParam, false);
                return AgentOperandUpdateResult.Success;
            }

            return AgentOperandUpdateResult.Pending;
        }
    }
}