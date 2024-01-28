using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CuboidCollider))]
public class CuboidColliderEditor : Editor
{
    CuboidCollider cuboidCollider;
    private void OnEnable()
    {
        cuboidCollider = (CuboidCollider)target;
        cuboidCollider.Validate();
    }

    public override void OnInspectorGUI()
    {
        DrawProps();

        if (GUI.changed)
        {
            cuboidCollider.Validate();
        }
    }

    private void DrawProps()
    {
        if (cuboidCollider.CuboidColl == null)
            return;

        Undo.RecordObject(cuboidCollider, "cuboid props");
        Vector3 oldCenter = cuboidCollider.Center;
        cuboidCollider.Center = EditorGUILayout.Vector3Field("Center", cuboidCollider.Center);
        Vector3 oldScale = cuboidCollider.Scale;
        cuboidCollider.Scale = EditorGUILayout.Vector3Field("Scale", new Vector3(Mathf.Max(cuboidCollider.Scale.x, 0), Mathf.Max(cuboidCollider.Scale.y, 0), Mathf.Max(cuboidCollider.Scale.z, 0)));
        
        cuboidCollider.Restitution = EditorGUILayout.Slider("Restitution", cuboidCollider.Restitution, 0, 1);
        
        if (oldScale != cuboidCollider.Scale || oldCenter != cuboidCollider.Center)
        {
            SceneView.RepaintAll();
        }
        EditorGUILayout.Space();

        cuboidCollider.foldOutDebugManager = EditorGUILayout.Foldout(cuboidCollider.foldOutDebugManager, "Debug Manager");
        if (cuboidCollider.foldOutDebugManager)
        {
            cuboidCollider.debugManager.showMessages = EditorGUILayout.Toggle("Show Messages", cuboidCollider.debugManager.showMessages);
            cuboidCollider.debugManager.showWarnigs = EditorGUILayout.Toggle("Show Warnings", cuboidCollider.debugManager.showWarnigs);
            cuboidCollider.debugManager.showErrors = EditorGUILayout.Toggle("Show Errors", cuboidCollider.debugManager.showErrors);
        }

    }

    private void OnSceneGUI()
    {
        Vector3[] points = cuboidCollider.Points;
        int[] indices = cuboidCollider.Indices;
        Vector3 offset = cuboidCollider.transform.position + cuboidCollider.Center;

        Handles.color = Color.green;

        for (int i = 0; i < indices.Length; i += 2)
        {
            Handles.DrawLine(points[indices[i]] + offset, points[indices[i + 1]] + offset);
        }
    }
}
