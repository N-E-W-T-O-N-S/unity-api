using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core;
using System;

[RequireComponent(typeof(TransformConnector))]
public class NTS_Rigidbody : MonoBehaviour, NEWTONS.Core.IRigidbodyReference
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core.Rigidbody _body;

    /// <summary>
    /// Direct access to the Physics Engine's Rigidbody
    /// <inheritdoc cref="NEWTONS.Core.Rigidbody"/>
    /// <para><u><b>WARNING:</b></u> <b>Do not directly use to change properties</b></para>
    /// </summary>
    public NEWTONS.Core.Rigidbody Body { get => _body; private set => _body = value; }

    private TransformConnector _transformConnector;

    public bool IsStatic { get => Body.IsStatic; set => Body.IsStatic = value; }
    
    public UnityEngine.Vector3 Position { get => Body.Position.ToUnityVector(); set => Body.Position = value.ToNewtonsVector(); }

    public UnityEngine.Vector3 PositionNoNotify { set => Body.PositionNoNotify = value.ToNewtonsVector(); }
    
    public UnityEngine.Quaternion Rotation { get => Body.Rotation.ToUnityQuaternion(); set => Body.Rotation = value.ToNewtonsQuaternion(); }

    public UnityEngine.Quaternion RotationNoNotify { set => Body.RotationNoNotify = value.ToNewtonsQuaternion(); }

    public bool UseGravity { get => Body.UseGravity; set => Body.UseGravity = value; }

    public UnityEngine.Vector3 Velocity { get => Body.Velocity.ToUnityVector(); set => Body.Velocity = value.ToNewtonsVector(); }

    public float Mass { get => Body.Mass; set => Body.Mass = value; }

    public float Drag { get => Body.Drag; set => Body.Drag = value; }

    private void Awake()
    {
        Body.OnUpdatePosition += OnUpdateNEWTONSPosition;
        Body.OnUpdateRotation += OnUpdateNEWTONSRotation;
        Body.AddToPhysicsEngine();
    }

    private void OnValidate()
    {
        _transformConnector = GetComponent<TransformConnector>();
        _transformConnector.OnPositionChanged += OnTransformPositionChange;
        _transformConnector.OnRotationChanged += OnTransformRotationChange;
    }

    public void AddForce(UnityEngine.Vector3 force, NEWTONS.Core.ForceMode forceMode)
    {
        Body?.AddForce(force.ToNewtonsVector(), forceMode, Time.fixedDeltaTime);
    }

    private void OnTransformPositionChange() => PositionNoNotify = transform.position;

    private void OnTransformRotationChange() => RotationNoNotify = transform.rotation;

    /// <summary>
    /// Updates the position of the KinematicBody in the Physics Engine without notifying Unity
    /// </summary>
    private void OnUpdateNEWTONSPosition() => transform.position = Position;

    private void OnUpdateNEWTONSRotation() => transform.rotation = Rotation;

    private void OnDestroy()
    {
        NTS_PhysicsWorld.DestroyBody(this);
    }

    public IRigidbodyReference SetRigidbody(NEWTONS.Core.Rigidbody kinematicBody)
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