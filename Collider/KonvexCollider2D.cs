using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core;
using System;

[RequireComponent(typeof(KinematicBody2D), typeof(TransformConnector))]
public class KonvexCollider2D : MonoBehaviour, IColliderReference2D
{

    [SerializeField, HideInInspector]
    private NEWTONS.Core.KonvexCollider2D _konvexCollider;

    public NEWTONS.Core.KonvexCollider2D KonvexCollider { get => _konvexCollider; set { _konvexCollider = value; } }

    private TransformConnector _transformConnector;

    public UnityEngine.Vector2 Scale
    {
        get => KonvexCollider.Scale.ToUnityVector();
        set => KonvexCollider.Scale = value.ToNewtonsVector();
    }

    public UnityEngine.Vector2 GlobalScale
    {
        get => KonvexCollider.GlobalScales.ToUnityVector();
        set => KonvexCollider.GlobalScales = value.ToNewtonsVector();
    }

    public float Rotation
    {
        get => KonvexCollider.Body.Rotation;
    }


    public UnityEngine.Vector2 Center
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
        try
        {
            KonvexCollider.Body = GetComponent<KinematicBody2D>().Body;
            _transformConnector = GetComponent<TransformConnector>();
        }
        catch { }
        _transformConnector.OnScaleChanged += UpdateNEWTONSGlobalScale;
    }

    private void UpdateNEWTONSGlobalScale()
    {
        GlobalScale = transform.lossyScale;
    }

    public void Dispose()
    {
        KonvexCollider = null;
        Destroy(this);
    }

    public IColliderReference2D SetCollider(NEWTONS.Core.Collider2D collider)
    {
        KonvexCollider = collider as NEWTONS.Core.KonvexCollider2D;
        if (KonvexCollider == null)
            throw new ArgumentException("Collider must be of type CuboidCollider");
        return this;
    }
}
