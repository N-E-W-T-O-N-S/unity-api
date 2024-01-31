using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_PhysicsWorld2D))]
public class PhysicsWorld2DEditor : Editor
{
    NTS_PhysicsWorld2D physicsWorld;

    private void OnEnable()
    {
        physicsWorld = (NTS_PhysicsWorld2D)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (Application.isPlaying)
        {
            NTS_PhysicsWorld2D.Gravity = EditorGUILayout.Vector2Field("Gravity", NTS_PhysicsWorld2D.Gravity);
            NTS_PhysicsWorld2D.UseCustomDrag = EditorGUILayout.Toggle("Use Physical Drag", NTS_PhysicsWorld2D.UseCustomDrag);
            if (!NTS_PhysicsWorld2D.UseCustomDrag)
                return;
            NTS_PhysicsWorld2D.Temperature = Mathf.Max(EditorGUILayout.FloatField("Temperature", NTS_PhysicsWorld2D.Temperature), NEWTONS.Core.PhysicsInfo.MinTemperature);
            NTS_PhysicsWorld2D.Density = Mathf.Max(EditorGUILayout.FloatField("Density", NTS_PhysicsWorld2D.Density), NEWTONS.Core.PhysicsInfo.MinDensity);
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
