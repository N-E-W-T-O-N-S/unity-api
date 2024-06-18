using NEWTONS.Core._2D;
using NEWTONS.Core._3D;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class NTS_CircleCollider : NTS_Collider2D
{
    private NEWTONS.Core._2D.CircleCollider _circleCollider = new();

    public NEWTONS.Core._2D.CircleCollider CircleCollider { get => _circleCollider; set => _circleCollider = value; }

    public override NEWTONS.Core._2D.Collider2D BaseCollider => CircleCollider;


    public float Radius { get => CircleCollider.Radius; set => CircleCollider.Radius = value; }

    public float ScaledRadius => CircleCollider.ScaledRadius;

    #region Serialization

    [System.Serializable]
    private struct SerializerCircleCollider2D
    {
        public float radius;
    }

    [SerializeField, HideInInspector]
    private SerializerCircleCollider2D _serializerCircleCollider;

    public override void OnBeforeSerialize()
    {
        base.OnBeforeSerialize();
        _serializerCircleCollider = new()
        {
            radius = Radius
        };
    }

    public override void OnAfterDeserialize()
    {
        base.OnAfterDeserialize();
        Radius = _serializerCircleCollider.radius;
    }

    #endregion

}
