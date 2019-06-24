using UnityEngine;

public class MoveToTransformOrPosition {
    Transform _transform;
    Vector3 _position;

    public MoveToTransformOrPosition(Transform transform) {
        _transform = transform;
    }

    public MoveToTransformOrPosition(Vector3 position) {
        _position = position;
    }

    public Vector3 GetWorldPosition() {
        return _transform != null ? _transform.position : _position;
    }
}
