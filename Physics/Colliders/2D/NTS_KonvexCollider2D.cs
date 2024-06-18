using UnityEngine;
using System;
using NEWTONS.Core._2D;

public class NTS_KonvexCollider2D : NTS_Collider2D
{
    private KonvexCollider2D _konvexCollider = new();

    public KonvexCollider2D KonvexCollider 
    { 
        get => _konvexCollider;
        private set => _konvexCollider = value;
    }

    public override NEWTONS.Core._2D.Collider2D BaseCollider => KonvexCollider;

    public UnityEngine.Vector2 Size { get => KonvexCollider.Size.ToUnityVector(); set => KonvexCollider.Size = value.ToNewtonsVector(); }
    
    public UnityEngine.Vector2 ScaledSize => KonvexCollider.ScaledSize.ToUnityVector();

    public UnityEngine.Vector2[] Points => KonvexCollider.Points.ToUnityVectorArray();

    public UnityEngine.Vector2[] PointsRaw { get => KonvexCollider.PointsRaw.ToUnityVectorArray(); set => KonvexCollider.PointsRaw = value.ToNewtonsVectorArray(); }

    #region Serialization

    [System.Serializable]
    private struct SerializerKonvexCollider2D
    {
        public Vector2 size;
        public Vector2[] pointsRaw;
    }

    [SerializeField, HideInInspector]
    private SerializerKonvexCollider2D _serializerKonvexCollider;

    public override void OnBeforeSerialize()
    {
        base.OnBeforeSerialize();
        _serializerKonvexCollider = new()
        {
            size = Size,
            pointsRaw = PointsRaw
        };
    }

    public override void OnAfterDeserialize()
    {
        base.OnAfterDeserialize();
        Size = _serializerKonvexCollider.size;
        PointsRaw = _serializerKonvexCollider.pointsRaw;
    }

    #endregion
}
