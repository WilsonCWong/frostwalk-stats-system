#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace Frostwalk.StatSystem.Editor
{
    public class StatDrawer : OdinValueDrawer<Stat>
    {
        float addValue = 0;
        bool toolsToggle = false;

        protected override void Initialize()
        {
            base.Initialize();
            Stat value = this.ValueEntry.SmartValue;
            if (value != null) value.Reset();
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            Stat value = this.ValueEntry.SmartValue;

            GUIHelper.GetCurrentLayoutStyle().padding.left = 10;
            var rect = EditorGUILayout.GetControlRect();

            // In Odin, labels are optional and can be null, so we have to account for that.
            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }

            GUILayout.Label("Skill Properties");

            var prev = EditorGUIUtility.labelWidth;
            var prevIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            EditorGUIUtility.labelWidth = 120;

            SirenixEditorGUI.BeginBox();
            EditorGUI.BeginChangeCheck();
            float sp = EditorGUILayout.FloatField("Starting Points", value.StartingPoints);
            if (EditorGUI.EndChangeCheck()) {
                value.RemoveSkillPoints(value.StartingPoints);
                value.StartingPoints = sp;
                value.AddSkillPoints(sp);
            }
            for (int i = 0; i < this.Property.Children.Count; i++)
            {
                var child = this.Property.Children[i];
                if (child.Name == "StartingPoints") continue;
                child.Draw(child.Label);
            }
            SirenixEditorGUI.EndBox();

            GUILayout.Space(8);

            GUILayout.Label("Current Progress");
            SirenixEditorGUI.BeginBox();
            GUILayout.Label("Current Points: " + value.CurrentPoints);
            GUILayout.Label("Current Experience: " + value.CurrentExp);
            SirenixEditorGUI.EndBox();

            GUILayout.Space(8);

            // DictionaryDrawer uses the CN Box GUIStyle, so we use it to hide tools in the AddKeyDrawer.
            if (GUIHelper.GetCurrentLayoutStyle().name != "CN Box")
                toolsToggle = SirenixEditorGUI.Foldout(toolsToggle, "Tools");

            if (toolsToggle)
            {
                SirenixEditorGUI.BeginBox();
                GUILayout.Space(8);
                addValue = EditorGUILayout.FloatField("Value to Add", addValue);
                if (GUILayout.Button("Add Points"))
                    value.AddSkillPoints(addValue);
                if (GUILayout.Button("Add Exp"))
                    value.AddExp(addValue);
                if (GUILayout.Button("Reset"))
                    value.Reset();
                GUILayout.Space(8);
                SirenixEditorGUI.EndBox();
            }

            GUILayout.Space(8);

            EditorGUIUtility.labelWidth = prev;
            EditorGUI.indentLevel = prevIndent;

            this.ValueEntry.SmartValue = value;
        }
    }
}
#endif
