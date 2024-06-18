using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleTest : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
