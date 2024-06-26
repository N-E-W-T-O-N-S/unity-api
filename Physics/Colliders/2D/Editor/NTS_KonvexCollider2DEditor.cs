using Codice.Client.Commands;
using NEWTONS.Core._3D;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_KonvexCollider2D)), CanEditMultipleObjects]
public class NTS_KonvexCollider2DEditor : Editor
{
    NTS_KonvexCollider2D _konvexCollider;
    private void OnEnable()
    {
        _konvexCollider = (NTS_KonvexCollider2D)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (_konvexCollider.KonvexCollider == null)
            return;

        Undo.RecordObject(_konvexCollider, "konvex props");
        Vector2 oldCenter = _konvexCollider.Center;
        _konvexCollider.Center = EditorGUILayout.Vector2Field("Center", _konvexCollider.Center);
        Vector2 oldScale = _konvexCollider.Size;
        Vector2 size = EditorGUILayout.Vector2Field("Size", _konvexCollider.Size);
        _konvexCollider.Size = new Vector2(Mathf.Max(0, size.x), Mathf.Max(0, size.y));

        _konvexCollider.Restitution = EditorGUILayout.Slider("Restitution", _konvexCollider.Restitution, 0, 1);

        if (oldScale != _konvexCollider.Size || oldCenter != _konvexCollider.Center)
        {
            SceneView.RepaintAll();
        }
        EditorGUILayout.Space();
    }

    private void OnSceneGUI()
    {
        Vector2[] points = _konvexCollider.Points;
        Vector2 offset = _konvexCollider.GlobalCenter;

        Handles.color = Color.green;
        for (int i = 0; i < points.Length; i++)
        {
            Handles.DrawLine(points[i] + offset, points[(i + 1) % points.Length] + offset);
        }

        EditorHelper.DrawBounds(_konvexCollider);
    }
}
