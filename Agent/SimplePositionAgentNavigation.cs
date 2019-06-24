using UnityEngine;
using UnityEngine.AI;

namespace SandboxAI {
    [AddComponentMenu("AI/Sandbox/Navigation/SimplePosition")]
    public class SimplePositionAgentNavigation : MonoBehaviour, IAgentNavigation {
        public Vector3 velocity {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Vector3 desiredVelocity {
            get { return _velocity; }
        }

        public bool hasArrived {
            get { return _hasArrived; }
        }

        public bool failed {
            get { return false; }
        }

        public float speed = 1;

        MoveToTransformOrPosition _target;
        float _targetDistance;
        Vector3 _velocity;
        bool _hasArrived;

        public void MoveTo(MoveToTransformOrPosition target, float distance = 0) {
            _target = target;
            _targetDistance = distance;
            _hasArrived = false;
        }

        public void LerpTo(Vector3 position, Quaternion rotation, float time = 1) {
            MoveTo(new MoveToTransformOrPosition(position));
        }

        public void StopMoving() {
            _target = null;
            _velocity = Vector3.zero;
            _hasArrived = false;
        }

        public void Disable() {
            _target = null;
            _velocity = Vector3.zero;
            _hasArrived = false;
        }

        void Update() {
            if (_target == null || _hasArrived)
                return;

            var diff = _target.GetWorldPosition() - transform.position;

            var mag = diff.magnitude;
            if (mag > 0.01f) {
                diff /= mag;
                diff *= Mathf.Min(speed * Time.deltaTime, mag);
            }
            else {
                diff = Vector3.zero;
                _hasArrived = true;
            }
            
            _velocity = diff;
            transform.position += velocity;

            transform.rotation = Quaternion.LookRotation(diff);
        }
    }
}