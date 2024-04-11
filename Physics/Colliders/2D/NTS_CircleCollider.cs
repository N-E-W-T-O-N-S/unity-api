using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTS_CircleCollider : NTS_Collider2D
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core._2D.CircleCollider _circleCollider;

    public NEWTONS.Core._2D.CircleCollider CircleCollider { get => _circleCollider; set => _circleCollider = value; }

    public override NTS_Rigidbody2D Body { get; protected set; }
    
    public override Vector2 Center { get => CircleCollider.Center.ToUnityVector(); set => CircleCollider.Center = value.ToNewtonsVector(); }

    public override Vector2 GlobalCenter => CircleCollider.GlobalCenter.ToUnityVector();

    public override float Rotation => CircleCollider.Rotation;

    public override Vector2 Scale { get => CircleCollider.Scale.ToUnityVector(); set => CircleCollider.Scale = value.ToNewtonsVector(); }

    public override Vector2 ScaleNoNotify { set => CircleCollider.ScaleNoNotify = value.ToNewtonsVector(); }


    public float Radius { get => CircleCollider.Radius; set => CircleCollider.Radius = value; }

    public float ScaledRadius => CircleCollider.ScaledRadius;

    private void Awake()
    {
        Body = GetComponent<NTS_Rigidbody2D>();
        CircleCollider.Body = Body.Body;
        CircleCollider.OnUpdateScale += OnUpdateNEWTONSScale;

        CircleCollider.AddToPhysicsEngine();
    }

    private void OnUpdateNEWTONSScale()
    {
        UnityEngine.Vector3 loc = transform.localScale;
        UnityEngine.Vector3 los = transform.lossyScale;
        UnityEngine.Vector3 k = new UnityEngine.Vector3(los.x / loc.x, los.y / loc.y);
        transform.localScale = new UnityEngine.Vector3(Scale.x / k.x, Scale.y / k.y);

        // lossy = local * K
        // K = lossy / local
        // newLocal += newLossy - K
    }

#if UNITY_EDITOR
    public void Validate()
    {
        try
        {
            NTS_Rigidbody2D kBody = GetComponent<NTS_Rigidbody2D>();
            if (CircleCollider.Body != kBody.Body || Body != kBody)
            {
                CircleCollider.Body = kBody.Body;
                Body = kBody;
            }
        }
        catch
        {
            debugManager.LogError("CuboidCollider: " + name + " is missing a KinematicBody component");
        }
    }

    public bool foldOutDebugManager = false;

#endif

}
