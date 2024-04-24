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

    public override NEWTONS.Core._2D.Collider2D BaseCollider => CuboidCollider;


    public UnityEngine.Vector2 Size { get => CuboidCollider.Size.ToUnityVector(); set => CuboidCollider.Size = value.ToNewtonsVector(); }

    public UnityEngine.Vector2 ScaledSize => CuboidCollider.ScaledSize.ToUnityVector();

    public UnityEngine.Vector2[] Points => CuboidCollider.Points.ToUnityVectorArray();

    public UnityEngine.Vector2[] PointsRaw => CuboidCollider.PointsRaw.ToUnityVectorArray();

#if UNITY_EDITOR
    public bool foldOutDebugManager = false;
#endif

}
