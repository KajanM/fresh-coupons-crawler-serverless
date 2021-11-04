using System;
using Amazon.Lambda.Core;

namespace FetchAndSaveUdemyCouponsHandler.Shared.Helpers
{
    public static class LoggerUtils
    {
        public static void Info(string msg)
        {
            LambdaLogger.Log($"INFO: {msg}");
        }

        public static void Error(string msg, Exception e = null)
        {
            LambdaLogger.Log($"ERROR: {msg}");
            if (e != null)
            {
                LambdaLogger.Log(e.ToString());
            }
        }

        public static void Warn(string msg)
        {
            LambdaLogger.Log($"WARN: {msg}");
        }
    }
}