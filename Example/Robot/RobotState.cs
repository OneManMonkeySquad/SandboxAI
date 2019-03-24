using SandboxAI;
using SandboxAI.HTN;
using System;
using UnityEngine;

[Serializable]
public class RobotState : IState {
    public RobotState(HTNAgent agent, Robot robot) {
        this.agent = agent;
        this.robot = robot;
    }

    public HTNAgent agent {
        get;
        internal set;
    }

    public Robot robot {
        get;
        internal set;
    }

    public GameObject target;

    public IState Clone() {
        return new RobotState(agent, robot) { };
    }
}