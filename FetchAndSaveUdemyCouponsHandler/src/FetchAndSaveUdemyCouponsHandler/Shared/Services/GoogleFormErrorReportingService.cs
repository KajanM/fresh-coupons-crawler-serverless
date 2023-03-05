using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

namespace FetchAndSaveUdemyCouponsHandler.Shared.Services;

public class GoogleFormErrorReportingService : IErrorReportingService
{
    private readonly string _timestampFieldId;
    private readonly string _urlFieldId;
    private readonly string _messageFieldId;
    private readonly string _formSubmitUrl;

    private readonly HttpClient _httpClient;

    public GoogleFormErrorReportingService(Dictionary<string, string> parameterStore) : this(parameterStore,
        new HttpClient())
    {
    }

    public GoogleFormErrorReportingService(Dictionary<string, string> parameterStore, HttpClient httpClient)
    {
        if (!parameterStore.ContainsKey(Function.ConfigurationKeys.GoogleFormErrorReportingMessageKey))
            throw new ArgumentNullException(Function.ConfigurationKeys.GoogleFormErrorReportingMessageKey);
        if (!parameterStore.ContainsKey(Function.ConfigurationKeys.GoogleFormErrorReportingUrlKey))
            throw new ArgumentNullException(Function.ConfigurationKeys.GoogleFormErrorReportingUrlKey);
        if (!parameterStore.ContainsKey(Function.ConfigurationKeys.GoogleFormErrorReportingTimestampKey))
            throw new ArgumentNullException(Function.ConfigurationKeys.GoogleFormErrorReportingTimestampKey);
        if (!parameterStore.ContainsKey(Function.ConfigurationKeys.GoogleFormErrorReportingSubmitUrl))
            throw new ArgumentNullException(Function.ConfigurationKeys.GoogleFormErrorReportingSubmitUrl);

        _timestampFieldId = parameterStore[Function.ConfigurationKeys.GoogleFormErrorReportingTimestampKey];
        _urlFieldId = parameterStore[Function.ConfigurationKeys.GoogleFormErrorReportingUrlKey];
        _messageFieldId = parameterStore[Function.ConfigurationKeys.GoogleFormErrorReportingMessageKey];
        _formSubmitUrl = parameterStore[Function.ConfigurationKeys.GoogleFormErrorReportingSubmitUrl];
        
        _httpClient = httpClient;
    }

    public async Task ReportAsync(string message, string url = null)
    {
        var body = new Dictionary<string, string>
        {
            { _timestampFieldId, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) },
            { _messageFieldId, message },
            { _urlFieldId, url },
        };

        try
        {
            var res = await _httpClient.PostAsync(_formSubmitUrl, new FormUrlEncodedContent(body));
            res.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            LambdaLogger.Log($"An error occurred while submitting google form {e.Message}");
        }
    }
}