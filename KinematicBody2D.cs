using NEWTONS.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TransformConnector))]
public class KinematicBody2D : MonoBehaviour, IKinematicBodyReference2D
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core.KinematicBody2D _body;

    /// <summary>
    /// Direct access to the Physics Engine's KinematicBody
    /// <inheritdoc cref="NEWTONS.Core.KinematicBody2D"/>
    /// <para><u><b>WARNING:</b></u> <b>Do not directly use to change properties</b></para>
    /// </summary>
    public NEWTONS.Core.KinematicBody2D Body { get => _body; set { _body = value; } }

    private TransformConnector _transformConnector;

    public bool IsStatic
    {
        get => Body.IsStatic;
        set => Body.IsStatic = value;
    }

    public UnityEngine.Vector2 Position
    {
        get => Body.Position.ToUnityVector();
        set => Body.Position = value.ToNewtonsVector();
    }

    public UnityEngine.Vector2 PositionNoNotify
    {
        get => _body.PositionNoNotify.ToUnityVector();
        set => _body.PositionNoNotify = value.ToNewtonsVector();
    }

    public bool UseGravity
    {
        get => Body.UseGravity;
        set => Body.UseGravity = value;
    }


    public UnityEngine.Vector2 Velocity
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
        PhysicsWorld2D.tests.Add(this);
        Body.OnUpdatePosition += UpdateTransformPosition;
        Body.AddToPhysicsEngine();
    }

    private void OnValidate()
    {
        _transformConnector = GetComponent<TransformConnector>();
        _transformConnector.OnPositionChanged += UpdateNEWTONSPosition;
    }

    private void UpdateNEWTONSPosition()
    {
        PositionNoNotify = transform.position;
    }

    public void AddForce(UnityEngine.Vector2 force, NEWTONS.Core.ForceMode forceMode)
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
        //PhysicsWorld.DestroyBody(this);
    }

    public void Dispose()
    {
        Body = null;
        Destroy(this);
    }

    public IKinematicBodyReference2D SetKinematicBody(NEWTONS.Core.KinematicBody2D kinematicBody)
    {
        Body = kinematicBody;
        return this;
    }


#if UNITY_EDITOR
    public bool foldOutInfo = false;
#endif
}
