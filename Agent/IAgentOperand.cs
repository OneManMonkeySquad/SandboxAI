using System;
using UnityEngine;

namespace SandboxAI {
    public enum AgentOperandUpdateResult {
        Pending,
        Success,
        Failed
    }

    public interface IAgentOperand {
        void Start(HTNAgent agent);
        AgentOperandUpdateResult Update(HTNAgent agent);
    }
    
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

    public class LerpToPositionOperand : IAgentOperand {
        Vector3 _position;
        Quaternion _rotation;

        public LerpToPositionOperand(Vector3 position, Quaternion rotation) {
            _position = position;
            _rotation = rotation;
        }

        public void Start(HTNAgent agent) {
            agent.navigation.Disable();
        }

        public AgentOperandUpdateResult Update(HTNAgent agent) {
            var a = Time.deltaTime * 4;
            agent.transform.position = Vector3.Lerp(agent.transform.position, _position, a);
            agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, _rotation, a);

            if ((agent.transform.position - _position).sqrMagnitude > 0.01f)
                return AgentOperandUpdateResult.Pending;

            if (Quaternion.Angle(agent.transform.rotation, _rotation) > 5)
                return AgentOperandUpdateResult.Pending;

            return AgentOperandUpdateResult.Success;
        }
    }
}