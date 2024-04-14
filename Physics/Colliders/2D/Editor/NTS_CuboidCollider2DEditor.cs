using Codice.Client.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_CuboidCollider2D)), CanEditMultipleObjects]
public class NTS_CuboidCollider2DEditor : Editor
{
    NTS_CuboidCollider2D cuboidCollider;
    private void OnEnable()
    {
        cuboidCollider = (NTS_CuboidCollider2D)target;
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
        if (cuboidCollider.CuboidCollider == null)
            return;

        Undo.RecordObject(cuboidCollider, "konvex props");
        Vector2 oldCenter = cuboidCollider.Center;
        cuboidCollider.Center = EditorGUILayout.Vector2Field("Center", cuboidCollider.Center);
        Vector2 oldScale = cuboidCollider.Size;
        Vector2 size = EditorGUILayout.Vector2Field("Size", cuboidCollider.Size);
        cuboidCollider.Size = new Vector2(Mathf.Max(0, size.x), Mathf.Max(0, size.y));

        if (oldScale != cuboidCollider.Size || oldCenter != cuboidCollider.Center)
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
        Vector2[] points = cuboidCollider.Points;
        Vector2 offset = (Vector2)cuboidCollider.transform.position + cuboidCollider.Center;

        Handles.color = Color.green;
        for (int i = 0; i < points.Length; i++)
        {
            Handles.DrawLine(points[i] + offset, points[(i + 1) % points.Length] + offset);
        }


    }
}
