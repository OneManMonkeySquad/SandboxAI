using UnityEngine;

namespace SandboxAI {
    public class MoveToOperand : IAgentOperand {
        Transform _target;
        float _targetDistance;

        public MoveToOperand(Transform target, float targetDistance) {
            _target = target;
            _targetDistance = targetDistance;
        }

        public void Start(Agent agent) {
            agent.navigation.MoveTo(_target, _targetDistance);
            agent.animator.SetBool("Moving", true);
        }

        public AgentOperandUpdateResult Update(Agent agent) {
            if (agent.navigation.failed || _target == null) {
                agent.navigation.StopMoving();
                agent.animator.SetBool("Moving", false);
                return AgentOperandUpdateResult.Failed;
            }

            if (agent.navigation.hasArrived) {
                agent.navigation.StopMoving();
                agent.animator.SetBool("Moving", false);
                return AgentOperandUpdateResult.Success;
            }

            return AgentOperandUpdateResult.Pending;
        }
    }
}