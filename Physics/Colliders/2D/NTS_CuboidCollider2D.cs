using NEWTONS.Core._2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTS_CuboidCollider2D : NTS_Collider2D
{

    [SerializeField, HideInInspector]
    private CuboidCollider2D _konvexCollider;

    public CuboidCollider2D CuboidCollider { get => _konvexCollider; private set => _konvexCollider = value; }


    public override NTS_Rigidbody2D Body { get; protected set; }

    public override UnityEngine.Vector2 Center { get => CuboidCollider.Center.ToUnityVector(); set => CuboidCollider.Center = value.ToNewtonsVector(); }

    public override UnityEngine.Vector2 GlobalCenter => Center + Body.Position;

    public override UnityEngine.Vector2 Scale { get => CuboidCollider.Scale.ToUnityVector(); set => CuboidCollider.Scale = value.ToNewtonsVector(); }

    public override UnityEngine.Vector2 ScaleNoNotify { set => CuboidCollider.ScaleNoNotify = value.ToNewtonsVector(); }

    public override float Rotation { get => CuboidCollider.Rotation; }


    public UnityEngine.Vector2 Size { get => CuboidCollider.Size.ToUnityVector(); set => CuboidCollider.Size = value.ToNewtonsVector(); }

    public UnityEngine.Vector2 ScaledSize => CuboidCollider.ScaledSize.ToUnityVector();

    public UnityEngine.Vector2[] Points => CuboidCollider.Points.ToUnityVectorArray();

    public UnityEngine.Vector2[] PointsRaw => CuboidCollider.PointsRaw.ToUnityVectorArray();

    private void Awake()
    {
        Body = GetComponent<NTS_Rigidbody2D>();
        CuboidCollider.Body = Body.Body;
        Body.Body.collider = CuboidCollider;
        CuboidCollider.OnUpdateScale += OnUpdateNEWTONSScale;

        CuboidCollider.AddToPhysicsEngine();
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
        CuboidCollider = null;
        Destroy(this);
    }

    public override IColliderReference2D SetCollider(NEWTONS.Core._2D.Collider2D collider)
    {
        CuboidCollider = collider as CuboidCollider2D;
        if (CuboidCollider == null)
            throw new ArgumentException("Collider must be of type CuboidCollider2D");
        return this;
    }

#if UNITY_EDITOR
    public void Validate()
    {
        try
        {
            NTS_Rigidbody2D kBody = GetComponent<NTS_Rigidbody2D>();
            if (CuboidCollider.Body != kBody.Body || Body != kBody)
            {
                CuboidCollider.Body = kBody.Body;
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
