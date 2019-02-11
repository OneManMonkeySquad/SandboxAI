namespace SandboxAI.HTN {
    [CreateNodeMenu("CtxScorers/Constant")]
    public class ConstantContextualScorer : ContextualScorerBase {
        public float constantScore;
        
        public override float Score(IState state) {
            return constantScore;
        }
    }
}