using Codice.Client.Commands;
using NEWTONS.Core._2D;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_CuboidCollider2D)), CanEditMultipleObjects]
public class NTS_CuboidCollider2DEditor : Editor
{
    NTS_CuboidCollider2D _cuboidCollider;
    private void OnEnable()
    {
        _cuboidCollider = (NTS_CuboidCollider2D)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (_cuboidCollider.CuboidCollider == null)
            return;

        Undo.RecordObject(_cuboidCollider, "konvex props");
        Vector2 oldCenter = _cuboidCollider.Center;
        _cuboidCollider.Center = EditorGUILayout.Vector2Field("Center", _cuboidCollider.Center);
        Vector2 oldSize = _cuboidCollider.Size;
        Vector2 size = EditorGUILayout.Vector2Field("Size", _cuboidCollider.Size);
        _cuboidCollider.Size = new Vector2(Mathf.Max(0, size.x), Mathf.Max(0, size.y));

        _cuboidCollider.Restitution = EditorGUILayout.Slider("Restitution", _cuboidCollider.Restitution, 0, 1);

        GUI.enabled = false;
        EditorGUILayout.FloatField("Inertia", _cuboidCollider.Body.Inertia);
        GUI.enabled = true;

        if (oldSize != _cuboidCollider.Size || oldCenter != _cuboidCollider.Center)
        {
            SceneView.RepaintAll();
        }
        EditorGUILayout.Space();
    }

    private void OnSceneGUI()
    {
        Vector2[] points = _cuboidCollider.Points;
        Vector2 offset = _cuboidCollider.GlobalCenter;

        Handles.color = Color.green;
        for (int i = 0; i < points.Length; i++)
        {
            Handles.DrawLine(points[i] + offset, points[(i + 1) % points.Length] + offset);
        }

        EditorHelper.DrawBounds(_cuboidCollider);

    }
}
