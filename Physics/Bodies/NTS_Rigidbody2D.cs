using NEWTONS.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TransformConnector))]
public class NTS_Rigidbody2D : MonoBehaviour, NEWTONS.Core._2D.IRigidbodyReference2D
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core._2D.Rigidbody2D _body;

    /// <summary>
    /// Direct access to the Physics Engine's KinematicBody
    /// <inheritdoc cref="NEWTONS.Core.KinematicBody2D"/>
    /// <para><u><b>WARNING:</b></u> <b>Do not directly use to change properties</b></para>
    /// </summary>
    public NEWTONS.Core._2D.Rigidbody2D Body { get => _body; private set => _body = value; }

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
        set => _body.PositionNoNotify = value.ToNewtonsVector();
    }

    public float Rotation
    {
        get => _body.Rotation;
        set => _body.Rotation = value;
    }

    public float RotationNoNotify
    {
        set => _body.RotationNoNotify = value;
    }

    public bool UseGravity
    {
        get => Body.UseGravity;
        set => Body.UseGravity = value;
    }

    public float Inertia => Body.Inertia;

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
        Body.OnUpdatePosition += OnUpdateNEWTONSPosition;
        Body.OnUpdateRotation += OnUpdateNEWTOSRotation;
        Body.AddToPhysicsEngine();
    }

    private void OnValidate()
    {
        _transformConnector = GetComponent<TransformConnector>();
        _transformConnector.OnPositionChanged += OnTransformPositionChange;
        _transformConnector.OnRotationChanged += OnTransformRotationChange;
    }

    public void AddForce(UnityEngine.Vector2 force, NEWTONS.Core.ForceMode forceMode)
    {
        Body?.AddForce(force.ToNewtonsVector(), forceMode);
    }

    private void OnUpdateNEWTONSPosition()
    {
        transform.position = Position;
    }

    private void OnUpdateNEWTOSRotation()
    {
        UnityEngine.Vector3 rot = transform.rotation.eulerAngles;
        transform.rotation = UnityEngine.Quaternion.Euler(rot.x, rot.y, Rotation);
    }

    private void OnTransformPositionChange()
    {
        PositionNoNotify = transform.position;
    }

    private void OnTransformRotationChange()
    {
        RotationNoNotify = transform.rotation.eulerAngles.z;
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

    public NEWTONS.Core._2D.IRigidbodyReference2D SetRigidbody(NEWTONS.Core._2D.Rigidbody2D kinematicBody)
    {
        Body = kinematicBody;
        return this;
    }


#if UNITY_EDITOR
    public bool foldOutInfo = false;
#endif
}
