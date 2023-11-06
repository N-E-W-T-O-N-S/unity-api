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
        serializedObject.Update();
        DrawProps();
    }

    private void DrawProps()
    {
        if (cuboidCollider.CuboidColl == null)
            return;

        cuboidCollider.Center = EditorGUILayout.Vector3Field("Center", cuboidCollider.Center);
        cuboidCollider.Scale = EditorGUILayout.Vector3Field("Scale", cuboidCollider.Scale);
        EditorGUILayout.Space();

        Debug.Log(cuboidCollider.Points.Length);

        //SerializedProperty listProperty = serializedObject.FindProperty("Points");
        //EditorGUILayout.PropertyField(listProperty, true);

        //serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        //TODO: think about center and position of collider (is center just offset or compllete position?)
        Handles.color = Color.green;
        Handles.DrawLine(cuboidCollider.Points[0] + cuboidCollider.transform.position + cuboidCollider.Center, cuboidCollider.Points[1] + cuboidCollider.transform.position + cuboidCollider.Center);
    }
}
