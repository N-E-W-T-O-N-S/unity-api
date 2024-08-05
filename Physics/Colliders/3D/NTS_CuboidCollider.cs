using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTS_CuboidCollider : NTS_Collider
{
    private NEWTONS.Core._3D.CuboidCollider _cuboidCollider = new();

    public NEWTONS.Core._3D.CuboidCollider CuboidCollider { get => _cuboidCollider; private set => _cuboidCollider = value; }

    public override NEWTONS.Core._3D.Collider BaseCollider => CuboidCollider;

    public Vector3 Size { get => CuboidCollider.Size.ToUnityVector(); set => CuboidCollider.Size = value.ToNewtonsVector(); }

    public Vector3 ScaledSize => CuboidCollider.ScaledSize.ToUnityVector();

    public Vector3[] Points
    {
        get => CuboidCollider.Points.ToUnityVectorArray();
    }

    public int[] Indices
    {
        get => CuboidCollider.Indices;
    }

    public Vector3[] PointsRaw
    {
        get => CuboidCollider.PointsRaw.ToUnityVectorArray();
    }

    #region Serialization

    [System.Serializable]
    private struct SerializerCuboidCollider
    {
        public Vector3 size;
    }

    [SerializeField, HideInInspector]
    private SerializerCuboidCollider _serializerCuboidCollider;

    public override void OnBeforeSerialize()
    {
        base.OnBeforeSerialize();
        _serializerCuboidCollider = new()
        {
            size = Size
        };
    }

    public override void OnAfterDeserialize()
    {
        base.OnAfterDeserialize();
        Size = _serializerCuboidCollider.size;
    }

    #endregion
}
