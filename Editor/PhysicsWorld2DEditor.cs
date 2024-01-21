using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PhysicsWorld2D))]
public class PhysicsWorld2DEditor : Editor
{
    PhysicsWorld2D physicsWorld;

    private void OnEnable()
    {
        physicsWorld = (PhysicsWorld2D)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (Application.isPlaying)
        {
            PhysicsWorld2D.Gravity = EditorGUILayout.Vector2Field("Gravity", PhysicsWorld2D.Gravity);
            PhysicsWorld2D.UseCustomDrag = EditorGUILayout.Toggle("Use Physical Drag", PhysicsWorld2D.UseCustomDrag);
            if (!PhysicsWorld2D.UseCustomDrag)
                return;
            PhysicsWorld2D.Temperature = Mathf.Max(EditorGUILayout.FloatField("Temperature", PhysicsWorld2D.Temperature), NEWTONS.Core.PhysicsInfo.MinTemperature);
            PhysicsWorld2D.Density = Mathf.Max(EditorGUILayout.FloatField("Density", PhysicsWorld2D.Density), NEWTONS.Core.PhysicsInfo.MinDensity);
        }
        else
        {
            physicsWorld.initialGravity = EditorGUILayout.Vector2Field("Gravity", physicsWorld.initialGravity);
            physicsWorld.initialUseCustomDrag = EditorGUILayout.Toggle("Use Physical Drag", physicsWorld.initialUseCustomDrag);
            if (!physicsWorld.initialUseCustomDrag)
                return;
            physicsWorld.initialTemperature = Mathf.Max(EditorGUILayout.FloatField("Temperature", physicsWorld.initialTemperature), NEWTONS.Core.PhysicsInfo.MinTemperature);
            physicsWorld.initialDensity = Mathf.Max(EditorGUILayout.FloatField("Density", physicsWorld.initialDensity), NEWTONS.Core.PhysicsInfo.MinDensity);
        }
    }
}
