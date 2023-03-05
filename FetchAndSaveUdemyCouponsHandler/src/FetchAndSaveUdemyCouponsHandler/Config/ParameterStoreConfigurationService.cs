using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;
using FetchAndSaveUdemyCouponsHandler.Shared.Helpers;

namespace FetchAndSaveUdemyCouponsHandler.Config
{
    public class ParameterStoreConfigurationService
    {
        private readonly AmazonSimpleSystemsManagementClient _client;

        public ParameterStoreConfigurationService(RegionEndpoint region)
        {
            _client = new AmazonSimpleSystemsManagementClient(region);
        }

        public async Task<GetConfigResult> GetAsync(params string[] keys)
        {
            var result = new GetConfigResult();

            try
            {
                result.Config = (await _client.GetParametersAsync(new GetParametersRequest
                {
                    Names = keys.ToList(),
                    WithDecryption = true
                })).Parameters.ToDictionary(p => p.Name, p => p.Value);
                result.IsSuccess = true;
                return result;
            }
            catch (Exception e)
            {
                var msg =
                    $"an error occured while getting config value from the parameter store keys: {string.Join(',', keys)}";
                await LoggerUtils.ErrorAsync(msg, e);
                result.AddError(msg, e);
            }

            return result;
        }
        
        public class GetConfigResult : BaseResult
        {
            public Dictionary<string, string> Config { get; set; } 
        }
    }
}