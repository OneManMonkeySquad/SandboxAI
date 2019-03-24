using SandboxAI.HTN;
using UnityEngine;

public class TargetRobot : TaskWithOptions<RobotState, GameObject> {
    public override bool Execute(RobotState state) {
        state.target = GetBest(state, state.robot.robotsInProximity);
        return state.target != null;
    }
}
