using System;
using System.Net.Http;
using System.Threading.Tasks;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;

namespace FetchAndSaveUdemyCouponsHandler.Shared.Helpers
{
    public static class HttpHelper
    {
        public static async Task<GetHttpResponseAsStringResult> GetStringAsync(string url,
            HttpClient httpClient = null)
        {
            var result = new GetHttpResponseAsStringResult();
            
            if (string.IsNullOrWhiteSpace(url))
            {
                result.AddError($"{nameof(url)} is empty");
                return result;
            }

            httpClient ??= new HttpClient();

            LoggerUtils.Info($"getting HTTP response as string from {url}");
            try
            {
                result.Response = await httpClient.GetStringAsync(url);
                result.IsSuccess = true;
                return result;
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
                LoggerUtils.Error($"an error occured while getting HTTP response as string from {url}", e);
            }

            return result;
        }
    }
}