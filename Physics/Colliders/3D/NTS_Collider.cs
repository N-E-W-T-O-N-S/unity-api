using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(NTS_Rigidbody))]
public abstract class NTS_Collider : MonoBehaviour, NEWTONS.Core._3D.IColliderReference, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private NTS_Rigidbody _body;

    /// <summary>
    /// KinematicBody attached to the collider
    /// </summary>
    public NTS_Rigidbody Body { get => _body; protected set => _body = value; }

    public abstract NEWTONS.Core._3D.Collider BaseCollider { get; }

    /// <summary>
    /// the local center of the collider
    /// </summary>
    public UnityEngine.Vector3 Center
    {
        get => BaseCollider.Center.ToUnityVector();
        set => BaseCollider.Center = value.ToNewtonsVector();
    }

    public UnityEngine.Vector3 GlobalCenter => BaseCollider.GlobalCenter.ToUnityVector();

    public UnityEngine.Quaternion Rotation => BaseCollider.Rotation.ToUnityQuaternion();

    /// <summary>
    /// lossy scale of the collider
    /// </summary>
    public UnityEngine.Vector3 Scale
    {
        get => BaseCollider.Scale.ToUnityVector();
        set => BaseCollider.Scale = value.ToNewtonsVector();
    }

    public UnityEngine.Vector3 ScaleNoNotify
    {
        set => BaseCollider.ScaleNoNotify = value.ToNewtonsVector();
    }

    public float Restitution
    {
        get => BaseCollider.Restitution;
        set => BaseCollider.Restitution = value;
    }

    public NEWTONS.Core._3D.Bounds Bounds => BaseCollider.Bounds;

    private void Awake()
    {
        Body = GetComponent<NTS_Rigidbody>();
        if (!Body.TryAttachCollider(this))
            Debug.LogError("Collider " + name + " could not be attached to its Rigidbody " + Body.name);

        BaseCollider.Body = Body.Body;

        if (!Application.isPlaying)
            return;

        BaseCollider.OnUpdateScale += OnUpdateNEWTONSScale;
        BaseCollider.AddReference(this);
        BaseCollider.AddToPhysicsEngine();
    }

    private void OnUpdateNEWTONSScale()
    {
        UnityEngine.Vector3 loc = transform.localScale;
        UnityEngine.Vector3 los = transform.lossyScale;
        UnityEngine.Vector3 k = new UnityEngine.Vector3(los.x / loc.x, los.y / loc.y, los.z / loc.z);
        transform.localScale = new UnityEngine.Vector3(Scale.x / k.x, Scale.y / k.y, Scale.z / k.z);
    }

    private void OnValidate()
    {
        Body = Body != null ? Body : GetComponent<NTS_Rigidbody>();
    }

    private void Update()
    {
        if (transform.lossyScale != Scale)
            ScaleNoNotify = transform.lossyScale;
    }

    private void OnDestroy()
    {
        Body.TryAttachCollider(null);
        if (!Application.isPlaying)
            return;

        BaseCollider.Dispose();
    }


    public void Dispose()
    {
        Body.TryAttachCollider(null);
        Destroy(this);
    }

    #region Serialization

    [System.Serializable]
    private struct SerializerCollider
    {
        public Vector3 center;
        public Vector3 scale;
        public float restitution;
    }

    [SerializeField, HideInInspector]
    private SerializerCollider _serializerCollider;

    public virtual void OnBeforeSerialize()
    {
        _serializerCollider = new()
        {
            center = Center,
            scale = Scale,
            restitution = Restitution
        };

        Body.TryAttachCollider(this);
        BaseCollider.Body = Body.Body;

    }

    public virtual void OnAfterDeserialize()
    {
        Center = _serializerCollider.center;
        Scale = _serializerCollider.scale;
        Restitution = _serializerCollider.restitution;
    }
    #endregion
}
