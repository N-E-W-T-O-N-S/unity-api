using UnityEngine;
using System;
using NEWTONS.Core._2D;

public class NTS_KonvexCollider2D : NTS_Collider2D
{
    [SerializeField, HideInInspector]
    private KonvexCollider2D _konvexCollider;

    public KonvexCollider2D KonvexCollider 
    { 
        get => _konvexCollider;
        private set => _konvexCollider = value;
    }

    protected override NEWTONS.Core._2D.Collider2D BaseCollider => KonvexCollider;

    public override NTS_Rigidbody2D Body { get; protected set; }

    public override UnityEngine.Vector2 Center { get => KonvexCollider.Center.ToUnityVector(); set => KonvexCollider.Center = value.ToNewtonsVector(); }

    public override UnityEngine.Vector2 GlobalCenter => Center + Body.Position;

    public override UnityEngine.Vector2 Scale { get => KonvexCollider.Scale.ToUnityVector(); set => KonvexCollider.Scale = value.ToNewtonsVector(); }

    public override UnityEngine.Vector2 ScaleNoNotify { set => KonvexCollider.ScaleNoNotify = value.ToNewtonsVector(); }

    public override float Rotation { get => KonvexCollider.Rotation; }

    public override float Restitution { get => KonvexCollider.Restitution; set => KonvexCollider.Restitution = value; }


    public UnityEngine.Vector2 Size { get => KonvexCollider.Size.ToUnityVector(); set => KonvexCollider.Size = value.ToNewtonsVector(); }
    
    public UnityEngine.Vector2 ScaledSize => KonvexCollider.ScaledSize.ToUnityVector();

    public UnityEngine.Vector2[] Points => KonvexCollider.Points.ToUnityVectorArray();

    public UnityEngine.Vector2[] PointsRaw { get => KonvexCollider.PointsRaw.ToUnityVectorArray(); set => KonvexCollider.PointsRaw = value.ToNewtonsVectorArray(); }

    [Obsolete("Use Points instead")]
    public UnityEngine.Vector2[] ScaledPoints => KonvexCollider.ScaledPoints.ToUnityVectorArray();

#if UNITY_EDITOR
    public bool foldOutDebugManager = false;
#endif

}
