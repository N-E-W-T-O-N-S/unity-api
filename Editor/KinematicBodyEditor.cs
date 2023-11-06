using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KinematicBody), true)]
[CanEditMultipleObjects]
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
        if (FindObjectOfType<PhysicsWorld>() == null)
            WarningBox("No PhysicsWorld in the current scene. Add a PhysicsWorld to enable physics calculations");
    }

    private void DrawProps()
    {
        if (kinematicBody.Body == null)
            return;

        kinematicBody.Mass = Mathf.Max(EditorGUILayout.FloatField("Mass", kinematicBody.Mass), NEWTONS.Core.PhysicsInfo.MinMass);
        kinematicBody.Drag = Mathf.Max(EditorGUILayout.FloatField("Drag", kinematicBody.Drag), NEWTONS.Core.PhysicsInfo.MinDrag);
        kinematicBody.Velocity = EditorGUILayout.Vector3Field("Velocity", kinematicBody.Velocity);
        EditorGUILayout.Space();
        kinematicBody.UseGravity = EditorGUILayout.Toggle("Use Gravity", kinematicBody.UseGravity);

    }

    private void DrawInfo()
    {
        if (!Application.isPlaying)
            return;
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

    private void WarningBox(string text)
    {
        EditorGUILayout.HelpBox(text, MessageType.Warning);
    }

    private void InfoBox(string text)
    {
        EditorGUILayout.HelpBox(text, MessageType.Info);
    }
}
