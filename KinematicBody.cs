using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core;
using System;

public class KinematicBody : MonoBehaviour
{
    public WeakReference<NEWTONS.Core.KinematicBody> body;
    [SerializeField]
    private bool useGravity = false;
    [SerializeField]
    private UnityEngine.Vector3 initVelocity;

    public bool UseGravity
    {
        get => GetPhysicsBody().UseGravity;
        set
        {
            if (!body.TryGetTarget(out NEWTONS.Core.KinematicBody b))
                throw new Exception("No KinematicBody");
            if (b.UseGravity != value)
                b.UseGravity = value;
        }
    }

    public UnityEngine.Vector3 Velocity
    {
        get => GetPhysicsBody().Velocity.ToUnityVector();
        set
        {
            if (!body.TryGetTarget(out NEWTONS.Core.KinematicBody b))
                throw new Exception("No KinematicBody");
            if (b.Velocity != value.ToNewtonsVector())
                b.Velocity = value.ToNewtonsVector();
        }
    }

    public UnityEngine.Vector3 Position
    {
        get => GetPhysicsBody().Position.ToUnityVector();
        set
        {
            if (!body.TryGetTarget(out NEWTONS.Core.KinematicBody b))
                throw new Exception("No KinematicBody");
            if (b.Position != value.ToNewtonsVector())
                b.Position = value.ToNewtonsVector();
        }
    }

    private void Awake()
    {
        NEWTONS.Core.KinematicBody physicsBody = new NEWTONS.Core.KinematicBody()
        {
            Position = transform.position.ToNewtonsVector(),
            UseGravity = useGravity,
            Velocity = initVelocity.ToNewtonsVector(),
        };
        physicsBody.UpdatePosition += UpdateTransformPosition;
        body = new WeakReference<NEWTONS.Core.KinematicBody>(physicsBody);
        PhysicsWorld.tests.Add(this);
    }

    //Experimental
    public NEWTONS.Core.KinematicBody GetPhysicsBody()
    {
        if (!body.TryGetTarget(out NEWTONS.Core.KinematicBody target))
        {
            Debug.LogWarning("No Physics Body Attached to the KinematicBody, KinematicBody will be Destroyed");
            Destroy(gameObject);
            return null;
        }
        return target;
    }

    private void OnDestroy()
    {
        PhysicsWorld.DestroyBody(this);
    }

    private void UpdateTransformPosition()
    {
        transform.position = GetPhysicsBody().Position.ToUnityVector();
    }

    //Needs own Quaternion implementation
    private void UpdateTransformRotation()
    {
        //transform.rotation = GetPhysicsBody().Rotation;
    }
}