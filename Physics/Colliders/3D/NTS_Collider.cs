using NEWTONS.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KinematicBody), typeof(TransformConnector))]
public abstract class NTS_Collider : MonoBehaviour, IColliderReference
{
    protected TransformConnector _transformConnector;

    public abstract UnityEngine.Vector3 Scale { get; set; }

    public abstract UnityEngine.Vector3 GlobalScale { get; set; }

    public abstract UnityEngine.Quaternion Rotation { get; }

    public abstract UnityEngine.Vector3 Center { get; set; }
    
    protected void UpdateNEWTONSGlobalScale()
    {
        GlobalScale = transform.lossyScale;
    }

    public virtual void Dispose()
    {
        throw new System.NotImplementedException();
    }

    public virtual IColliderReference SetCollider(NEWTONS.Core.Collider collider)
    {
        throw new System.NotImplementedException();
    }
}
