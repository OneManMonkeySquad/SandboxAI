namespace SandboxAI.HTN {
    [CreateNodeMenu("Constant")]
    public class ConstantContextualScorer : ContextualScorerBase {
        public float constantScore;
        
        public override float Score(IState state) {
            return constantScore;
        }
    }
}