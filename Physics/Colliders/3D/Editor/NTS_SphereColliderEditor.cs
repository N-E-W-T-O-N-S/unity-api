using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NTS_SphereCollider)), CanEditMultipleObjects]
public class NTS_SphereColliderEditor : Editor
{
    private NTS_SphereCollider _sphereCollider;

    private void OnEnable()
    {
        _sphereCollider = (NTS_SphereCollider)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        if (_sphereCollider.SphereCollider == null)
            return;

        Undo.RecordObject(_sphereCollider, "sphere props");

        Vector3 oldCenter = _sphereCollider.Center;
        _sphereCollider.Center = EditorGUILayout.Vector3Field("Center", _sphereCollider.Center);

        float oldRadius = _sphereCollider.Radius;
        _sphereCollider.Radius = Mathf.Max(EditorGUILayout.FloatField("Radius", _sphereCollider.Radius), 0);

        _sphereCollider.Restitution = EditorGUILayout.Slider("Restitution", _sphereCollider.Restitution, 0, 1);

        if (oldRadius != _sphereCollider.Radius || oldCenter != _sphereCollider.Center)
        {
            SceneView.RepaintAll();
        }
        EditorGUILayout.Space();
    }

    private void OnSceneGUI()
    {
        float scaledRadius = _sphereCollider.ScaledRadius;

        Handles.color = Color.green;
        Handles.DrawWireDisc(_sphereCollider.GlobalCenter, _sphereCollider.Rotation * Vector3.right, scaledRadius);
        Handles.DrawWireDisc(_sphereCollider.GlobalCenter, _sphereCollider.Rotation * Vector3.up, scaledRadius);
        Handles.DrawWireDisc(_sphereCollider.GlobalCenter, _sphereCollider.Rotation * Vector3.forward, scaledRadius);

        Handles.DrawWireDisc(_sphereCollider.GlobalCenter, _sphereCollider.Rotation * new Vector3(0.707107f, 0f, 0.707107f), scaledRadius);
        Handles.DrawWireDisc(_sphereCollider.GlobalCenter, _sphereCollider.Rotation * new Vector3(0.707107f, 0f, -0.707107f), scaledRadius);

        // Handles.DrawWireDisc(_sphereCollider.GlobalCenter, _sphereCollider.Rotation * new Vector3(0f, 0.707107f, 0.707107f), scaledRadius);
        // Handles.DrawWireDisc(_sphereCollider.GlobalCenter, _sphereCollider.Rotation * new Vector3(0f, 0.707107f, -0.707107f), scaledRadius);

        Handles.DrawWireDisc(_sphereCollider.GlobalCenter + (_sphereCollider.Rotation * Vector3.up * scaledRadius * 0.5f), _sphereCollider.Rotation * Vector3.up, scaledRadius * Mathf.Cos(30f * Mathf.Deg2Rad));
        Handles.DrawWireDisc(_sphereCollider.GlobalCenter - (_sphereCollider.Rotation * Vector3.up * scaledRadius * 0.5f), _sphereCollider.Rotation * Vector3.up, scaledRadius * Mathf.Cos(30f * Mathf.Deg2Rad));

    }
}
