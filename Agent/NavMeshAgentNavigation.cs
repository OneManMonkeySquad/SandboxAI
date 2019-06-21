using UnityEngine;
using UnityEngine.AI;

namespace SandboxAI {
    [AddComponentMenu("AI/Sandbox/Navigation/NavMesh")]
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshAgentNavigation : MonoBehaviour, IAgentNavigation {
        public Vector3 velocity {
            get { return _agent.velocity; }
        }

        public bool hasArrived {
            get { return !_agent.pathPending && _agent.isOnNavMesh && _agent.remainingDistance <= _agent.stoppingDistance; }
        }

        public bool failed {
            get { return !_agent.isOnNavMesh; }
        }

        NavMeshAgent _agent;
        MoveToTransformOrPosition _moveTarget;

        public void MoveTo(MoveToTransformOrPosition target, float distance = 0) {
            _agent.enabled = true;

            if (!_agent.isOnNavMesh)
                return;

            _agent.stoppingDistance = distance;
            _agent.destination = target.GetPosition();
            _agent.isStopped = false;

            _moveTarget = target;
        }

        public void LerpTo(Vector3 position, Quaternion rotation, float time = 1) {
            transform.position = position;
            transform.rotation = rotation;
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
        }

        void Update() {
            if (!_agent.enabled || _moveTarget == null)
                return;

            var diff = _agent.destination - _moveTarget.GetPosition();
            if (diff.sqrMagnitude > 1) {
                _agent.destination = _moveTarget.GetPosition();
            }
        }
    }
}