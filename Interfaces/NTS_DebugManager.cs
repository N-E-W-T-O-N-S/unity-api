using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NTS_DebugManager
{
    public bool showMessages = true;
    public bool showWarnigs = true;
    public bool showErrors = true;

    public void Log(string message)
    {
        if (showMessages)
            Debug.Log(message);
    }

    public void LogWarning(string message)
    {
        if (showWarnigs)
            Debug.LogWarning(message);
    }

    public void LogError(string message)
    {
        if (showErrors)
            Debug.LogError(message);
    }
}
