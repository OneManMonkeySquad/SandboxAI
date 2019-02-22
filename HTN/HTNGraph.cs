using SandboxAI.HTN;
using System;
using System.Linq;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class HTNGraph : NodeGraph {
    [NonSerialized]
    TaskBase _mainTask;
    public TaskBase mainTask {
        get {
            if (_mainTask == null) {
                var mn = nodes.Where(n => n is MainNode).First() as MainNode;
                if (mn == null) {
                    Debug.LogWarning("No MainNode found, instantiate one in the graph");
                    return null;
                }
                
                _mainTask = mn.GetInputValue<TaskBase>("main");
            }
            return _mainTask;
        }
    }
}