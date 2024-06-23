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
        Undo.RecordObject(physicsWorld, "Properies Changed");

        NTS_PhysicsWorld2D.Instance.Steps = Mathf.Max(1, EditorGUILayout.IntField("Steps", NTS_PhysicsWorld2D.Instance.Steps));
        NTS_PhysicsWorld2D.Instance.Gravity = EditorGUILayout.Vector2Field("Gravity", NTS_PhysicsWorld2D.Instance.Gravity);
        NTS_PhysicsWorld2D.Instance.UseCustomDrag = EditorGUILayout.Toggle("Use Physical Drag", NTS_PhysicsWorld2D.Instance.UseCustomDrag);
        if (!NTS_PhysicsWorld2D.Instance.UseCustomDrag)
            return;
        NTS_PhysicsWorld2D.Instance.Temperature = Mathf.Max(EditorGUILayout.FloatField("Temperature", NTS_PhysicsWorld2D.Instance.Temperature), NEWTONS.Core.PhysicsInfo.MinTemperature);
        NTS_PhysicsWorld2D.Instance.Density = Mathf.Max(EditorGUILayout.FloatField("Density", NTS_PhysicsWorld2D.Instance.Density), NEWTONS.Core.PhysicsInfo.MinDensity);
    }
}
