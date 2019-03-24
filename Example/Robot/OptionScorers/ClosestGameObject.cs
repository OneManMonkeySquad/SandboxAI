using UnityEngine;

public class ClosestGameObject : OptionScorer<RobotState, GameObject> {
    public override float Score(RobotState state, GameObject option) {
        var dist = state.robot.transform.position - option.transform.position;
        return 1 - Mathf.Min(Mathf.CeilToInt(dist.sqrMagnitude * 0.5f) / 100f, 1f);
    }
}
