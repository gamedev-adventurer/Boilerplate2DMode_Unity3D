using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLoggerUtil : MonoBehaviour
{
    public void DebugLogWithMessage(string message)
    {
        if (GameManager.GameMaster.DebuggingOn)
        {
            Debug.Log(message);
        }

    }
}
