using UnityEngine;
using XNode;

namespace SandboxAI.HTN {
    [NodeTint(225, 255, 150)]
    public abstract class CompoundTaskBase : TaskBase {
        [Input(ShowBackingValue.Never)] public Method[] methods;

        public override object GetValue(NodePort port) {
            methods = GetInputValues<Method>("methods");
            return base.GetValue(port);
        }
    }

    public abstract class CompoundTask<StateT> : CompoundTaskBase {
        public virtual bool Check(StateT state) { return true; }
        public virtual void Apply(StateT state) { }
        public virtual bool Execute(StateT state) { return true; }
        public virtual bool Terminate(StateT state, bool success) { return true; }

        public override bool Check(IState state) { return Check((StateT)state); }
        public override void Apply(IState state) { Apply((StateT)state); }
        public override bool Execute(IState state) { return Execute((StateT)state); }
        public override bool Terminate(IState state, bool success) { return Terminate((StateT)state, success); }
    }
}