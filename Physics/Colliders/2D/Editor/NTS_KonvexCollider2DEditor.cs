using Codice.Client.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_KonvexCollider2D)), CanEditMultipleObjects]
public class NTS_KonvexCollider2DEditor : Editor
{
    NTS_KonvexCollider2D konvexCollider;
    private void OnEnable()
    {
        konvexCollider = (NTS_KonvexCollider2D)target;
        konvexCollider.Validate();
    }

    public override void OnInspectorGUI()
    {
        DrawProps();

        if (GUI.changed)
        {
            konvexCollider.Validate();
        }
    }

    private void DrawProps()
    {
        if (konvexCollider.KonvexCollider == null)
            return;

        Undo.RecordObject(konvexCollider, "konvex props");
        Vector2 oldCenter = konvexCollider.Center;
        konvexCollider.Center = EditorGUILayout.Vector2Field("Center", konvexCollider.Center);
        Vector2 oldScale = konvexCollider.Size;
        Vector2 size = EditorGUILayout.Vector2Field("Size", konvexCollider.Size);
        konvexCollider.Size = new Vector2(Mathf.Max(0, size.x), Mathf.Max(0, size.y));

        if (oldScale != konvexCollider.Size || oldCenter != konvexCollider.Center)
        {
            SceneView.RepaintAll();
        }
        EditorGUILayout.Space();

        konvexCollider.foldOutDebugManager = EditorGUILayout.Foldout(konvexCollider.foldOutDebugManager, "Debug Manager");
        if (konvexCollider.foldOutDebugManager)
        {
            konvexCollider.debugManager.showMessages = EditorGUILayout.Toggle("Show Messages", konvexCollider.debugManager.showMessages);
            konvexCollider.debugManager.showWarnigs = EditorGUILayout.Toggle("Show Warnings", konvexCollider.debugManager.showWarnigs);
            konvexCollider.debugManager.showErrors = EditorGUILayout.Toggle("Show Errors", konvexCollider.debugManager.showErrors);
        }
    }

    private void OnSceneGUI()
    {
        Vector2[] points = konvexCollider.Points;
        Vector2 offset = (Vector2)konvexCollider.transform.position + konvexCollider.Center;

        Handles.color = Color.green;
        for (int i = 0; i < points.Length; i++)
        {
            Handles.DrawLine(points[i] + offset, points[(i + 1) % points.Length] + offset);
        }


    }
}
