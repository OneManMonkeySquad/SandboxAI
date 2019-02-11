using XNode;

namespace SandboxAI.HTN {
    [NodeTint(230, 230, 255)]
    [NodeWidth(270)]
    public abstract class ContextualScorerBase : Node {
        [Output] public ContextualScorerBase ctxScorer;

        public abstract float Score(IState state);

        public override object GetValue(NodePort port) {
            if (port.fieldName == "ctxScorer") return this;
            else return null;
        }
    }
}