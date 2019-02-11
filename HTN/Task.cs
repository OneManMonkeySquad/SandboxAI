using XNode;

namespace SandboxAI.HTN {
    [NodeTint(180, 255, 180)]
    [NodeWidth(270)]
    public abstract class TaskBase : Node {
        [Output] public TaskBase task;

        public virtual string description => "";

        public virtual bool Check(IState state) { return true; }
        public virtual void Apply(IState state) { }
        public virtual bool Execute(IState state) { return true; }
        public virtual bool Terminate(IState state, bool success) { return true; }
        
        public override object GetValue(NodePort port) {
            task = this;
            return this;
        }
    }

    public abstract class Task<StateT> : TaskBase {
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