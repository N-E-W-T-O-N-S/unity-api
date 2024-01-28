using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core;
using System;

public class KonvexCollider2D : NTS_Collider2D
{

    [SerializeField, HideInInspector]
    private NEWTONS.Core.KonvexCollider2D _konvexCollider;

    public NEWTONS.Core.KonvexCollider2D KonvexCollider { get => _konvexCollider; set => _konvexCollider = value; }

    public DebugManager debugManager;

    public override UnityEngine.Vector2 Scale
    {
        get => KonvexCollider.Scale.ToUnityVector();
        set => KonvexCollider.Scale = value.ToNewtonsVector();
    }

    public override UnityEngine.Vector2 GlobalScale
    {
        get => KonvexCollider.GlobalScales.ToUnityVector();
        set => KonvexCollider.GlobalScales = value.ToNewtonsVector();
    }

    public override float Rotation
    {
        get => KonvexCollider.Body.Rotation;
    }


    public override UnityEngine.Vector2 Center
    {
        get => KonvexCollider.Center.ToUnityVector();
        set => KonvexCollider.Center = value.ToNewtonsVector();
    }

    public UnityEngine.Vector2[] Points
    {
        get => KonvexCollider.Points.ToUnityVectorArray();
    }

    public UnityEngine.Vector2[] PointsRaw
    {
        get => KonvexCollider.PointsRaw.ToUnityVectorArray();
        set { KonvexCollider.PointsRaw = value.ToNewtonsVectorArray(); }
    }

    [Obsolete("Use Points instead")]
    public UnityEngine.Vector2[] ScaledPoints
    {
        get => KonvexCollider.ScaledPoints.ToUnityVectorArray();
    }

    private void Awake()
    {
        KonvexCollider.Body = GetComponent<KinematicBody2D>().Body;
        PhysicsWorld2D.colltest.Add(this);
        KonvexCollider.AddToPhysicsEngine();
    }

    private void OnValidate()
    {
        _transformConnector = GetComponent<TransformConnector>();
        _transformConnector.OnScaleChanged += UpdateNEWTONSGlobalScale;
    }

    public override void Dispose()
    {
        KonvexCollider = null;
        Destroy(this);
    }

    public override IColliderReference2D SetCollider(NEWTONS.Core.Collider2D collider)
    {
        KonvexCollider = collider as NEWTONS.Core.KonvexCollider2D;
        if (KonvexCollider == null)
            throw new ArgumentException("Collider must be of type CuboidCollider");
        return this;
    }

#if UNITY_EDITOR
    public void Validate() 
    {
        try
        {
            NEWTONS.Core.KinematicBody2D b = GetComponent<KinematicBody2D>().Body;
            if (KonvexCollider.Body == null || KonvexCollider.Body != b)
                KonvexCollider.Body = b;
        }
        catch
        {
            debugManager.LogError("KonvexCollider2D: " + name + " is missing a KinematicBody2D component");
        }
    }

    public bool foldOutDebugManager = false;

#endif

}
