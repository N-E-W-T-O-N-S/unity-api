using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 2);
    }
}
