using SandboxAI.HTN;
using UnityEngine;

public class ShootAtTarget : Task<RobotState> {
    public GameObject shotPrefab;

    public override bool Execute(RobotState state) {
        if (state.target == null || Time.time < state.nextShotTime)
            return false;

        var diff = state.target.transform.position - state.robot.transform.position;
        var rotation = Quaternion.LookRotation(diff);

        Instantiate(shotPrefab, state.robot.transform.position + diff.normalized * 0.8f, rotation);

        state.nextShotTime = Time.time + 1;

        return true;
    }

    public override bool Check(RobotState state) {
        return Time.time >= state.nextShotTime;
    }
}
