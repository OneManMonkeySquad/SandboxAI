using System.Linq;
using UnityEngine;
using XNode;

namespace SandboxAI.HTN {
    [NodeTint(200, 255, 150)]
    public class CompoundTask : TaskBase {
        [Input(ShowBackingValue.Never)] public Method[] methods;

        public override object GetValue(NodePort port) {
            methods = GetInputValues<Method>("methods").OrderBy(m => m.position.y).ToArray();
            return base.GetValue(port);
        }
    }
}