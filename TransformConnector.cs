using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Look into removing these:
[ExecuteAlways]
public class TransformConnector : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private KinematicBody _body;

    private void Start()
    {
        _body = GetComponent<KinematicBody>();
    }

    private void Update()
    {
        _body.Body.position = transform.position.ToNewtonsVector();
    }
}
