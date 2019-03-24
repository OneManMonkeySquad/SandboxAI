using SandboxAI.HTN;
using UnityEngine;

public class TargetFood : TaskWithOptions<RobotState, GameObject> {
    public override bool Execute(RobotState state) {
        state.target = GetBest(state, state.robot.foodInProximity);
        return state.target != null;
    }
}
