using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static log4net.Appender.ColoredConsoleAppender;

[CustomEditor(typeof(NTS_PhysicsWorld2D))]
public class PhysicsWorld2DEditor : Editor
{
    NTS_PhysicsWorld2D physicsWorld;

    private void OnEnable()
    {
        physicsWorld = (NTS_PhysicsWorld2D)target;
    }

    public override void OnInspectorGUI()
    {
        DrawProps();
    }

    private void DrawProps()
    {
        Undo.RecordObject(physicsWorld, "Properies Changed");

        NTS_PhysicsWorld2D.Instance.Steps = Mathf.Max(1, EditorGUILayout.IntField("Steps", NTS_PhysicsWorld2D.Instance.Steps));
        NTS_PhysicsWorld2D.Instance.Gravity = EditorGUILayout.Vector2Field("Gravity", NTS_PhysicsWorld2D.Instance.Gravity);
        NTS_PhysicsWorld2D.Instance.BroadphaseAlgorithm = (NTS_PhysicsWorld2D.Broadphase)EditorGUILayout.EnumPopup("Broadphase Algorithm", NTS_PhysicsWorld2D.Instance.BroadphaseAlgorithm);

        EditorGUILayout.Space();

        NTS_PhysicsWorld2D.Instance.UseCustomDrag = EditorGUILayout.Toggle("Use Custom Drag", NTS_PhysicsWorld2D.Instance.UseCustomDrag);
        if (NTS_PhysicsWorld2D.Instance.UseCustomDrag)
        {
            //NTS_PhysicsWorld2D.Instance.Temperature = Mathf.Max(EditorGUILayout.FloatField("Temperature", NTS_PhysicsWorld2D.Instance.Temperature), NEWTONS.Core.PhysicsInfo.MinTemperature);
            NTS_PhysicsWorld2D.Instance.Density = Mathf.Max(EditorGUILayout.FloatField("Density", NTS_PhysicsWorld2D.Instance.Density), NEWTONS.Core.PhysicsInfo.MinDensity);
        }

        EditorGUILayout.Space();

        physicsWorld.foldOutAirCompositions = EditorGUILayout.Foldout(physicsWorld.foldOutAirCompositions, "Air Compositions", true);

        if (!physicsWorld.foldOutAirCompositions)
            return;

        EditorGUI.indentLevel++;

        var airComps = NTS_PhysicsWorld2D.Instance.AirCompositions;
        var colors = physicsWorld.airCompositionColors;
        var foldOuts = physicsWorld.airCompositionFoldOuts;

        while (colors.Count < airComps.Count)
            colors.Add(new(0.7f, 0.2f, 0.1f, 1f));
        while (foldOuts.Count < airComps.Count)
            foldOuts.Add(false);

        for (int i = 0; i < airComps.Count; i++)
        {
            foldOuts[i] = EditorGUILayout.Foldout(foldOuts[i], $"Composition {i + 1}", true);
            if (!foldOuts[i])
                continue;

            var item = airComps[i];
            Bounds bounds = item.Bounds.ToUnityBounds();

            bounds.center = EditorGUILayout.Vector2Field("Center", bounds.center);
            bounds.extents = EditorGUILayout.Vector2Field("Extents", bounds.extents);
            item.Density = EditorGUILayout.FloatField("Density", item.Density);
            colors[i] = EditorGUILayout.ColorField("Color", colors[i]);

            item.Bounds = bounds.ToNewtonsBounds2D();
            airComps[i] = item;

            EditorGUILayout.Space();
        }
        SceneView.RepaintAll();

        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();

        GUIStyle style = new(GUI.skin.button)
        {
            fontSize = 18
        };

        if (GUILayout.Button("+", style, GUILayout.Width(22f), GUILayout.Height(22f)))
        {
            airComps.Add(new()
            {
                Bounds = new(new NEWTONS.Core.Vector2(-0.5f, -0.5f), new NEWTONS.Core.Vector2(0.5f, 0.5f)),
                Density = NEWTONS.Core._2D.Physics2D.airDensity,
            });

            if (colors.Count < airComps.Count)
                colors.Add(new(0.7f, 0.2f, 0.1f, 1f));

            if (foldOuts.Count < airComps.Count)
                foldOuts.Add(true);
        }

        if (GUILayout.Button("-", style, GUILayout.Width(22f), GUILayout.Height(22f)))
        {
            airComps.RemoveAt(foldOuts.Count - 1);
            colors.RemoveAt(foldOuts.Count - 1);
            foldOuts.RemoveAt(foldOuts.Count - 1);
        }

        EditorGUILayout.EndHorizontal();

    }

    private void OnSceneGUI()
    {
        Undo.RecordObject(physicsWorld, "AirComposition Position Change");
        var airComps = NTS_PhysicsWorld2D.Instance.AirCompositions;
        var colors = physicsWorld.airCompositionColors;
        var foldOuts = physicsWorld.airCompositionFoldOuts;

        while (colors.Count < airComps.Count)
            colors.Add(new(0.7f, 0.2f, 0.1f, 1f));
        while (foldOuts.Count < airComps.Count)
            foldOuts.Add(false);
        for (int i = 0; i < airComps.Count; i++)
        {
            if (!foldOuts[i])
                continue;

            var item = airComps[i];
            var bounds = item.Bounds.ToUnityBounds();

            bounds.center = Handles.PositionHandle(bounds.center, Quaternion.identity);

            item.Bounds = bounds.ToNewtonsBounds2D();
            airComps[i] = item;

            Color c = physicsWorld.airCompositionColors[i];
            Handles.DrawSolidRectangleWithOutline(new Rect(bounds.center - bounds.extents, bounds.size), new Color(c.r, c.g, c.b, 0.15f), new Color(c.r, c.g, c.b, 0.9f));
        }
    }
}
