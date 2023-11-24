using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Look into removing this:
[ExecuteAlways]
public class TransformConnector : MonoBehaviour
{
    private Vector3 _oldPosition;
    private Vector3 _oldScale;

    public event Action OnPositionChanged;
    public event Action OnScaleChanged;

    private void Start()
    {
        _oldPosition = transform.position;
        _oldScale = transform.localScale;
    }

    private void Update()
    {
        if (transform.position != _oldPosition)
        {
            OnPositionChanged?.Invoke();
            _oldPosition = transform.position;
        }
        if (transform.lossyScale != _oldScale)
        {
            OnScaleChanged?.Invoke();
            _oldScale = transform.lossyScale;
        }
    }
}
