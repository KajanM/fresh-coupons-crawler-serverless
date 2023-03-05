using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;
using FetchAndSaveUdemyCouponsHandler.Shared.Extensions;

namespace FetchAndSaveUdemyCouponsHandler.Shared.Helpers
{
    public static class HttpHelper
    {
        public static async Task<BaseResultWithPayload<TResponse>> GetAsync<TResponse>(string url, HttpClient httpClient = null)
        {
            var result = new BaseResultWithPayload<TResponse>();

            if (string.IsNullOrWhiteSpace(url))
            {
                result.AddError($"{nameof(url)} is empty");
                return result;
            }

            HttpResponseMessage response = null;
            try
            {
                httpClient ??= new HttpClient();
                response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                await LoggerUtils.ErrorAsync($"An error occured while making GET request to {url}", e);
                result.AddError(e.Message);
            }

            if (response == null)
            {
                result.AddError("The HttpResponseMessage is null");
                return result;
            }

            try
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var deserializeOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                result.Data = await JsonSerializer.DeserializeAsync<TResponse>(contentStream, deserializeOptions);
                result.IsSuccess = true;

                return result;
            }
            catch (Exception e)
            {
                await LoggerUtils.ErrorAsync(
                    $"An error occured while deserializing HTTP response to {nameof(TResponse)}.{Environment.NewLine}Response: {response.ToJson()}",
                    e);
                result.AddError(e.Message);
                return result;
            }
        }
        
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
                await LoggerUtils.ErrorAsync($"an error occured while getting HTTP response as string from {url}", e);
            }

            return result;
        }
    }
}