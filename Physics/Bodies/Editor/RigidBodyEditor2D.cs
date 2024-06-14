using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_Rigidbody2D)), CanEditMultipleObjects]
public class KinematicBodyEditor2D : Editor
{
    NTS_Rigidbody2D _rigidBody;
    private void OnEnable()
    {
        _rigidBody = (NTS_Rigidbody2D)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
        if (FindObjectOfType<NTS_PhysicsWorld2D>() == null)
            WarningBox("No PhysicsWorld in the current scene. Add a PhysicsWorld to enable physics calculations");
        else if (FindObjectsOfType<NTS_PhysicsWorld2D>().Length > 1)
            ErrorBox("More than one PhysicsWorld in the current scene. Remove all but one PhysicsWorld to enable physics calculations");
    }

    private void DrawProps()
    {
        if (_rigidBody.Body == null)
            return;

        Undo.RecordObject(_rigidBody, "2d kinematic props");
        _rigidBody.IsStatic = EditorGUILayout.Toggle("Is Static", _rigidBody.IsStatic);

        EditorGUILayout.Space();

        if (_rigidBody.IsStatic)
            return;

        _rigidBody.Mass = Mathf.Max(EditorGUILayout.FloatField("Mass", _rigidBody.Mass), NEWTONS.Core.PhysicsInfo.MinMass);
        _rigidBody.Drag = Mathf.Max(EditorGUILayout.FloatField("Drag", _rigidBody.Drag), NEWTONS.Core.PhysicsInfo.MinDrag);
        _rigidBody.Velocity = EditorGUILayout.Vector2Field("Velocity", _rigidBody.Velocity);
        _rigidBody.FixRotation = EditorGUILayout.Toggle("Fix Rotation", _rigidBody.FixRotation);

        if (!_rigidBody.FixRotation)
            _rigidBody.AngularVelocity = EditorGUILayout.FloatField("Angular Velocity", _rigidBody.AngularVelocity);

        EditorGUILayout.Space();

        _rigidBody.UseGravity = EditorGUILayout.Toggle("Use Gravity", _rigidBody.UseGravity);

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
