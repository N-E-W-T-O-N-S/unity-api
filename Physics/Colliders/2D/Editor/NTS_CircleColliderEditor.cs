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
        _circleCollider.Validate();
    }

    public override void OnInspectorGUI()
    {
        DrawProps();

        if (GUI.changed)
        {
            _circleCollider.Validate();
        }
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

        if (oldRadius != _circleCollider.Radius || oldCenter != _circleCollider.Center)
        {
            SceneView.RepaintAll();
        }

        EditorGUILayout.Space();

        _circleCollider.foldOutDebugManager = EditorGUILayout.Foldout(_circleCollider.foldOutDebugManager, "Debug Manager");
        if (_circleCollider.foldOutDebugManager)
        {
            _circleCollider.debugManager.showMessages = EditorGUILayout.Toggle("Show Messages", _circleCollider.debugManager.showMessages);
            _circleCollider.debugManager.showWarnigs = EditorGUILayout.Toggle("Show Warnings", _circleCollider.debugManager.showWarnigs);
            _circleCollider.debugManager.showErrors = EditorGUILayout.Toggle("Show Errors", _circleCollider.debugManager.showErrors);
        }
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(_circleCollider.GlobalCenter, Vector3.forward, _circleCollider.ScaledRadius);
    }
}
