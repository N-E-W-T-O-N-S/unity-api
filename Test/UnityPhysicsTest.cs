using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnityPhysicsTest : MonoBehaviour
{
    private Rigidbody _rb;
    public Hello[] hello;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_rb.velocity = Vector3.right * 2;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _rb.MovePosition(_rb.position + 2 * Time.deltaTime * Vector3.right);
        }
    }

    [System.Serializable]
    public struct Hello
    {
        public NEWTONS.Core._2D.Bounds2D bounds;
        public float test;
    }
}
