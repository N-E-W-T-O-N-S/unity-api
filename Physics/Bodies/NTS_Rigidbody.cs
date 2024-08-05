using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteAlways]
public class NTS_Rigidbody : MonoBehaviour, NEWTONS.Core._3D.IRigidbodyReference, ISerializationCallbackReceiver
{
    private NEWTONS.Core._3D.Rigidbody _body = new();

    /// <summary>
    /// Direct access to the Physics Engine's Rigidbody
    /// <inheritdoc cref="NEWTONS.Core.Rigidbody"/>
    /// <para><u><b>WARNING:</b></u> <b>Do not directly use to change properties</b></para>
    /// </summary>
    public NEWTONS.Core._3D.Rigidbody Body { get => _body; private set => _body = value; }

    [SerializeField, HideInInspector]
    private NTS_Collider _attachedCollider;

    public bool IsStatic
    {
        get => Body.IsStatic;
        set => Body.IsStatic = value;
    }

    public UnityEngine.Vector3 Position
    {
        get => Body.Position.ToUnityVector();
        set => Body.Position = value.ToNewtonsVector();
    }


    public UnityEngine.Quaternion Rotation
    {
        get => Body.Rotation.ToUnityQuaternion();
        set => Body.Rotation = value.ToNewtonsQuaternion();
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

    public UnityEngine.Vector3 AngularVelocity
    {
        get => Body.AngularVelocity.ToUnityVector();
        set => Body.AngularVelocity = value.ToNewtonsVector();
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

    public float InvMass => Body.InvMass;

    private void Awake()
    {
        if (!Application.isPlaying)
            return;

        Body.OnUpdatePosition += OnUpdateNEWTONSPosition;
        Body.OnUpdateRotation += OnUpdateNEWTONSRotation;
        Body.AddReference(this);
        Body.AddToPhysicsEngine();
    }

    private void Update()
    {
        if (Position != transform.position)
            Position = transform.position;

        if (Rotation != transform.rotation)
            Rotation = transform.rotation;
    }

    public void AddForce(UnityEngine.Vector3 force, NEWTONS.Core.ForceMode forceMode)
    {
        Body?.AddForce(force.ToNewtonsVector(), forceMode);
    }

    private void OnUpdateNEWTONSPosition()
    {
        transform.position = Position;
    }

    private void OnUpdateNEWTONSRotation() => transform.rotation = Rotation;

    /// <summary>
    /// Gets the rigidbodys attached collider
    /// </summary>
    /// <returns>true if a collider is attached else false</returns>
    public bool TryGetAttachedCollider(out NTS_Collider collider)
    {
        collider = null;
        if (_attachedCollider == null)
            return false;

        collider = _attachedCollider;
        return true;
    }

    /// <summary>
    /// Sets this rigidbodys collider if the colliders rigidbody is the same as this one (or null).
    /// </summary>
    /// <returns>true if the bodies are the same or null else false</returns>
    public bool TryAttachCollider(NTS_Collider collider)
    {

        if (collider == null)
        {
            _attachedCollider = null;
            Body.Collider = null;
            return true;
        }
        else if (collider.Body != this) return false;

        _attachedCollider = collider;
        Body.Collider = _attachedCollider.BaseCollider;
        return true;
    }

    private void OnDestroy()
    {
        if (!Application.isPlaying)
            return;

        Body.Dispose();
    }

    public void Dispose()
    {
        if (TryGetAttachedCollider(out NTS_Collider coll))
            Destroy(coll);

        Destroy(this);
    }

    #region Serialization
    [System.Serializable]
    private struct SerializerRigidbody2D
    {
        public bool isStatic;
        public Vector3 position;
        public Quaternion rotation;
        public bool customInertia;
        public float inertia;
        public bool useGravity;
        public Vector3 velocity;
        public float angularVelocity;
        public float mass;
        public float drag;
    }

    [SerializeField, HideInInspector]
    private SerializerRigidbody2D _serializerBody;

    public void OnBeforeSerialize()
    {
        _serializerBody = new()
        {
            isStatic = IsStatic,
            position = Position,
            rotation = Rotation,
            useGravity = UseGravity,
            velocity = Velocity,
            mass = Mass,
            drag = Drag,
        };
    }

    public void OnAfterDeserialize()
    {
        IsStatic = _serializerBody.isStatic;
        Position = _serializerBody.position;
        Rotation = _serializerBody.rotation;
        UseGravity = _serializerBody.useGravity;
        Velocity = _serializerBody.velocity;
        Mass = _serializerBody.mass;
        Drag = _serializerBody.drag;
    }
    #endregion

#if UNITY_EDITOR
    public bool foldOutInfo = false;
#endif
}