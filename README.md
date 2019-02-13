# SandboxAI
Bare-bones HTN ai implementation for [Unity3d](https://unity3d.com).
Implements a Hierarchical Task Network planner which creates plans from designer given sequences of tasks with preconditions and effects.
Sequences can be prioritized by utility functions.

![Example Graph](Docs/ExampleGraph.jpg)

Features:
- HTN/utility ai hybrid: priorize tasks and select inputs by utility functions
- One included dependency: [xNode](https://github.com/Siccity/xNode)

Missing:
- Example project
- Interruptions like getting attacked

## Based upon
Introduction: https://www.youtube.com/watch?v=kXm467TFTcY

[Game AI Pro: Exploring HTN Planners through Example](http://www.gameaipro.com/GameAIPro/GameAIPro_Chapter12_Exploring_HTN_Planners_through_Example.pdf)

### Prerequisites
- Recent Unity version (2018)
- [xNode](https://github.com/Siccity/xNode)

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
