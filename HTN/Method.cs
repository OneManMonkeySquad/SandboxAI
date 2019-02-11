using XNode;

namespace SandboxAI.HTN {
    [CreateNodeMenu("Method")]
    public class Method : Node {
        [Output] public Method method;
        [Input(ShowBackingValue.Never)] public TaskBase[] tasks;
        [Input(ShowBackingValue.Never)] public ContextualScorerBase[] ctxScorers;

        public float Score(IState state) {
            var score = 1f;
            foreach (var scorer in ctxScorers) {
                score *= scorer.Score(state);
            }
            return score;
        }

        public override object GetValue(NodePort port) {
            method = this;
            tasks = GetInputValues<TaskBase>("tasks");
            ctxScorers = GetInputValues<ContextualScorerBase>("ctxScorers");
            return this;
        }
    }
}