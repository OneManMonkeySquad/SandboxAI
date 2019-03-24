using SandboxAI.HTN;

public class HasItemInBackpack : Check<RobotState> {
    public override bool Test(RobotState state) {
        return state.robot.backpackItem != null;
    }
}
