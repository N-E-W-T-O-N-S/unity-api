using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core;
using System;

public class KinematicBody : MonoBehaviour
{
    //TODO: serialze this
    private WeakReference<NEWTONS.Core.KinematicBody> _bodyRef;
    [SerializeField]
    private bool useGravity = false;
    [SerializeField]
    private UnityEngine.Vector3 initVelocity;

    public bool UseGravity
    {
        get => GetPhysicsBody().UseGravity;
        set
        {
            NEWTONS.Core.KinematicBody b = GetPhysicsBody();
            if (b != null)
                b.UseGravity = value;
        }
    }

    public UnityEngine.Vector3 Velocity
    {
        get => GetPhysicsBody().Velocity.ToUnityVector();
        set
        {
            NEWTONS.Core.KinematicBody b = GetPhysicsBody();
            if (b != null)
                b.Velocity = value.ToNewtonsVector();
        }
    }

    [SerializeField]
    private float mass = 1f;

    public float Mass
    {
        get => GetPhysicsBody().Mass;
        set 
        {
            NEWTONS.Core.KinematicBody b = GetPhysicsBody();
            if (b != null)
                b.Mass = value;
        }
    }

    public UnityEngine.Vector3 Position
    {
        get => GetPhysicsBody().Position.ToUnityVector();
        set
        {
            NEWTONS.Core.KinematicBody b = GetPhysicsBody();
            if (b != null)
                b.Position = value.ToNewtonsVector();
        }
    }

    private void Reset()
    {
        NEWTONS.Core.KinematicBody physicsBody = new NEWTONS.Core.KinematicBody()
        {
            Position = transform.position.ToNewtonsVector(),
            UseGravity = useGravity,
            Velocity = initVelocity.ToNewtonsVector(),
            Mass = mass,
        };
        physicsBody.UpdatePosition += UpdateTransformPosition;
        _bodyRef = new WeakReference<NEWTONS.Core.KinematicBody>(physicsBody);
        PhysicsWorld.tests.Add(this);
    }

    public void AddForce(UnityEngine.Vector3 force, NEWTONS.Core.ForceMode forceMode)
    {
        NEWTONS.Core.KinematicBody b = GetPhysicsBody();
        b?.AddForce(force.ToNewtonsVector(), forceMode, Time.fixedDeltaTime);
    }

    /// <summary>
    /// Gets the Physics Body attached to the KinematicBody if it exists, destroys the KinematicBody if it doesn't
    /// </summary>
    /// <returns>KineamBody, null if not</returns>
    public NEWTONS.Core.KinematicBody GetPhysicsBody()
    {
        if (!_bodyRef.TryGetTarget(out NEWTONS.Core.KinematicBody target))
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


#if UNITY_EDITOR
    public bool foldOutInfo = false;
#endif
}