using XNode;

namespace SandboxAI.HTN {
    [NodeWidth(270)]
    [NodeTint(140, 255, 140)]
    public abstract class OptionScorerBase : Node {
        [Output] public OptionScorerBase optionScorer;

        public override object GetValue(NodePort port) {
            return this;
        }
    }
    
    public abstract class OptionScorer<OptionT> : OptionScorerBase {
        public abstract float Score(IState state, OptionT option);
    }
}