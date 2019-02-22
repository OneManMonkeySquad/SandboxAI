using UnityEngine;
using XNode;

namespace SandboxAI.HTN {
    [NodeTint(225, 255, 150)]
    public class CompoundTask : TaskBase {
        [Input(ShowBackingValue.Never)] public Method[] methods;

        public override object GetValue(NodePort port) {
            methods = GetInputValues<Method>("methods");
            return base.GetValue(port);
        }
    }
}