using System;
using System.Linq;
using System.Threading.Tasks;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;
using FetchAndSaveUdemyCouponsHandler.Shared.Helpers;
using Octokit;

namespace FetchAndSaveUdemyCouponsHandler.DataStore
{
    public class GithubService : IGithubService
    {
        private readonly GitHubClient _client;
        private readonly string _owner;
        private readonly string _repository;
        private readonly string _branch;

        public GithubService(string owner, string repository, string branch, string token)
        {
            _owner = owner;
            _repository = repository;
            _branch = branch;

            _client = new GitHubClient(new ProductHeaderValue(owner))
            {
                Credentials = new Credentials(token)
            };
        }

        public async Task<UpdateOrCreateGithubFileResult> UpdateOrCreateFileAsync(string path, string content, string message)
        {
            var result = new UpdateOrCreateGithubFileResult();
            try
            {
                var updateResult = await UpdateFileAsync(path, content, message);

                if (updateResult.IsSuccess)
                {
                    return new UpdateOrCreateGithubFileResult
                    {
                        IsSuccess = true,
                    };
                }
            }
            catch (Exception e)
            {
               LoggerUtils.Error($"An error occured while updating {path} on GitHub", e);
               result.AddError(e.Message);
            }
            
            try
            {
                var createResult = await CreateFileAsync(path, content, message);

                if (createResult.IsSuccess)
                {
                    return new UpdateOrCreateGithubFileResult
                    {
                        IsSuccess = true,
                    };
                }
            }
            catch (Exception e)
            {
               LoggerUtils.Error($"An error occured while creating {path} on GitHub", e);
               result.AddError(e.Message);
            }

            return result;
        }

        public async Task<UpdateGithubFileResult> UpdateFileAsync(string path, string content, string message)
        {
            var result = new UpdateGithubFileResult();
            RepositoryContent metaContent;
            try
            {
                metaContent = (await _client.Repository.Content.GetAllContentsByRef(_owner, _repository, path,
                    _branch)).FirstOrDefault();
            }
            catch (Exception e)
            {
                var msg = $"an error occured while fetching repository content from {_repository}/{path}";
                LoggerUtils.Error(msg, e);
                result.AddError(msg, e);
                return result;
            }

            if (metaContent == null)
            {
                result.AddError($"unable to determine the Sha of {path}");
                return result;
            }

            try
            {
                var updateChangeSet = await _client.Repository.Content.UpdateFile(_owner, _repository, path,
                    new UpdateFileRequest(message, content, metaContent.Sha)
                    {
                        Branch = _branch,
                    });
                LoggerUtils.Info(
                    $"successfully updated {path} and committed the changes (Sha: {updateChangeSet.Commit.Sha})");
                result.IsSuccess = true;
                return result;
            }
            catch (Exception e)
            {
                var msg = $"an error occured while updating the file with new content {path}";
                LoggerUtils.Error(msg, e);
                result.AddError(msg, e);
            }

            return result;
        }

        public async Task<CreateGithubFileResult> CreateFileAsync(string path, string content, string message)
        {
            var result = new CreateGithubFileResult();
            try
            {
                var createChangeSet = await _client.Repository.Content.CreateFile(_owner, _repository, path,
                    new CreateFileRequest(message, content)
                    {
                        Branch = _branch,
                    });
                LoggerUtils.Info(
                    $"successfully created file {path} and committed changes (Sha: {createChangeSet.Commit.Sha})");
                result.IsSuccess = true;
                return result;
            }
            catch (Exception e)
            {
                var msg = $"an error occured while creating file at {path}";
                LoggerUtils.Error(msg, e);
                result.AddError(msg, e);
            }

            return result;
        }
    }
    
    
    public class UpdateOrCreateGithubFileResult : BaseResult
    {
    }

    public class UpdateGithubFileResult : BaseResult
    {
    }

    public class CreateGithubFileResult : BaseResult
    {
    }
}