using SandboxAI;
using SandboxAI.HTN;

public class MoveToTarget : Task<RobotState> {
    public float targetDistance;

    public override bool Execute(RobotState state) {
        if (state.target == null)
            return false;
        
        state.agent.QueueOperand(new MoveToOperand(new MoveToTransformOrPosition(state.target.transform), targetDistance));
        return true;
    }
}
