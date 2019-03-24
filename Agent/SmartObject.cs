using SandboxAI.HTN;
using UnityEngine;

namespace SandboxAI {
    [CreateAssetMenu]
    public class SmartObjectFlag : ScriptableObject {
    }

    public abstract class SmartObjectBase : MonoBehaviour {
        public SmartObjectFlag flags;
        public Transform moveToTarget;

        public virtual void Apply(IState state) { }
        public virtual bool Execute(IState state) { return true; }
        public virtual bool Terminate(IState state) { return true; }

        public virtual Transform GetMoveToTarget() {
            return moveToTarget;
        }
    }

    public abstract class SmartObject<StateT> : SmartObjectBase {
        public virtual void Apply(StateT state) { }
        public virtual bool Execute(StateT state) { return true; }
        public virtual bool Terminate(StateT state) { return true; }

        public override void Apply(IState state) { Apply((StateT)state); }
        public override bool Execute(IState state) { return Execute((StateT)state); }
        public override bool Terminate(IState state) { return Terminate((StateT)state); }
    }
}