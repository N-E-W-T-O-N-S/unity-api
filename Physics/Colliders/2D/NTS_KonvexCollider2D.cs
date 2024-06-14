using UnityEngine;
using System;
using NEWTONS.Core._2D;

public class NTS_KonvexCollider2D : NTS_Collider2D
{
    [SerializeField, HideInInspector]
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
}
