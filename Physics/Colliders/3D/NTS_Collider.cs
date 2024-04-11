using NEWTONS.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NTS_Rigidbody), typeof(TransformConnector))]
public abstract class NTS_Collider : MonoBehaviour, NEWTONS.Core._3D.IColliderReference
{
    protected TransformConnector _transformConnector;

    // INFO: Debug
    // <------------------------->
    [HideInInspector]
    public NTS_DebugManager debugManager;
    // <------------------------->

    /// <summary>
    /// KinematicBody attached to the collider
    /// </summary>
    public abstract NTS_Rigidbody Body { get; protected set; }

    /// <summary>
    /// local center of the collider
    /// </summary>
    public abstract UnityEngine.Vector3 Center { get; set; }

    public abstract UnityEngine.Vector3 GlobalCenter { get; }

    public abstract UnityEngine.Quaternion Rotation { get; }

    /// <summary>
    /// lossy scale of the collider
    /// </summary>
    public abstract UnityEngine.Vector3 Scale { get; set; }

    public abstract UnityEngine.Vector3 ScaleNoNotify { set; }

    public abstract float Restitution { get; set; }

    private void OnValidate()
    {
        _transformConnector = GetComponent<TransformConnector>();
        _transformConnector.OnScaleChanged += OnTransformScaleChange;
    }

    protected void OnTransformScaleChange()
    {
        ScaleNoNotify = transform.lossyScale;
    }

    public virtual void Dispose()
    {
        throw new System.NotImplementedException();
    }

    public virtual NEWTONS.Core._3D.IColliderReference SetCollider(NEWTONS.Core._3D.Collider collider)
    {
        throw new System.NotImplementedException();
    }
}
