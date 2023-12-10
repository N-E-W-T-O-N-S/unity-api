using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KinematicBody2D))]
public class KinematicBodyEditor2D : Editor
{
    KinematicBody2D kinematicBody;
    private void OnEnable()
    {
        kinematicBody = (KinematicBody2D)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
        if (FindObjectOfType<PhysicsWorld2D>() == null)
            WarningBox("No PhysicsWorld in the current scene. Add a PhysicsWorld to enable physics calculations");
        else if (FindObjectsOfType<PhysicsWorld2D>().Length > 1)
            ErrorBox("More than one PhysicsWorld in the current scene. Remove all but one PhysicsWorld to enable physics calculations");
    }

    private void DrawProps()
    {
        if (kinematicBody.Body == null)
            return;

        kinematicBody.IsStatic = EditorGUILayout.Toggle("Is Static", kinematicBody.IsStatic);
        kinematicBody.Mass = Mathf.Max(EditorGUILayout.FloatField("Mass", kinematicBody.Mass), NEWTONS.Core.PhysicsInfo.MinMass);
        kinematicBody.Drag = Mathf.Max(EditorGUILayout.FloatField("Drag", kinematicBody.Drag), NEWTONS.Core.PhysicsInfo.MinDrag);
        kinematicBody.Velocity = EditorGUILayout.Vector2Field("Velocity", kinematicBody.Velocity);
        EditorGUILayout.Space();
        kinematicBody.UseGravity = EditorGUILayout.Toggle("Use Gravity", kinematicBody.UseGravity);
    }


    private void InfoBox(string text)
    {
        EditorGUILayout.HelpBox(text, MessageType.Info);
    }

    private void WarningBox(string text)
    {
        EditorGUILayout.HelpBox(text, MessageType.Warning);
    }

    private void ErrorBox(string text)
    {
        EditorGUILayout.HelpBox(text, MessageType.Error);
    }
}
