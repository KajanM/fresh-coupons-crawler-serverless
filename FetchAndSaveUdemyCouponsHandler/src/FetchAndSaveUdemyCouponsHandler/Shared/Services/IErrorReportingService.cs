using System.Threading.Tasks;

namespace FetchAndSaveUdemyCouponsHandler.Shared.Services;

public interface IErrorReportingService
{
   Task ReportAsync(string message, string url = null);
}