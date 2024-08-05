using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_Rigidbody)), CanEditMultipleObjects]
public class RigidBodyEditor : Editor
{
    NTS_Rigidbody rigidBody;
    private void OnEnable()
    {
        rigidBody = (NTS_Rigidbody)target;
    }

    public override void OnInspectorGUI()
    {
        if (FindObjectOfType<NTS_PhysicsWorld>() == null)
            WarningBox("No PhysicsWorld in the current scene. Add a PhysicsWorld to enable physics calculations");
        else if (FindObjectsOfType<NTS_PhysicsWorld>().Length > 1)
            ErrorBox("More than one PhysicsWorld in the current scene. Remove all but one PhysicsWorld to enable physics calculations");

        DrawProps();
    }

    private void DrawProps()
    {
        if (rigidBody.Body == null)
            return;

        Undo.RecordObject(rigidBody, "kinematic props");
        rigidBody.IsStatic = EditorGUILayout.Toggle("Is Static", rigidBody.IsStatic);
        if (rigidBody.IsStatic)
            return;

        rigidBody.Mass = Mathf.Max(EditorGUILayout.FloatField("Mass", rigidBody.Mass), NEWTONS.Core.PhysicsInfo.MinMass);
        rigidBody.Drag = Mathf.Max(EditorGUILayout.FloatField("Drag", rigidBody.Drag), NEWTONS.Core.PhysicsInfo.MinDrag);
        rigidBody.Velocity = EditorGUILayout.Vector3Field("Velocity", rigidBody.Velocity);
        EditorGUILayout.Space();
        rigidBody.UseGravity = EditorGUILayout.Toggle("Use Gravity", rigidBody.UseGravity);
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
