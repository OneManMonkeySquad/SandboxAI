using System;
using System.Linq;
using UnityEngine;
using XNodeEditor;
using XNode;
using UnityEditor;

namespace SandboxAI.HTN {
    [CustomNodeGraphEditor(typeof(Graph))]
    public class HTNGraphEditor : NodeGraphEditor {
        public override void OnGUI() {
            var graph = target as Graph;
            if (!graph.nodes.Any(n => n is MainNode)) {
                CreateNode(typeof(MainNode), new Vector2(-104, -40));
            }
        }

        public override string GetNodeMenuName(Type type) {
            if (type == typeof(CompoundTask))
                return "Compound Task";

            var subMenu = "";
            if (type.IsSubclassOf(typeof(TaskBase))) {
                subMenu = "Tasks/";
            }
            else if (type.IsSubclassOf(typeof(ContextualScorerBase))) {
                subMenu = "Ctx Scorers/";
            }
            else if (type.IsSubclassOf(typeof(OptionScorerBase))) {
                subMenu = "Option Scorers/";
            }
            else if (type.IsSubclassOf(typeof(CheckBase))) {
                subMenu = "Checks/";
            }

            //Check if type has the CreateNodeMenuAttribute
            Node.CreateNodeMenuAttribute attrib;
            if (NodeEditorUtilities.GetAttrib(type, out attrib)) // Return custom path
                return subMenu + attrib.menuName;
            else // Return generated path
                return subMenu + ObjectNames.NicifyVariableName(type.ToString().Replace('.', '/'));
        }

        public override Color GetTypeColor(Type type) {
            if (type.IsArray) {
                type = type.GetElementType();
            }
            return base.GetTypeColor(type);
        }
    }
}