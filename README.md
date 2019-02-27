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

## Based upon
Introduction: https://www.youtube.com/watch?v=kXm467TFTcY

[Game AI Pro: Exploring HTN Planners through Example](http://www.gameaipro.com/GameAIPro/GameAIPro_Chapter12_Exploring_HTN_Planners_through_Example.pdf)

### Prerequisites
- Recent Unity version (2018)
- [xNode](https://github.com/Siccity/xNode)

## License
This project is licensed under the MIT License. You cannot sell this software on the Unity Asset Store or any other platform that sells software tools for developers. You may use this software in a commercial game.
