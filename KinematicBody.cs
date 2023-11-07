using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core;
using System;

[RequireComponent(typeof(TransformConnector))]
public class KinematicBody : MonoBehaviour, NEWTONS.Core.IKinematicBodyReference
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core.KinematicBody _body;

    /// <summary>
    /// Direct access to the Physics Engine's KinematicBody
    /// <inheritdoc cref="NEWTONS.Core.KinematicBody"/>
    /// <para><u><b>WARNING:</b></u> <b>Do not directly use to change properties</b></para>
    /// </summary>
    public NEWTONS.Core.KinematicBody Body { get => _body; set { _body = value; } }

    public UnityEngine.Vector3 Position
    {
        get => Body.Position.ToUnityVector();
        set => Body.Position = value.ToNewtonsVector();
    }

    public UnityEngine.Vector3 PositionNoNotify 
    { 
        get => _body.PositionNoNotify.ToUnityVector();
        set => _body.PositionNoNotify = value.ToNewtonsVector(); 
    }

    public bool UseGravity
    {
        get => Body.UseGravity;
        set => Body.UseGravity = value;
    }


    public UnityEngine.Vector3 Velocity
    {
        get => Body.Velocity.ToUnityVector();
        set => Body.Velocity = value.ToNewtonsVector();
    }

    public float Mass
    {
        get => Body.Mass;
        set => Body.Mass = value;
    }

    public float Drag
    {
        get => Body.Drag;
        set => Body.Drag = value;
    }

    private void Awake()
    {
        PhysicsWorld.tests.Add(this);
        Body.OnUpdatePosition += UpdateTransformPosition;
    }

    public void AddForce(UnityEngine.Vector3 force, NEWTONS.Core.ForceMode forceMode)
    {
        Body?.AddForce(force.ToNewtonsVector(), forceMode, Time.fixedDeltaTime);
    }

    private void UpdateTransformPosition()
    {
        transform.position = Position;
    }
    
    //Needs own Quaternion implementation
    private void UpdateTransformRotation()
    {
        //transform.rotation = GetPhysicsBody().Rotation;
    }

    private void OnDestroy()
    {
        PhysicsWorld.DestroyBody(this);
    }

    public IKinematicBodyReference SetKinematicBody(NEWTONS.Core.KinematicBody kinematicBody)
    {
        Body = kinematicBody;
        return this;
    }

    public void Dispose()
    {
        Body = null;
        Destroy(this);
    }


#if UNITY_EDITOR
    public bool foldOutInfo = false;
#endif
}