using System.Linq;
using UnityEngine;
using XNode;

namespace SandboxAI.HTN {
    public sealed class CompoundTask : TaskBase {
        [Input(ShowBackingValue.Never)] public Method[] methods;

        public override string description => "Foo";

        public override object GetValue(NodePort port) {
            methods = GetInputValues<Method>("methods").OrderBy(m => m.position.y).ToArray();
            return base.GetValue(port);
        }
    }
}