using XNode;

namespace SandboxAI.HTN {
    [CreateNodeMenu("")]
    [NodeTint(255, 255, 160)]
    public abstract class CheckBase : Node {
        [Output] public CheckBase check;
        public bool invert;

        public abstract bool Test(IState state);

        public override object GetValue(NodePort port) {
            check = this;
            return this;
        }
    }

    public abstract class Check<StateT> : CheckBase {
        public virtual bool Test(StateT state) { return true; }

        public override bool Test(IState state) { return Test((StateT)state) ^ invert; }
    }
}