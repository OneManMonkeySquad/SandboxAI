using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace SandboxAI.HTN {
    public class TaskWithOptions<StateT, OptionT> : Task<StateT> {
        [Input(ShowBackingValue.Never)] public OptionScorerBase[] optionScorers;

        protected OptionT GetBest(IState state, IEnumerable<OptionT> options) {
            var bestScore = 0f;
            OptionT bestOption = default;
            foreach (var option in options) {
                if (optionScorers.Length == 0)
                    return option;

                var score = 1f;
                foreach (OptionScorer<OptionT> scorer in optionScorers) {
                    score *= scorer.Score(state, option);
                    if (score < bestScore)
                        break;
                }
                if (score > bestScore) {
                    bestScore = score;
                    bestOption = option;
                }
            }
            return bestOption;
        }

        public override object GetValue(NodePort port) {
            optionScorers = GetInputValues<OptionScorer<OptionT>>("optionScorers");
            return base.GetValue(port);
        }
    }
}