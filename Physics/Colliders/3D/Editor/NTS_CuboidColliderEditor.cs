using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_CuboidCollider)), CanEditMultipleObjects]
public class NTS_CuboidColliderEditor : Editor
{
    NTS_CuboidCollider cuboidCollider;
    private void OnEnable()
    {
        cuboidCollider = (NTS_CuboidCollider)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (cuboidCollider.CuboidCollider == null)
            return;

        Undo.RecordObject(cuboidCollider, "cuboid props");
        Vector3 oldCenter = cuboidCollider.Center;
        cuboidCollider.Center = EditorGUILayout.Vector3Field("Center", cuboidCollider.Center);
        Vector3 oldSize = cuboidCollider.Size;
        Vector3 size = cuboidCollider.Size;
        cuboidCollider.Size = EditorGUILayout.Vector3Field("Scale", new Vector3(Mathf.Max(size.x, 0), Mathf.Max(size.y, 0), Mathf.Max(size.z, 0)));
        
        cuboidCollider.Restitution = EditorGUILayout.Slider("Restitution", cuboidCollider.Restitution, 0, 1);
        
        if (oldSize != cuboidCollider.Size || oldCenter != cuboidCollider.Center)
        {
            SceneView.RepaintAll();
        }
        EditorGUILayout.Space();
    }

    private void OnSceneGUI()
    {
        Vector3[] points = cuboidCollider.Points;
        int[] indices = cuboidCollider.Indices;
        Vector3 offset = cuboidCollider.GlobalCenter;


        Handles.color = Color.green;

        for (int i = 0; i < indices.Length; i += 2)
        {
            Handles.DrawLine(points[indices[i]] + offset, points[indices[i + 1]] + offset);
        }
    }
}
