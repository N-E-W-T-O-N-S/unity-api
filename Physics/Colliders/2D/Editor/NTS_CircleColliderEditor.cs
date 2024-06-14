using NEWTONS.Core._3D;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_CircleCollider))]
public class NTS_CircleColliderEditor : Editor
{
    private NTS_CircleCollider _circleCollider;

    private void OnEnable()
    {
        _circleCollider = (NTS_CircleCollider)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (_circleCollider.CircleCollider == null)
            return;

        Undo.RecordObject(_circleCollider, "circle props");

        Vector2 oldCenter = _circleCollider.Center;
        _circleCollider.Center = EditorGUILayout.Vector2Field("Center", _circleCollider.Center);

        float oldRadius = _circleCollider.Radius;
        _circleCollider.Radius = Mathf.Max(EditorGUILayout.FloatField("Radius", _circleCollider.Radius), 0);

        _circleCollider.Restitution = EditorGUILayout.Slider("Restitution", _circleCollider.Restitution, 0, 1);

        GUI.enabled = false;
        EditorGUILayout.FloatField("Inertia", _circleCollider.Body.Inertia);
        GUI.enabled = true;

        if (oldRadius != _circleCollider.Radius || oldCenter != _circleCollider.Center)
        {
            SceneView.RepaintAll();
        }

        EditorGUILayout.Space();
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(_circleCollider.GlobalCenter, Vector3.forward, _circleCollider.ScaledRadius);
    }
}
