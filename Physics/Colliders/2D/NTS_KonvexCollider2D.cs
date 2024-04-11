using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NEWTONS.Core._2D;

public class NTS_KonvexCollider2D : NTS_Collider2D
{
    [SerializeField, HideInInspector]
    private KonvexCollider2D _konvexCollider;

    public KonvexCollider2D KonvexCollider { get => _konvexCollider; private set => _konvexCollider = value; }

    
    public override NTS_Rigidbody2D Body { get; protected set; }

    public override UnityEngine.Vector2 Center { get => KonvexCollider.Center.ToUnityVector(); set => KonvexCollider.Center = value.ToNewtonsVector(); }

    public override UnityEngine.Vector2 GlobalCenter => Center + Body.Position;

    public override UnityEngine.Vector2 Scale { get => KonvexCollider.Scale.ToUnityVector(); set => KonvexCollider.Scale = value.ToNewtonsVector(); }

    public override UnityEngine.Vector2 ScaleNoNotify { set => KonvexCollider.ScaleNoNotify = value.ToNewtonsVector(); }

    public override float Rotation { get => KonvexCollider.Rotation; }
    

    public UnityEngine.Vector2 Size { get => KonvexCollider.Size.ToUnityVector(); set => KonvexCollider.Size = value.ToNewtonsVector(); }
    
    public UnityEngine.Vector2 ScaledSize => KonvexCollider.ScaledSize.ToUnityVector();

    public UnityEngine.Vector2[] Points => KonvexCollider.Points.ToUnityVectorArray();

    public UnityEngine.Vector2[] PointsRaw { get => KonvexCollider.PointsRaw.ToUnityVectorArray(); set => KonvexCollider.PointsRaw = value.ToNewtonsVectorArray(); }

    [Obsolete("Use Points instead")]
    public UnityEngine.Vector2[] ScaledPoints => KonvexCollider.ScaledPoints.ToUnityVectorArray();

    private void Awake()
    {
        Body = GetComponent<NTS_Rigidbody2D>();
        KonvexCollider.Body = Body.Body;
        KonvexCollider.OnUpdateScale += OnUpdateNEWTONSScale;

        KonvexCollider.AddToPhysicsEngine();
    }

    private void OnUpdateNEWTONSScale()
    {
        UnityEngine.Vector3 loc = transform.localScale;
        UnityEngine.Vector3 los = transform.lossyScale;
        UnityEngine.Vector3 k = new UnityEngine.Vector3(los.x / loc.x, los.y / loc.y);
        transform.localScale = new UnityEngine.Vector3(Scale.x / k.x, Scale.y / k.y);

        // lossy = local * K
        // K = lossy / local
        // newLocal += newLossy - K
    }

    public override void Dispose()
    {
        KonvexCollider = null;
        Destroy(this);
    }

    public override IColliderReference2D SetCollider(NEWTONS.Core._2D.Collider2D collider)
    {
        KonvexCollider = collider as KonvexCollider2D;
        if (KonvexCollider == null)
            throw new ArgumentException("Collider must be of type CuboidCollider");
        return this;
    }

#if UNITY_EDITOR
    public void Validate() 
    {
        try
        {
            NTS_Rigidbody2D kBody = GetComponent<NTS_Rigidbody2D>();
            if (KonvexCollider.Body != kBody.Body || Body != kBody)
            {
                KonvexCollider.Body = kBody.Body;
                Body = kBody;
            }
        }
        catch
        {
            debugManager.LogError("KonvexCollider2D: " + name + " is missing a KinematicBody2D component");
        }
    }

    public bool foldOutDebugManager = false;

#endif

}
