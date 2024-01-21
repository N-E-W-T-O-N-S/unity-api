using NEWTONS.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KinematicBody), typeof(TransformConnector))]
public class CuboidCollider : MonoBehaviour, IColliderReference
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core.CuboidCollider _cuboidColl;

    public NEWTONS.Core.CuboidCollider CuboidColl { get => _cuboidColl; set { _cuboidColl = value; } }

    private TransformConnector _transformConnector;

    public UnityEngine.Vector3 Scale
    {
        get => CuboidColl.Scale.ToUnityVector();
        set
        {
            CuboidColl.Scale = value.ToNewtonsVector();
        }
    }

    public UnityEngine.Vector3 GlobalScale 
    { 
        get => CuboidColl.GlobalScales.ToUnityVector(); 
        set => CuboidColl.GlobalScales = value.ToNewtonsVector(); 
    }

    public UnityEngine.Quaternion Rotation
    {
        get => CuboidColl.Rotation.ToUnityQuaternion();
        set => CuboidColl.Rotation = value.ToNewtonsQuaternion();
    }

    public UnityEngine.Vector3 Center
    {
        get => CuboidColl.Center.ToUnityVector();
        set
        {
            CuboidColl.Center = value.ToNewtonsVector();
        }
    }

    public UnityEngine.Vector3[] PointsRaw
    {
        get => CuboidColl.Points.ToUnityVectorArray();
        set { CuboidColl.PointsRaw = value.ToNewtonsVectorArray(); }
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
        _transformConnector.OnRotationChanged += UpdateNEWTONSRotation;
    }

    private void UpdateNEWTONSRotation()
    {
        Rotation = transform.rotation;
    }

    private void UpdateNEWTONSGlobalScale()
    {
        GlobalScale = transform.lossyScale;
    }

    public void Dispose()
    {
        CuboidColl = null;
        Destroy(this);
    }

    public IColliderReference SetCollider(NEWTONS.Core.Collider collider)
    {
        CuboidColl = collider as NEWTONS.Core.CuboidCollider;
        if (CuboidColl == null)
            throw new ArgumentException("Collider must be of type CuboidCollider");
        return this;
    }
}
