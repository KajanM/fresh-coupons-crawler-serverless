using System;
using System.Threading.Tasks;
using Amazon;
using FetchAndSaveUdemyCouponsHandler.Config;
using FetchAndSaveUdemyCouponsHandler.DataStore;
using FetchAndSaveUdemyCouponsHandler.Shared.Extensions;
using Xunit;
using static FetchAndSaveUdemyCouponsHandler.Function;

namespace FetchAndSaveUdemyCouponsHandler.Tests
{
    public class GithubServiceTest
    {
        [Fact]
        public async Task CloneShouldSucceed()
        {
            #region Arrange

            var configurationService = new ParameterStoreConfigurationService(RegionEndpoint.APSouth1);
            var getConfigResult = await configurationService.GetAsync(ConfigurationKeys.Branch, ConfigurationKeys.Owner,
                ConfigurationKeys.Repository,
                ConfigurationKeys.Token);
            IGithubService githubService = new GithubService(
                getConfigResult.Config[ConfigurationKeys.Owner],
                getConfigResult.Config[ConfigurationKeys.Repository],
                getConfigResult.Config[ConfigurationKeys.Branch],
                getConfigResult.Config[ConfigurationKeys.Token]
            );

            var testObj = new
            {
                LastSynced = DateTime.Now.ToString("yyyy-MM-dd-HH-ss"),
                Id = Guid.NewGuid().ToString(),
                IsTest = true,
                Age = 10
            };

            #endregion

            #region Act

            var result = await githubService.CreateFileAsync(
                $"test-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.json", testObj.ToJson(),
                "test: test file create using octokit");

            #endregion

            #region Assert

            Assert.True(result.IsSuccess);

            #endregion
        }
    }
}