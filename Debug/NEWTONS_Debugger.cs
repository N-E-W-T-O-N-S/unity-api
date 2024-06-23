using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class NEWTONS_Debugger : MonoBehaviour
{
    private void OnEnable()
    {
        NEWTONS_Debugger[] allDebuger = FindObjectsOfType<NEWTONS_Debugger>();
        if (allDebuger.Length > 1)
        {
            gameObject.SetActive(false);
            Debug.LogWarning("There are more than one NEWTONS_Debugger in the scene. Disabling the new one.");
            return;
        }

        NEWTONS.Debugger.Debug.OnLog += OnLog;
        NEWTONS.Debugger.Debug.OnWarning += OnWarning;
        NEWTONS.Debugger.Debug.OnError += OnError;
    }

    private void OnLog(string message)
    {
        Debug.Log(message);
    }

    private void OnWarning(string message) 
    {
        Debug.LogWarning(message);
    }

    private void OnError(string message)
    {
        Debug.LogError(message);
    }

    private void OnDisable()
    {
        NEWTONS.Debugger.Debug.OnLog -= OnLog;
        NEWTONS.Debugger.Debug.OnWarning -= OnWarning;
        NEWTONS.Debugger.Debug.OnError -= OnError;
    }

}
