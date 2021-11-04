using System.Threading.Tasks;
using Amazon;
using FetchAndSaveUdemyCouponsHandler.Config;
using Xunit;
using static FetchAndSaveUdemyCouponsHandler.Function;

namespace FetchAndSaveUdemyCouponsHandler.Tests
{
    public class ParameterStoreConfigurationServiceTest
    {
        [Fact]
        public async Task ShouldBeAbleToGetConfigurationFromTheParameterStoreWithoutIssues()
        {
            #region Arrange

            var service = new ParameterStoreConfigurationService(RegionEndpoint.APSouth1);

            #endregion

            #region Act

            var getConfigResult = await service.GetAsync(ConfigurationKeys.Branch, ConfigurationKeys.Owner, ConfigurationKeys.Repository,
                ConfigurationKeys.Token);

            #endregion

            #region Assert

            Assert.True(getConfigResult.IsSuccess);
            Assert.NotEmpty(getConfigResult.Config[ConfigurationKeys.Branch]);
            Assert.NotEmpty(getConfigResult.Config[ConfigurationKeys.Owner]);
            Assert.NotEmpty(getConfigResult.Config[ConfigurationKeys.Repository]);
            Assert.NotEmpty(getConfigResult.Config[ConfigurationKeys.Token]);

            #endregion
        }
    }
}