using NEWTONS.Core._2D;
using NEWTONS.Core._3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTS_CircleCollider : NTS_Collider2D
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core._2D.CircleCollider _circleCollider;

    public NEWTONS.Core._2D.CircleCollider CircleCollider { get => _circleCollider; set => _circleCollider = value; }

    protected override NEWTONS.Core._2D.Collider2D BaseCollider => CircleCollider;


    public override NTS_Rigidbody2D Body { get; protected set; }

    public override Vector2 Center { get => CircleCollider.Center.ToUnityVector(); set => CircleCollider.Center = value.ToNewtonsVector(); }

    public override Vector2 GlobalCenter => CircleCollider.GlobalCenter.ToUnityVector();

    public override float Rotation => CircleCollider.Rotation;

    public override Vector2 Scale { get => CircleCollider.Scale.ToUnityVector(); set => CircleCollider.Scale = value.ToNewtonsVector(); }

    public override Vector2 ScaleNoNotify { set => CircleCollider.ScaleNoNotify = value.ToNewtonsVector(); }

    public override float Restitution { get => CircleCollider.Restitution; set => CircleCollider.Restitution = value; }


    public float Radius { get => CircleCollider.Radius; set => CircleCollider.Radius = value; }

    public float ScaledRadius => CircleCollider.ScaledRadius;

#if UNITY_EDITOR
    public bool foldOutDebugManager = false;
#endif

}
