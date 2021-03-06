﻿namespace SandboxAI {
    public class MoveToOperand : IAgentOperand {
        MoveToTransformOrPosition _target;
        float _targetDistance;

        public MoveToOperand(MoveToTransformOrPosition target, float targetDistance = 0) {
            _target = target;
            _targetDistance = targetDistance;
        }

        public void Start(HTNAgent agent) {
            agent.navigation.MoveTo(_target, _targetDistance);
        }

        public AgentOperandUpdateResult Update(HTNAgent agent) {
            if (agent.navigation.failed || _target == null) {
                agent.navigation.StopMoving();
                return AgentOperandUpdateResult.Failed;
            }

            if (agent.navigation.hasArrived) {
                agent.navigation.StopMoving();
                return AgentOperandUpdateResult.Success;
            }

            return AgentOperandUpdateResult.Pending;
        }
    }
}