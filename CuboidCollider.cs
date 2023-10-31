using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KinematicBody))]
public class CuboidCollider : MonoBehaviour
{
    private WeakReference<NEWTONS.Core.CuboidCollider> _cuboidRef;
    public Vector3 initialScale = Vector3.one;
    public Vector3 initialCenter = Vector3.zero;

    public Vector3 Scale
    {
        get => GetCuboidCollider().Scale.ToUnityVector();
        set
        {
            GetCuboidCollider().Scale = value.ToNewtonsVector();
        }
    }

    public NEWTONS.Core.KinematicBody Body
    {
        get => GetCuboidCollider().KinematicBody;
        set
        {
            GetCuboidCollider().KinematicBody = value;
        }
    }

    public Vector3 Center
    {
        get => GetCuboidCollider().Center.ToUnityVector();
        set
        {
            GetCuboidCollider().Center = value.ToNewtonsVector();
        }
    }


    private void Awake()
    {
        _cuboidRef = new WeakReference<NEWTONS.Core.CuboidCollider>
        (
            new NEWTONS.Core.CuboidCollider
            (
                initialScale.ToNewtonsVector(),
                GetComponent<KinematicBody>().GetPhysicsBody(),
                initialCenter.ToNewtonsVector()
            )
        );
        PhysicsWorld.colltest.Add(this);
    }

    /// <summary>
    /// Gets the Collider attached to the CuboidCollider if it exists, destroys the CuboidCollider if it doesn't
    /// </summary>
    /// <returns>CuboidCollider, null if not</returns>
    public NEWTONS.Core.CuboidCollider GetCuboidCollider()
    {
        if (!_cuboidRef.TryGetTarget(out NEWTONS.Core.CuboidCollider target))
        {
            Debug.LogWarning("No Cuboid Collider Attached to the CuboidCollider, gameObject will be Destroyed");
            Destroy(gameObject);
            return null;
        }
        return target;
    }

    public WeakReference<NEWTONS.Core.CuboidCollider> GetCuboidColliderRef()
    {
        return _cuboidRef;
    }
}
