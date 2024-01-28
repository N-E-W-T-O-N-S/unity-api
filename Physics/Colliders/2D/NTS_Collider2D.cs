using NEWTONS.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KinematicBody2D), typeof(TransformConnector))]
public abstract class NTS_Collider2D : MonoBehaviour, IColliderReference2D
{
    protected TransformConnector _transformConnector;

    public abstract UnityEngine.Vector2 Scale { get; set; }

    public abstract UnityEngine.Vector2 GlobalScale { get; set; }

    public abstract float Rotation { get; }

    public abstract UnityEngine.Vector2 Center { get; set; }

    protected void UpdateNEWTONSGlobalScale()
    {
        GlobalScale = transform.lossyScale;
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
