using NEWTONS.Core._2D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NTS_CuboidCollider2D : NTS_Collider2D
{

    [SerializeField, HideInInspector]
    private CuboidCollider2D _cuboidCollider;

    public CuboidCollider2D CuboidCollider { get => _cuboidCollider; private set => _cuboidCollider = value; }

    protected override NEWTONS.Core._2D.Collider2D BaseCollider => CuboidCollider;


    public override NTS_Rigidbody2D Body { get; protected set; }

    public override UnityEngine.Vector2 Center { get => CuboidCollider.Center.ToUnityVector(); set => CuboidCollider.Center = value.ToNewtonsVector(); }

    public override UnityEngine.Vector2 GlobalCenter => Center + Body.Position;

    public override UnityEngine.Vector2 Scale { get => CuboidCollider.Scale.ToUnityVector(); set => CuboidCollider.Scale = value.ToNewtonsVector(); }

    public override UnityEngine.Vector2 ScaleNoNotify { set => CuboidCollider.ScaleNoNotify = value.ToNewtonsVector(); }

    public override float Rotation { get => CuboidCollider.Rotation; }

    public override float Restitution { get => CuboidCollider.Restitution; set => CuboidCollider.Restitution = value; }


    public UnityEngine.Vector2 Size { get => CuboidCollider.Size.ToUnityVector(); set => CuboidCollider.Size = value.ToNewtonsVector(); }

    public UnityEngine.Vector2 ScaledSize => CuboidCollider.ScaledSize.ToUnityVector();

    public UnityEngine.Vector2[] Points => CuboidCollider.Points.ToUnityVectorArray();

    public UnityEngine.Vector2[] PointsRaw => CuboidCollider.PointsRaw.ToUnityVectorArray();

#if UNITY_EDITOR
    public bool foldOutDebugManager = false;

#endif

}
