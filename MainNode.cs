using SandboxAI.HTN;
using XNode;

public class MainNode : Node {
    [Input(ShowBackingValue.Never)] public TaskBase main;

    public override object GetValue(NodePort port) {
        main = GetInputValue<TaskBase>("main");
        return this;
    }
}
