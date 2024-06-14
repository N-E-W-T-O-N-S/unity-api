using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public NTS_Rigidbody2D body;
    public float torque = 1f;

    void Start()
    {
        body = GetComponent<NTS_Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddTorque(torque, NEWTONS.Core.ForceMode.Force);
        }
    }
}
