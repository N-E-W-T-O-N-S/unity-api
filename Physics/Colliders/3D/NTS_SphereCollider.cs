using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTS_SphereCollider : NTS_Collider
{

    private NEWTONS.Core._3D.SphereCollider _sphereCollider = new();

    public NEWTONS.Core._3D.SphereCollider SphereCollider { get => _sphereCollider; set => _sphereCollider = value; }

    public override NEWTONS.Core._3D.Collider BaseCollider => SphereCollider;


    public float Radius { get => SphereCollider.Radius; set => SphereCollider.Radius = value; }

    public float ScaledRadius => SphereCollider.ScaledRadius;

    #region Serialization

    [System.Serializable]
    private struct SerializerCircleCollider
    {
        public float radius;
    }

    [SerializeField, HideInInspector]
    private SerializerCircleCollider _serializerSphereCollider;

    public override void OnBeforeSerialize()
    {
        base.OnBeforeSerialize();
        _serializerSphereCollider = new()
        {
            radius = Radius
        };
    }

    public override void OnAfterDeserialize()
    {
        base.OnAfterDeserialize();
        Radius = _serializerSphereCollider.radius;
    }

    #endregion
}
