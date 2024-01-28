using NEWTONS.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboidCollider : NTS_Collider
{
    // INFO: Debug
    // <------------------------->
    [HideInInspector]
    public DebugManager debugManager;
    // <------------------------->



    [SerializeField, HideInInspector]
    private NEWTONS.Core.CuboidCollider _cuboidColl;

    public NEWTONS.Core.CuboidCollider CuboidColl { get => _cuboidColl; set { _cuboidColl = value; } }

    public override UnityEngine.Vector3 Scale
    {
        get => CuboidColl.Scale.ToUnityVector();
        set => CuboidColl.Scale = value.ToNewtonsVector();
    }

    public override UnityEngine.Vector3 GlobalScale
    {
        get => CuboidColl.GlobalScales.ToUnityVector();
        set => CuboidColl.GlobalScales = value.ToNewtonsVector();
    }

    public override UnityEngine.Quaternion Rotation
    {
        get => CuboidColl.Body.Rotation.ToUnityQuaternion();
    }

    public override UnityEngine.Vector3 Center
    {
        get => CuboidColl.Center.ToUnityVector();
        set => CuboidColl.Center = value.ToNewtonsVector();
    }

    public int[] Indices
    {
        get => CuboidColl.Indices;
        set => CuboidColl.Indices = value;
    }

    public UnityEngine.Vector3[] PointsRaw
    {
        get => CuboidColl.Points.ToUnityVectorArray();
        set => CuboidColl.PointsRaw = value.ToNewtonsVectorArray();
    }

    public UnityEngine.Vector3[] Points
    {
        get => CuboidColl.Points.ToUnityVectorArray();
    }

    public float Restitution
    {
        get => CuboidColl.Restitution;
        set => CuboidColl.Restitution = value;
    }

    private void Awake()
    {
        CuboidColl.Body = GetComponent<KinematicBody>().Body;
        PhysicsWorld.colltest.Add(this);
        CuboidColl.AddToPhysicsEngine();
    }

    private void OnValidate()
    {
        _transformConnector = GetComponent<TransformConnector>();
        _transformConnector.OnScaleChanged += UpdateNEWTONSGlobalScale;
    }

    public override void Dispose()
    {
        CuboidColl = null;
        Destroy(this);
    }

    public override IColliderReference SetCollider(NEWTONS.Core.Collider collider)
    {
        CuboidColl = collider as NEWTONS.Core.CuboidCollider;
        if (CuboidColl == null)
            throw new ArgumentException("Collider must be of type CuboidCollider");
        return this;
    }

#if UNITY_EDITOR
    public void Validate()
    {
        try
        {
            NEWTONS.Core.KinematicBody b = GetComponent<KinematicBody>().Body;
            if (CuboidColl.Body == null || CuboidColl.Body != b)
                CuboidColl.Body = b;
        }
        catch
        {
            debugManager.LogError("CuboidCollider: " + name + " is missing a KinematicBody component");
        }
    }

    public bool foldOutDebugManager = false;

#endif

}
