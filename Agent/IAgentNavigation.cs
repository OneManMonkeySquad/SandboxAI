using UnityEngine;

namespace SandboxAI {
    public interface IAgentNavigation {
        Vector3 velocity {
            get;
            set;
        }

        Vector3 desiredVelocity {
            get;
        }

        bool hasArrived {
            get;
        }

        bool failed {
            get;
        }

        void MoveTo(MoveToTransformOrPosition target, float distance = 0);
        void LerpTo(Vector3 position, Quaternion rotation, float time = 1);
        void StopMoving();
        void Disable();
    }
}