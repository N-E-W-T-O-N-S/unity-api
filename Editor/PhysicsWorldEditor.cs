using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PhysicsWorld))]
public class PhysicsWorldEditor : Editor
{
    PhysicsWorld physicsWorld;

    private void OnEnable()
    {
        physicsWorld = (PhysicsWorld)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (Application.isPlaying)
        {
            PhysicsWorld.Gravity = EditorGUILayout.Vector3Field("Gravity", PhysicsWorld.Gravity);
            PhysicsWorld.UseCustomDrag = EditorGUILayout.Toggle("Use Physical Drag", PhysicsWorld.UseCustomDrag);
            if (!PhysicsWorld.UseCustomDrag)
                return;
            PhysicsWorld.Temperature = Mathf.Max(EditorGUILayout.FloatField("Temperature", PhysicsWorld.Temperature), NEWTONS.Core.PhysicsInfo.MinTemperature);
            PhysicsWorld.Density = Mathf.Max(EditorGUILayout.FloatField("Density", PhysicsWorld.Density), NEWTONS.Core.PhysicsInfo.MinDensity);
        }
        else
        {
            physicsWorld.initialGravity = EditorGUILayout.Vector3Field("Gravity", physicsWorld.initialGravity);
            physicsWorld.initialUseCustomDrag = EditorGUILayout.Toggle("Use Physical Drag", physicsWorld.initialUseCustomDrag);
            if (!physicsWorld.initialUseCustomDrag)
                return;
            physicsWorld.initialTemperature = Mathf.Max(EditorGUILayout.FloatField("Temperature", physicsWorld.initialTemperature), NEWTONS.Core.PhysicsInfo.MinTemperature);
            physicsWorld.initialDensity = Mathf.Max(EditorGUILayout.FloatField("Density", physicsWorld.initialDensity), NEWTONS.Core.PhysicsInfo.MinDensity);
        }
    }
}
