using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

namespace FetchAndSaveUdemyCouponsHandler.Shared.Helpers
{
    public static class LoggerUtils
    {
        public static void Info(string msg)
        {
            LambdaLogger.Log($"INFO: {msg}");
        }

        public static async Task ErrorAsync(string msg, Exception e = null)
        {
            LambdaLogger.Log($"ERROR: {msg}");
            if (e != null)
            {
                LambdaLogger.Log(e.ToString());
            }

            if (Function.ErrorReportingService != null)
            {
                if (e != null)
                {
                    msg = $"{msg} {Environment.NewLine}{e}";
                }
                await Function.ErrorReportingService.ReportAsync(msg);
            }
        }

        public static void Warn(string msg)
        {
            LambdaLogger.Log($"WARN: {msg}");
        }
    }
}