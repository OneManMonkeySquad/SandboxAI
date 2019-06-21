using UnityEngine.Serialization;
using XNode;

namespace SandboxAI.HTN {
    [CreateNodeMenu("")]
    public class MainNode : Node {
        [Input(ShowBackingValue.Never)] public TaskBase task;

        public override object GetValue(NodePort port) {
            task = GetInputValue<TaskBase>("task");
            return this;
        }
    }
}