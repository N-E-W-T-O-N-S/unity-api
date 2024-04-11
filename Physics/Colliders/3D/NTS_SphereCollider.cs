using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTS_SphereCollider : NTS_Collider
{

    [SerializeField, HideInInspector]
    private NEWTONS.Core._3D.SphereCollider _sphereCollider;

    public NEWTONS.Core._3D.SphereCollider SphereColl { get => _sphereCollider; private set => _sphereCollider = value; }

    public override NTS_Rigidbody Body { get; protected set; }

    public override Vector3 Center { get => SphereColl.Center.ToUnityVector(); set => SphereColl.Center = value.ToNewtonsVector(); }

    public override Vector3 GlobalCenter => SphereColl.GlobalCenter.ToUnityVector();

    public override Quaternion Rotation => SphereColl.Rotation.ToUnityQuaternion();

    public override Vector3 Scale { get => SphereColl.Scale.ToUnityVector(); set => SphereColl.Scale = value.ToNewtonsVector(); }

    public override Vector3 ScaleNoNotify { set => SphereColl.ScaleNoNotify = value.ToNewtonsVector(); }

    public override float Restitution { get => SphereColl.Restitution; set => SphereColl.Restitution = value; }

    
    public float Radius { get => SphereColl.Radius; set => SphereColl.Radius = value; }

    public float ScaledRadius => SphereColl.ScaledRadius;

    private void Awake()
    {
        Body = GetComponent<NTS_Rigidbody>();
        SphereColl.Body = Body.Body;
        SphereColl.OnUpdateScale += OnUpdateNEWTONSScale;

        SphereColl.AddToPhysicsEngine();
    }

    private void OnUpdateNEWTONSScale()
    {
        UnityEngine.Vector3 loc = transform.localScale;
        UnityEngine.Vector3 los = transform.lossyScale;
        UnityEngine.Vector3 k = new UnityEngine.Vector3(los.x / loc.x, los.y / loc.y, los.z / loc.z);
        transform.localScale = new UnityEngine.Vector3(Scale.x / k.x, Scale.y / k.y, Scale.z / k.z);

        // lossy = local * K
        // K = lossy / local
        // newLocal += newLossy - K
    }

#if UNITY_EDITOR
    public void Validate()
    {
        try
        {
            NTS_Rigidbody kBody = GetComponent<NTS_Rigidbody>();
            if (SphereColl.Body != kBody.Body || Body != kBody)
            {
                SphereColl.Body = kBody.Body;
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
