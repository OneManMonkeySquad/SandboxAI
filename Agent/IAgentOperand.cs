namespace SandboxAI {
    public enum AgentOperandUpdateResult {
        Pending,
        Success,
        Failed
    }

    public interface IAgentOperand {
        void Start(HTNAgent agent);
        AgentOperandUpdateResult Update(HTNAgent agent);
    }
}