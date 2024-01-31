using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_Rigidbody)), CanEditMultipleObjects]
public class KinematicBodyEditor : Editor
{
    NTS_Rigidbody kinematicBody;
    private void OnEnable()
    {
        kinematicBody = (NTS_Rigidbody)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
        if (FindObjectOfType<NTS_PhysicsWorld>() == null)
            WarningBox("No PhysicsWorld in the current scene. Add a PhysicsWorld to enable physics calculations");
        else if (FindObjectsOfType<NTS_PhysicsWorld>().Length > 1)
            ErrorBox("More than one PhysicsWorld in the current scene. Remove all but one PhysicsWorld to enable physics calculations");
    }

    private void DrawProps()
    {
        if (kinematicBody.Body == null)
            return;

        Undo.RecordObject(kinematicBody, "kinematic props");
        kinematicBody.IsStatic = EditorGUILayout.Toggle("Is Static", kinematicBody.IsStatic);
        kinematicBody.Mass = Mathf.Max(EditorGUILayout.FloatField("Mass", kinematicBody.Mass), NEWTONS.Core.PhysicsInfo.MinMass);
        kinematicBody.Drag = Mathf.Max(EditorGUILayout.FloatField("Drag", kinematicBody.Drag), NEWTONS.Core.PhysicsInfo.MinDrag);
        kinematicBody.Velocity = EditorGUILayout.Vector3Field("Velocity", kinematicBody.Velocity);
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
