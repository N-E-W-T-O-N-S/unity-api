using NEWTONS.Core._3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(NTS_Rigidbody2D))]
public abstract class NTS_Collider2D : MonoBehaviour, NEWTONS.Core._2D.IColliderReference2D
{
    // INFO: Debug
    // <------------------------->
    [HideInInspector]
    public NTS_DebugManager debugManager;
    // <------------------------->

    /// <summary>
    /// KinematicBody2D attached to the collider
    /// </summary>
    public abstract NTS_Rigidbody2D Body { get; protected set; }

    protected abstract NEWTONS.Core._2D.Collider2D BaseCollider { get; }

    /// <summary>
    /// the local center of the collider
    /// </summary>
    public abstract UnityEngine.Vector2 Center { get; set; }

    public abstract UnityEngine.Vector2 GlobalCenter { get; }

    public abstract float Rotation { get; }

    /// <summary>
    /// lossy scale of the colliser
    /// </summary>
    public abstract UnityEngine.Vector2 Scale { get; set; }

    public abstract UnityEngine.Vector2 ScaleNoNotify { set; }

    public abstract float Restitution { get; set; }

    private void Awake()
    {
        Body = GetComponent<NTS_Rigidbody2D>();
        if (!Body.TryAttachCollider(this))
            Debug.LogError("Collider " + name + " could not be attached to its Rigidbody " + Body.name);

        BaseCollider.Body = Body.Body;
        BaseCollider.Body.Collider = BaseCollider;

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

        BaseCollider.OnUpdateScale -= OnUpdateNEWTONSScale;
        BaseCollider.Dispose();
    }

    public void Dispose()
    {
        Body.TryAttachCollider(null);
        Destroy(this);
    }


#if UNITY_EDITOR
    public void Validate()
    {
        try
        {
            NTS_Rigidbody2D kBody = GetComponent<NTS_Rigidbody2D>();
            if (BaseCollider.Body != kBody.Body || Body != kBody)
            {
                BaseCollider.Body = kBody.Body;
                Body = kBody;
            }
        }
        catch
        {
            debugManager.LogError($"The collider {name} has no {typeof(NTS_Rigidbody2D)} attached to it");
        }
    }
#endif
}
