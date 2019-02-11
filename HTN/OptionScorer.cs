using SandboxAI.HTN;

public abstract class OptionScorer<StateT, OptionT> : OptionScorer<OptionT> {
    public abstract float Score(StateT state, OptionT option);

    public override float Score(IState state, OptionT option) {
        return Score((StateT)state, option);
    }
}
