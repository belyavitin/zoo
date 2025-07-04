using System;
using Extensions;
using UnityEngine;

namespace GameLogging
{
    [Flags]
    public enum LoggingTypes
    {
        Debug = 1,
        Info = 2,
        Warning = 4,
        Error = 8,
        Default = Info | Warning | Error
    }

    // да, я люблю настраиваемые логи которые легко читать и которые не устаревают при изменениях :)
    public abstract class MonoBehaviourLogger : MonoBehaviour
    {
        [SerializeField]
        private LoggingTypes logsEnabled = LoggingTypes.Default;

        protected void LogWarning(params object[] log)
        {
            if (logsEnabled.HasFlag(LoggingTypes.Warning))
                Debug.LogWarning(string.Join(' ', log) + About(), this);
        }

        protected void LogError(params object[] log)
        {
            if (logsEnabled.HasFlag(LoggingTypes.Error))
                Debug.LogError(string.Join(' ', log) + About(), this);
        }

        protected void LogInfo(params object[] log)
        {
            if (logsEnabled.HasFlag(LoggingTypes.Info))
                Debug.Log(string.Join(' ', log) + About(), this);
        }

        protected void LogDebug(params object[] log)
        {
            if (logsEnabled.HasFlag(LoggingTypes.Debug))
                Debug.Log(string.Join(' ', log) + About(), this);
        }

        private string About()
        {
            return " by " + GetType().Name + " in " + gameObject.Hierarchy() + " at " + transform.position;
        }
    }

    // и не только в монобехах. В принципе тут можно было бы обойтись без копипасты, но может потом 
    [Serializable]
    public abstract class GameLogger
    {
        protected GameObject relevant;

        [SerializeField]
        private LoggingTypes logsEnabled = LoggingTypes.Default;

        protected void LogWarning(params object[] log)
        {
            if (logsEnabled.HasFlag(LoggingTypes.Warning))
                Debug.LogWarning(string.Join(' ', log) + About(), relevant);
        }

        protected void LogError(params object[] log)
        {
            if (logsEnabled.HasFlag(LoggingTypes.Error))
                Debug.LogError(string.Join(' ', log) + About(), relevant);
        }

        protected void LogInfo(params object[] log)
        {
            if (logsEnabled.HasFlag(LoggingTypes.Info))
                Debug.Log(string.Join(' ', log) + About(), relevant);
        }

        protected void LogDebug(params object[] log)
        {
            if (logsEnabled.HasFlag(LoggingTypes.Debug))
                Debug.Log(string.Join(' ', log) + About(), relevant);
        }

        private string About()
        {
            if (relevant)
                return " by " + GetType().Name + " in " + relevant.Hierarchy() + " at " + relevant.transform.position;
            else
                return " by " + GetType().Name;
        }

    }
}