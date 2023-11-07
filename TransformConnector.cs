using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Look into removing this:
[ExecuteAlways]
public class TransformConnector : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private KinematicBody _body;

    [SerializeField, HideInInspector]
    private CuboidCollider _collider;

    private void Start()
    {
        _body = GetComponent<KinematicBody>();
        _collider = GetComponent<CuboidCollider>();
    }

    private void Update()
    {
        _body.Body.PositionNoNotify = transform.position.ToNewtonsVector();
        _collider.GlobalScale = transform.lossyScale;
    }
}
