using NEWTONS.Core._3D;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(NTS_Rigidbody2D))]
public abstract class NTS_Collider2D : MonoBehaviour, NEWTONS.Core._2D.IColliderReference2D, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private NTS_Rigidbody2D _body;

    /// <summary>
    /// KinematicBody2D attached to the collider
    /// </summary>
    public NTS_Rigidbody2D Body { get => _body; protected set => _body = value; }

    public abstract NEWTONS.Core._2D.Collider2D BaseCollider { get; }

    /// <summary>
    /// the local center of the collider
    /// </summary>
    public UnityEngine.Vector2 Center
    {
        get => BaseCollider.Center.ToUnityVector();
        set => BaseCollider.Center = value.ToNewtonsVector();
    }

    public UnityEngine.Vector2 GlobalCenter => BaseCollider.GlobalCenter.ToUnityVector();

    public float Rotation => BaseCollider.Rotation;

    /// <summary>
    /// lossy scale of the collider
    /// </summary>
    public UnityEngine.Vector2 Scale
    {
        get => BaseCollider.Scale.ToUnityVector();
        set => BaseCollider.Scale = value.ToNewtonsVector();
    }

    public UnityEngine.Vector2 ScaleNoNotify
    {
        set => BaseCollider.ScaleNoNotify = value.ToNewtonsVector();
    }

    public float Restitution 
    { 
        get => BaseCollider.Restitution; 
        set => BaseCollider.Restitution = value; 
    }

    public NEWTONS.Core._2D.Bounds2D Bounds => BaseCollider.Bounds;


    private void Awake()
    {
        Body = GetComponent<NTS_Rigidbody2D>();
        if (!Body.TryAttachCollider(this))
            Debug.LogError("Collider " + name + " could not be attached to its Rigidbody " + Body.name);

        BaseCollider.Body = Body.Body;

        if (!Application.isPlaying)
            return;

        BaseCollider.OnUpdateScale += OnUpdateNEWTONSScale;
        BaseCollider.AddReference(this);
        BaseCollider.AddToPhysicsEngine();
    }

    private void OnValidate()
    {
        Body = Body != null ? Body : GetComponent<NTS_Rigidbody2D>();
    }

    private void OnUpdateNEWTONSScale()
    {
        UnityEngine.Vector3 loc = transform.localScale;
        UnityEngine.Vector3 los = transform.lossyScale;
        UnityEngine.Vector3 k = new UnityEngine.Vector3(los.x / loc.x, los.y / loc.y);
        transform.localScale = new UnityEngine.Vector3(Scale.x / k.x, Scale.y / k.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BaseCollider.Dispose();

        if (transform.lossyScale != (Vector3)Scale)
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
    private struct SerializerCollider2D
    {
        public Vector2 center;
        public Vector2 scale;
        public float restitution;
    }

    [SerializeField, HideInInspector]
    private SerializerCollider2D _serializerCollider;

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
