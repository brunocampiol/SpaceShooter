using System;
using System.Threading.Tasks;
using UnityEngine;

public class Logger : ILogger
{
    public void LogException(Exception ex, string errorMessage)
    {
        Debug.LogError($"[ERROR]: {errorMessage}, [STACK]: {ex.StackTrace}");
    }

    public void LogException(Exception ex)
    {
        Debug.LogError($"[ERROR]: {ex.AllExceptionMessages()}, [STACK]: {ex.StackTrace}");
    }

    public void LogError(string errorMessage)
    {
        Debug.LogError($"[ERROR]: {errorMessage}");
    }

    public void LogWarning(string warningMessage)
    {
        Debug.LogWarning($"[WARNING]: {warningMessage}");
    }

    public void LogInfo(string infoMessage)
    {
        Debug.Log($"[INFO]: {infoMessage}");
    }
}
