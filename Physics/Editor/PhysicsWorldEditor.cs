using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_PhysicsWorld))]
public class PhysicsWorldEditor : Editor
{
    NTS_PhysicsWorld physicsWorld;

    private void OnEnable()
    {
        physicsWorld = (NTS_PhysicsWorld)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (Application.isPlaying)
        {
            NTS_PhysicsWorld.Gravity = EditorGUILayout.Vector3Field("Gravity", NTS_PhysicsWorld.Gravity);
            NTS_PhysicsWorld.UseCustomDrag = EditorGUILayout.Toggle("Use Physical Drag", NTS_PhysicsWorld.UseCustomDrag);
            if (!NTS_PhysicsWorld.UseCustomDrag)
                return;
            NTS_PhysicsWorld.Temperature = Mathf.Max(EditorGUILayout.FloatField("Temperature", NTS_PhysicsWorld.Temperature), NEWTONS.Core.PhysicsInfo.MinTemperature);
            NTS_PhysicsWorld.Density = Mathf.Max(EditorGUILayout.FloatField("Density", NTS_PhysicsWorld.Density), NEWTONS.Core.PhysicsInfo.MinDensity);
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
