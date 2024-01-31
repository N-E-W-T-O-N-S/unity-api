using NEWTONS.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NTS_Rigidbody2D), typeof(TransformConnector))]
public abstract class NTS_Collider2D : MonoBehaviour, IColliderReference2D
{
    protected TransformConnector _transformConnector;

    /// <summary>
    /// KinematicBody2D attached to the collider
    /// </summary>
    public abstract NTS_Rigidbody2D Body { get; protected set; }

    /// <summary>
    /// the local center of the collider
    /// </summary>
    public abstract UnityEngine.Vector2 Center { get; set; }

    public abstract UnityEngine.Vector2 GlobalCenter { get; }

    public abstract float Rotation { get; }

    public abstract UnityEngine.Vector2 Size { get; set; }

    /// <summary>
    /// lossy scale of the colliser
    /// </summary>
    public abstract UnityEngine.Vector2 Scale { get; set; }

    public abstract UnityEngine.Vector2 ScaledSize { get; }

    public abstract UnityEngine.Vector2 ScaleNoNotify { set; }

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

    public virtual IColliderReference2D SetCollider(NEWTONS.Core.Collider2D collider)
    {
        throw new System.NotImplementedException();
    }
}
