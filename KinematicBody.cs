using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core;
using System;
using System.Data.Common;
using Unity.VisualScripting;

public class KinematicBody : MonoBehaviour
{
    private WeakReference<NEWTONS.Core.KinematicBody> _bodyRef;

    #region Internal NEWTONS fields
    /// <summary>
    /// Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this method directly. Insteade use <see cref="UseGravity"/> to alter gravity.
    /// </remarks>
    public bool initialUseGravity = false;
    /// <summary>
    /// Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this method directly. Insteade use <see cref="Velocity"/> to alter the velocity.
    /// </remarks>
    public UnityEngine.Vector3 initialVelocity;
    /// <summary>
    /// Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this method directly. Insteade use <see cref="Mass"/> to alter the mass.
    /// </remarks>
    public float initialMass = 1f;
    /// <summary>
    /// Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this method directly. Insteade use <see cref="Drag"/> to alter the drag.
    /// </remarks>
    public float initialDrag = 0f;
    #endregion

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

    public float Drag
    {
        get => GetPhysicsBody().Drag;
        set
        {
            NEWTONS.Core.KinematicBody b = GetPhysicsBody();
            if (b != null)
                b.Drag = value;
        }
    }


    private void Awake()
    {
        NEWTONS.Core.KinematicBody physicsBody = new NEWTONS.Core.KinematicBody()
        {
            Position = transform.position.ToNewtonsVector(),
            UseGravity = initialUseGravity,
            Velocity = initialVelocity.ToNewtonsVector(),
            Mass = initialMass,
            Drag = initialDrag
        };
        physicsBody.OnUpdatePosition += UpdateTransformPosition;
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

    public WeakReference<NEWTONS.Core.KinematicBody> GetPhysicsBodyRef()
    {
        return _bodyRef;
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