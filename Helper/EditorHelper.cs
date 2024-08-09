#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public static class EditorHelper
{
    public static void DrawBounds(NTS_Collider2D coll)
    {
        var bounds = coll.Bounds;
        var min = bounds.Min.ToUnityVector();
        var max = bounds.Max.ToUnityVector();

        Handles.color = Color.red;
        Handles.DrawLine(new Vector2(min.x, min.y), new Vector2(min.x, max.y));
        Handles.DrawLine(new Vector2(min.x, max.y), new Vector2(max.x, max.y));
        Handles.DrawLine(new Vector2(max.x, max.y), new Vector2(max.x, min.y));
        Handles.DrawLine(new Vector2(max.x, min.y), new Vector2(min.x, min.y));
    }

    public static Bounds Bounds2DField(string label, Bounds bounds)
    {
        bool foldout = true;
        foldout = EditorGUILayout.Foldout(foldout, label);
        if (foldout)
        {
            EditorGUI.indentLevel++;
            bounds.min = EditorGUILayout.Vector2Field("Min", bounds.min);
            bounds.max = EditorGUILayout.Vector2Field("Max", bounds.max);
            EditorGUI.indentLevel--;
        }
        return bounds;
    }
}
#endif