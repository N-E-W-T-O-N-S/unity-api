using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public NTS_Rigidbody body;
    public float speed = 2.5f;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, Time.fixedDeltaTime * 15);
    }
}
