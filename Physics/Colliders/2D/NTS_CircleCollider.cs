using NEWTONS.Core._2D;
using NEWTONS.Core._3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTS_CircleCollider : NTS_Collider2D
{
    [SerializeField, HideInInspector]
    private NEWTONS.Core._2D.CircleCollider _circleCollider = new();

    public NEWTONS.Core._2D.CircleCollider CircleCollider { get => _circleCollider; set => _circleCollider = value; }

    public override NEWTONS.Core._2D.Collider2D BaseCollider => CircleCollider;


    public float Radius { get => CircleCollider.Radius; set => CircleCollider.Radius = value; }

    public float ScaledRadius => CircleCollider.ScaledRadius;

#if UNITY_EDITOR
    public bool foldOutDebugManager = false;
#endif

}
