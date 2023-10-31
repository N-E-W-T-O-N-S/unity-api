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
        if (Application.isPlaying)
        {
            if (cuboidCollider.GetCuboidColliderRef() == null)
                return;
                
            cuboidCollider.Center = EditorGUILayout.Vector3Field("Center", cuboidCollider.Center);
            cuboidCollider.Scale = EditorGUILayout.Vector3Field("Scale", cuboidCollider.Scale);
        }
        else
        {
            cuboidCollider.initialCenter = EditorGUILayout.Vector3Field("Center", cuboidCollider.initialCenter);
            cuboidCollider.initialScale = EditorGUILayout.Vector3Field("Scale", cuboidCollider.initialScale);
        }
    }
}
