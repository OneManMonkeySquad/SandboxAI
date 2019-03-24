# SandboxAI
HTN AI implementation for [Unity3d](https://unity3d.com). It's perfect for a Skyrim-like sandbox AI. It doesn't follow any existing implementation. Instead it's
my own interpretation of the concept. 

![Example Graph](Docs/ExampleGraph.jpg)
![Example Scorer](Docs/ExampleScorer.jpg)

Features:
- HTN/utility AI hybrid: priorize tasks and select inputs by utility functions
- One included dependency: [xNode](https://github.com/Siccity/xNode)

Missing:
- Debugger
- Example project
- Interruptions like getting attacked
- Partial plans: stop planning when the estimated execution time of the plan reaches some threshold

## Getting started
Use git to checkout the project or [unzip a release](https://github.com/SirPolly/SandboxAI/releases) into your Unit project Assets folder. Open the *Example/Example.scene*.

# Setup your GameObject
Derive from *SandboxAI.HTNAgent*. Create your state class deriving from *SandboxAI.IState*. 
Add an *IAgentNavigation* derived component (NavMeshAgentNavigation, SimplePositionAgentNavigation).

```CSharp
using SandboxAI.HTN;

[Serializable]
public class RobotState : IState {
	public Robot robot;

	public float hunger;
}

public class Robot : HTNAgent {
	RobotState _state = new RobotState();

	void Start() {
		_state.robot = this;
    }

	void Update() {
		UpdateAgent(_state);
	}
}
```

Look at the sample to learn how to create ContextualScorers, OptionScorers and Tasks. The *Robot_HTNGraph* contains the actual decision making.

Now, create your own logic classes and create your own HTNGraph by clicking right in the project explorer, then clicking Create/SandboxAI/HTNGraph.

## Based upon
Introduction: https://www.youtube.com/watch?v=kXm467TFTcY

[Game AI Pro: Exploring HTN Planners through Example](http://www.gameaipro.com/GameAIPro/GameAIPro_Chapter12_Exploring_HTN_Planners_through_Example.pdf)

### Prerequisites
- Recent Unity version (2018)
- [xNode](https://github.com/Siccity/xNode)
