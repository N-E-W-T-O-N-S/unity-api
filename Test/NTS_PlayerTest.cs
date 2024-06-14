using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NTS_Rigidbody2D))]
public class NTS_PlayerTest : MonoBehaviour
{
    NTS_Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<NTS_Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
            _rb.AddCurrentVelocity(Vector2.right * 2f);
    }
}
