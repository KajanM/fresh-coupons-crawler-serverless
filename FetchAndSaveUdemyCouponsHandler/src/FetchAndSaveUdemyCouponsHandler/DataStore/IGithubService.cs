using System.Threading.Tasks;

namespace FetchAndSaveUdemyCouponsHandler.DataStore
{
    public interface IGithubService
    {
        Task<UpdateGithubFileResult> UpdateFileAsync(string path, string content, string message);
        
        Task<CreateGithubFileResult> CreateFileAsync(string path, string content, string message);
    }
}