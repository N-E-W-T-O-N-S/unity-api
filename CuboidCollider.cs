using NEWTONS.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KinematicBody))]
public class CuboidCollider : MonoBehaviour, IColliderReference
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core.CuboidCollider _cuboidColl;

    public NEWTONS.Core.CuboidCollider CuboidColl { get => _cuboidColl; set { _cuboidColl = value; } }

    public UnityEngine.Vector3 Scale
    {
        get => CuboidColl.Scale.ToUnityVector();
        set
        {
            CuboidColl.Scale = value.ToNewtonsVector();
        }
    }


    public UnityEngine.Vector3 Center
    {
        get => CuboidColl.Center.ToUnityVector();
        set
        {
            CuboidColl.Center = value.ToNewtonsVector();
        }
    }

    public UnityEngine.Vector3[] Points 
    { 
        get => CuboidColl.Points.ToUnityVectorArray(); 
        set { CuboidColl.Points = value.ToNewtonsVectorArray(); } }


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

    private void Awake()
    {
        CuboidColl.Body = GetComponent<KinematicBody>().Body;
        PhysicsWorld.colltest.Add(this);
    }
}
