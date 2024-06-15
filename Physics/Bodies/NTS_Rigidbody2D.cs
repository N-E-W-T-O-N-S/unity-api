using UnityEngine;

[ExecuteAlways]
public class NTS_Rigidbody2D : MonoBehaviour, NEWTONS.Core._2D.IRigidbodyReference2D
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core._2D.Rigidbody2D _body = new();

    /// <summary>
    /// Direct access to the Physics Engine's KinematicBody
    /// <inheritdoc cref="NEWTONS.Core._2D.Rigidbody2D"/>
    /// <para><u><b>WARNING:</b></u> <b>Do not directly use to change properties</b></para>
    /// </summary>
    public NEWTONS.Core._2D.Rigidbody2D Body { get => _body; private set => _body = value; }

    private NTS_Collider2D _attachedCollider;

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

    public bool FixRotation
    {
        get => Body.FixRotation;
        set => Body.FixRotation = value;
    }

    public float Rotation
    {
        get => _body.Rotation;
        set => _body.Rotation = value;
    }

    public bool CustomInertia
    {
        get => Body.CustomInertia;
        set => Body.CustomInertia = value;
    }

    public float Inertia
    {
        get => Body.Inertia;
        set => Body.Inertia = value;
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

    public float AngularVelocity
    {
        get => Body.AngularVelocity;
        set => Body.AngularVelocity = value;
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
        Body.OnUpdateRotation += OnUpdateNEWTOSRotation;
        Body.AddReference(this);
        Body.AddToPhysicsEngine();
    }

    private void Update()
    {
        if (Position != (UnityEngine.Vector2)transform.position)
            Position = transform.position;

        if (Rotation != transform.rotation.z)
            Rotation = transform.rotation.eulerAngles.z;
    }

    public void AddCurrentVelocity(Vector2 velocity)
    {
        Body.AddCurrentVelocity(velocity.ToNewtonsVector());
    }

    public void AddCurrentAngularVelocity(float angle)
    {
        Body.AddCurrentAngularVelocity(angle);
    }


    public void AddForce(UnityEngine.Vector2 force, NEWTONS.Core.ForceMode forceMode)
    {
        Body.AddForce(force.ToNewtonsVector(), forceMode);
    }

    public void AddTorque(float torque, NEWTONS.Core.ForceMode force)
    {
        Body.AddTorque(torque, force);
    }

    /// <summary>
    /// Gets the rigidbodys attached collider
    /// </summary>
    /// <returns>true if a collider is attached else false</returns>
    public bool TryGetAttachedCollider(out NTS_Collider2D collider)
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
    public bool TryAttachCollider(NTS_Collider2D collider)
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

    private void OnUpdateNEWTONSPosition()
    {
        transform.position = Position;
    }

    private void OnUpdateNEWTOSRotation()
    {
        UnityEngine.Vector3 rot = transform.rotation.eulerAngles;
        transform.rotation = UnityEngine.Quaternion.Euler(rot.x, rot.y, Rotation);
    }

    private void OnDestroy()
    {
        if (!Application.isPlaying)
            return;

        Body.Dispose();
    }

    public void Dispose()
    {
        if (TryGetAttachedCollider(out NTS_Collider2D coll))
            Destroy(coll);

        Destroy(this);
    }


#if UNITY_EDITOR
    public bool foldOutInfo = false;
#endif
}
