using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace SandboxAI.HTN {
    [CustomNodeEditor(typeof(TaskBase))]
    public class TaskBaseEditor : NodeEditor {
        GUIStyle labelStyle;

        public TaskBaseEditor() {
            labelStyle = new GUIStyle(EditorStyles.label);
            labelStyle.wordWrap = true;
            labelStyle.richText = true;
        }

        public override void OnBodyGUI() {
            var task = (TaskBase)target;
            var desc = task.description;
            if(desc.Length > 0) {
                EditorGUILayout.LabelField(task.description, labelStyle);
            }
           
            base.OnBodyGUI();
        }
    }
}