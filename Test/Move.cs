using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody body;
    List<ContactPoint> contactPoints = new List<ContactPoint>();

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.velocity = Vector3.left;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.GetContacts(contactPoints);
        Debug.Log(contactPoints.Count);
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (ContactPoint point in contactPoints)
        {
            Gizmos.DrawRay(point.point, point.normal);
        }
    }
}
