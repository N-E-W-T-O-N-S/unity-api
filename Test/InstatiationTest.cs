using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiationTest : MonoBehaviour
{
    public GameObject obj;

    float time = 0f;

    public int count = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > 0.5f)
        {
            time = Time.time;
            var newG = GameObject.Instantiate(obj);
            newG.GetComponent<NTS_Rigidbody2D>().AddForce(new Vector2(2 * (Random.value - 0.5f), 2 * (Random.value - 0.5f)), NEWTONS.Core.ForceMode.VelocityChange);
            count++;
        }
    }
}
