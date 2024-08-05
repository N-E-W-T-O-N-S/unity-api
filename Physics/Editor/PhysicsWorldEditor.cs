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
        Undo.RecordObject(physicsWorld, "Properies Changed");

        NTS_PhysicsWorld.Instance.Steps = Mathf.Max(1, EditorGUILayout.IntField("Steps", NTS_PhysicsWorld.Instance.Steps));
        NTS_PhysicsWorld.Instance.Gravity = EditorGUILayout.Vector3Field("Gravity", NTS_PhysicsWorld.Instance.Gravity);
        NTS_PhysicsWorld.Instance.UseCustomDrag = EditorGUILayout.Toggle("Use Physical Drag", NTS_PhysicsWorld.Instance.UseCustomDrag);
        if (!NTS_PhysicsWorld.Instance.UseCustomDrag)
            return;
        NTS_PhysicsWorld.Instance.Temperature = Mathf.Max(EditorGUILayout.FloatField("Temperature", NTS_PhysicsWorld.Instance.Temperature), NEWTONS.Core.PhysicsInfo.MinTemperature);
        NTS_PhysicsWorld.Instance.Density = Mathf.Max(EditorGUILayout.FloatField("Density", NTS_PhysicsWorld.Instance.Density), NEWTONS.Core.PhysicsInfo.MinDensity);
    }
}
