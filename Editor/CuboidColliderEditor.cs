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
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (cuboidCollider.CuboidColl == null)
            return;

        Vector3 oldCenter = cuboidCollider.Center;
        cuboidCollider.Center = EditorGUILayout.Vector3Field("Center", cuboidCollider.Center);
        Vector3 oldScale = cuboidCollider.Scale;
        cuboidCollider.Scale = EditorGUILayout.Vector3Field("Scale", cuboidCollider.Scale);
        if (oldScale != cuboidCollider.Scale || oldCenter != cuboidCollider.Center)
        {
            SceneView.RepaintAll();
        }
        EditorGUILayout.Space();

        //SerializedProperty listProperty = serializedObject.FindProperty("Points");
        //EditorGUILayout.PropertyField(listProperty, true);

        //serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        Vector3[] points = cuboidCollider.ScaledPoints;
        Vector3 offset = cuboidCollider.transform.position + cuboidCollider.Center;
        //for (int i = 0; i < points.Length; i++)
        //{
        //    Debug.Log(points[i]);
        //}
        Handles.color = Color.green;
        Handles.DrawLine(points[0] + offset, points[1] + offset);
        Handles.DrawLine(points[1] + offset, points[3] + offset);
        Handles.DrawLine(points[2] + offset, points[3] + offset);
        Handles.DrawLine(points[0] + offset, points[2] + offset);
        
        Handles.DrawLine(points[4] + offset, points[5] + offset);
        Handles.DrawLine(points[5] + offset, points[7] + offset);
        Handles.DrawLine(points[6] + offset, points[7] + offset);
        Handles.DrawLine(points[4] + offset, points[6] + offset);

        Handles.DrawLine(points[0] + offset, points[4] + offset);
        Handles.DrawLine(points[1] + offset, points[5] + offset);
        Handles.DrawLine(points[2] + offset, points[6] + offset);
        Handles.DrawLine(points[3] + offset, points[7] + offset);


    }
}
