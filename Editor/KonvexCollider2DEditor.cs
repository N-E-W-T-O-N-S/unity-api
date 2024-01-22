using Codice.Client.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KonvexCollider2D))]
public class KonvexCollider2DEditor : Editor
{
    KonvexCollider2D konvexCollider;
    private void OnEnable()
    {
        konvexCollider = (KonvexCollider2D)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (konvexCollider.KonvexCollider == null)
            return;

        Undo.RecordObject(konvexCollider, "konvex props");
        Vector2 oldCenter = konvexCollider.Center;
        konvexCollider.Center = EditorGUILayout.Vector2Field("Center", konvexCollider.Center);
        Vector2 oldScale = konvexCollider.Scale;
        konvexCollider.Scale = EditorGUILayout.Vector2Field("Scale", new Vector3(Mathf.Max(konvexCollider.Scale.x, 0), Mathf.Max(konvexCollider.Scale.y, 0)));
        if (oldScale != konvexCollider.Scale || oldCenter != konvexCollider.Center)
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
        Vector2[] points = konvexCollider.Points;
        Vector2 offset = (Vector2)konvexCollider.transform.position + konvexCollider.Center;
        //for (int i = 0; i < points.Length; i++)
        //{
        //    Debug.Log(points[i]);
        //}
        Handles.color = Color.green;
        for (int i = 0; i < points.Length; i++)
        {
            Handles.DrawLine(points[i] + offset, points[(i + 1) % points.Length] + offset);
        }


    }
}
