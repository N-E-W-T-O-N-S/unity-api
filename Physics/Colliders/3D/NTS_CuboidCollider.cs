using NEWTONS.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTS_CuboidCollider : NTS_Collider
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core.CuboidCollider _cuboidColl;

    public NEWTONS.Core.CuboidCollider CuboidColl { get => _cuboidColl; private set => _cuboidColl = value; }

    public override NTS_Rigidbody Body { get; protected set; }

    public override UnityEngine.Vector3 Center { get => CuboidColl.Center.ToUnityVector(); set => CuboidColl.Center = value.ToNewtonsVector(); }

    public override UnityEngine.Vector3 GlobalCenter { get => CuboidColl.GlobalCenter.ToUnityVector(); }

    public override UnityEngine.Quaternion Rotation { get => CuboidColl.Rotation.ToUnityQuaternion(); }

    public override UnityEngine.Vector3 Scale { get => CuboidColl.Scale.ToUnityVector(); set => CuboidColl.Scale = value.ToNewtonsVector(); }

    public override UnityEngine.Vector3 ScaleNoNotify { set => CuboidColl.ScaleNoNotify = value.ToNewtonsVector(); }

    public override float Restitution { get => CuboidColl.Restitution; set => CuboidColl.Restitution = value; }
    
    
    public UnityEngine.Vector3 Size { get => CuboidColl.Size.ToUnityVector(); set => CuboidColl.Size = value.ToNewtonsVector(); }
    
    public UnityEngine.Vector3 ScaledSize => CuboidColl.ScaledSize.ToUnityVector();

    public int[] Indices { get => CuboidColl.Indices; set => CuboidColl.Indices = value; }

    public UnityEngine.Vector3[] PointsRaw { get => CuboidColl.PointsRaw.ToUnityVectorArray(); set => CuboidColl.PointsRaw = value.ToNewtonsVectorArray(); }

    public UnityEngine.Vector3[] Points { get => CuboidColl.Points.ToUnityVectorArray(); }


    private void Awake()
    {
        Body = GetComponent<NTS_Rigidbody>();
        CuboidColl.Body = Body.Body;
        CuboidColl.OnUpdateScale += OnUpdateNEWTONSScale;

        CuboidColl.AddToPhysicsEngine();
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

    public override void Dispose()
    {
        CuboidColl = null;
        Destroy(this);
    }

    public override IColliderReference SetCollider(NEWTONS.Core.Collider collider)
    {
        CuboidColl = collider as NEWTONS.Core.CuboidCollider;
        if (CuboidColl == null)
            throw new ArgumentException("Collider must be of type CuboidCollider");
        return this;
    }

#if UNITY_EDITOR
    public void Validate()
    {
        try
        {
            NTS_Rigidbody kBody = GetComponent<NTS_Rigidbody>();
            if (CuboidColl.Body != kBody.Body || Body != kBody)
            {
                CuboidColl.Body = kBody.Body;
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
