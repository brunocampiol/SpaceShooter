using System;
using System.Threading.Tasks;

public interface ILogger
{
        void LogError(string errorMessage);
        void LogException(Exception ex);
        void LogException(Exception ex, string errorMessage);
        void LogInfo(string infoMessage);
        void LogWarning(string warningMessage);
}
