using SandboxAI.HTN;

public class AddTargetToBackpack : Task<RobotState> {
    public override bool Execute(RobotState state) {
        if (state.target == null)
            return false;

        if (state.target.GetComponentInParent<Robot>() != null)
            return false; // Already in a backpack
        
        return state.robot.AddItemToBackpack(state.target);
    }
}
