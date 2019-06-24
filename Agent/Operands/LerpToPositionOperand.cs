using System;
using UnityEngine;

namespace SandboxAI {
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
            agent.navigation.LerpTo(_position, _rotation);
            return agent.navigation.hasArrived ? AgentOperandUpdateResult.Success : AgentOperandUpdateResult.Pending;
        }
    }
}