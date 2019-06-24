using UnityEngine;
using UnityEngine.AI;

namespace SandboxAI {
    [AddComponentMenu("AI/Sandbox/Navigation/NavMesh")]
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshAgentNavigation : MonoBehaviour, IAgentNavigation {
        public Vector3 velocity {
            get { return _agent.velocity; }
            set { _agent.velocity = value; }
        }

        public Vector3 desiredVelocity {
            get { return _agent.desiredVelocity; }
        }

        public bool hasArrived {
            get { return !_agent.pathPending && _agent.isOnNavMesh && _agent.remainingDistance <= _agent.stoppingDistance; }
        }

        public bool failed {
            get { return _moveToFailed || !_agent.isOnNavMesh; }
        }

        NavMeshAgent _agent;
        MoveToTransformOrPosition _moveTarget;
        bool _moveToFailed;
        float _targetDistance;

        public void MoveTo(MoveToTransformOrPosition target, float distance = 0) {
            _agent.enabled = true;
            _moveToFailed = false;

            if (!_agent.isOnNavMesh)
                return;

            var targetPosition = target.GetWorldPosition();
            if (!FindTargetPosition(targetPosition, distance, out targetPosition)) {
                _agent.enabled = false;
                _moveToFailed = true;
                return;
            }

            _targetDistance = distance;
            _agent.destination = targetPosition;
            _agent.isStopped = false;

            _moveTarget = target;
        }

        bool FindTargetPosition(Vector3 worldPosition, float distance, out Vector3 targetPosition) {
            targetPosition = worldPosition;
            if (distance > 0) {
                var diff = transform.position - worldPosition;
                diff.y = 0;
                var offset = diff.normalized * distance;
                targetPosition += offset;

//                 if (!NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, distance, _agent.areaMask))
//                     return false;
// 
//                 targetPosition = hit.position;
                Debug.DrawLine(targetPosition, targetPosition + Vector3.up * 10, Color.red, 10);
            }
            return true;
        }

        public void LerpTo(Vector3 position, Quaternion rotation, float time = 1) {
            transform.position = position;
            transform.rotation = rotation;
            _moveToFailed = false;
            _agent.enabled = false;
        }

        public void StopMoving() {
            if (_agent.isOnNavMesh) {
                _agent.isStopped = true;
                _agent.ResetPath();
            }
        }

        public void Disable() {
            _agent.enabled = false;
        }

        void Awake() {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updatePosition = false;
            _agent.updateRotation = false;
        }

        void Update() {
            if (!_agent.enabled || !_agent.isOnNavMesh || _moveTarget == null)
                return;

            var targetPosition = _moveTarget.GetWorldPosition();
            var diff = _agent.destination - targetPosition;
            if (diff.sqrMagnitude > _targetDistance * _targetDistance + 1) {
                if (!FindTargetPosition(targetPosition, _targetDistance, out targetPosition)) {
                    _agent.enabled = false;
                    _moveToFailed = true;
                    return;
                }

                _agent.destination = targetPosition;
            }
        }
    }
}