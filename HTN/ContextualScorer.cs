namespace SandboxAI.HTN {
    public abstract class ContextualScorer<StateT> : ContextualScorerBase {
        public abstract float Score(StateT state);

        public override float Score(IState state) {
            return Score((StateT)state);
        }
    }
}