using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS_Counter : MonoBehaviour
{
    public TMP_Text text;
    float time = 0;
    public float refreshRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > refreshRate)
        {
            text.text = (1f / Time.unscaledDeltaTime).ToString("F0");
            time = 0;
        }

        time += Time.unscaledDeltaTime;
    }
}
