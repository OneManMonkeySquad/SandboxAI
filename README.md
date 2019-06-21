# SandboxAI
**Not even close to production ready. All feedback and patches are welcome.**

Minimal HTN AI implementation for [Unity3d](https://unity3d.com). It's perfect for a Skyrim-like sandbox AI. It doesn't follow any existing implementation. Instead it's
my own interpretation of the concept. 

![Example Graph](Docs/ExampleGraph.jpg)
![Example Scorer](Docs/ExampleScorer.jpg)
![Example Graph 2](Docs/ExampleGraph2.jpg)
![Example Scorer](Docs/ServingSuggestion.png)

Features:
- HTN/utility AI hybrid: priorize tasks and select inputs by utility functions
- No additional dependencies
- Minimal api surface to learn while still having the possibility of creating a powerful game AI

Missing:
- Debugger
- Interruptions like getting attacked
- Partial plans: stop planning when the estimated execution time of the plan reaches some threshold
- No performance optimizations yet; The algorithm itself can be very fast and efficient


## Getting started
Use git to checkout the project or [unzip a release](https://github.com/SirPolly/SandboxAI/releases) into your Unit project Assets folder. Open the *Example/Example.scene*.

### Setup your GameObject
Derive from *SandboxAI.HTNAgent*. Create your state class deriving from *SandboxAI.IState*. 
Add an *IAgentNavigation* derived component (NavMeshAgentNavigation, SimplePositionAgentNavigation) to your AI GameObject. Add the Robot Component to your AI GameObject (see below).

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

Now, create your own logic classes and create your own HTNGraph by right-clicking in the project explorer, then clicking Create/SandboxAI/HTNGraph.


## Node/class types

### Predefined/sealed
**Methods** may have checks (yellow in the HTNGraph) for the AIs state (IState). The order in which they are checked depends on the contexual scorers (in blue). If one is found, it's tasks are validated with state Checks too. Methods with Checks and Compound Tasks allow you to disable a whole subpart of the graph (see 1).

**Compound Task** combines server Methods as one Task.

### Used defined (aka derive from those and implement your logic)
**Tasks/Task< StateT >** are actual actions the AI is supposed to do. You derive from them. They are grouped by Methods into sequences of actions (see 2). Each Task may have preconditions and effects on the AIs state (IState). *Task.Check(IState state)* and *Task.Apply(IState state)* are used during planning and execution to express these. 

**TaskWithOptions< StateT, OptionT >** are Tasks with support for option scoring. Use *TaskWithOptions.GetBest(IState state, IEnumerable< OptionT > options)* to get the best option. Use these to select attack targets, positions or cover to go to, which items to buy, ... .

**Option Scorers/OptionScorer< OptionT >** are used by TaskWithOptions. The task first builds a list of possible options and then asks the Option Socorers for a score between 0-1. *OptionScorer.Score(IState state, OptionT option)* is used for this.

**Checks** allow you to define preconditions for Methods. Tasks have preconditions/checks too, but Checks allow you to define additional restrictions in a modular and performant way.

**Context (Ctx) Scorers** are used to rank Methods. Methods are first sorted and then checked one after another until a valid one is found. Use these to define AI priorities, like using a medpack on low health or melee attack if the player is close range.


## The planner
![Planner](Docs/Planner.png)

Every time the current plan is completed or invalidated the planner is asked to create a new plan. The **Main Node**s main input tells it where to start planning. It recursivly searches through Compound Tasks methods for a valid method. The final aim of the planner is to reduce the whole graph to a list of tasks (in dark green). If you ignore Compound Tasks the final result of the planner are just the tasks of one valid Method. Compound tasks though allow you to nest more and more Methods, Compound Tasks and Tasks. As usual in such a graph, nodes are allowed to be connected to multiple other nodes which allows you to reuse parts of the graph. 


## HTNAgent
The HTNAgent implements the update loop for one AI unit. It has a reference to a HTNGraph


## SmartObjects
todo


## Based upon
Introduction: https://www.youtube.com/watch?v=kXm467TFTcY

[Game AI Pro: Exploring HTN Planners through Example](http://www.gameaipro.com/GameAIPro/GameAIPro_Chapter12_Exploring_HTN_Planners_through_Example.pdf)

### Prerequisites
- Recent Unity version (2018)
- [xNode](https://github.com/Siccity/xNode)

## License
This project is licensed under the MIT License. You cannot sell this software on the Unity Asset Store or any other platform that sells software tools for developers. You may use this software in a commercial game.
