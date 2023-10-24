using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KinematicBody))]
public class KinematicBodyEditor : Editor
{
    KinematicBody kinematicBody;
    private void OnEnable()
    {
        kinematicBody = (KinematicBody)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
        DrawInfo();
    }

    private void DrawProps()
    {
        kinematicBody.Position = EditorGUILayout.Vector3Field("Position", kinematicBody.Position);
        kinematicBody.Mass = EditorGUILayout.FloatField("Mass", kinematicBody.Mass);
        kinematicBody.UseGravity = EditorGUILayout.Toggle("Use Gravity", kinematicBody.UseGravity);
    }

    private void DrawInfo()
    {
        kinematicBody.foldOutInfo = EditorGUILayout.Foldout(kinematicBody.foldOutInfo, "Info");
        if (kinematicBody.foldOutInfo)
        {
            EditorGUI.indentLevel++;
            GUI.enabled = false;
            kinematicBody.Velocity = EditorGUILayout.Vector3Field("Velocity", kinematicBody.Velocity);
            GUI.enabled = true;
            EditorGUI.indentLevel++;
        }
    }
}
