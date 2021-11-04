using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;

namespace FetchAndSaveUdemyCouponsHandler.Shared.Services
{
    public interface IUdemyCouponProviderService
    {
        public string Name { get; }
        
        Task<List<UdemyUrlWithCouponCode>> GetCouponDataAsync(int startPageNo = 1, int numberOfPagesToCrawl = 5,
            HttpClient httpClient = null, IBrowsingContext browsingContext = null);
    }
}