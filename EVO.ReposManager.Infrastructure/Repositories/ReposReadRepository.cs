using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Application.DTOs;
using EVO.ReposManager.Application.Features.Repositories.DTOs;
using EVO.ReposManager.Domain.Entities;
using EVO.ReposManager.Infrastructure.Config;
using EVO.ReposManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EVO.ReposManager.Infrastructure.Repositories
{
    public class ReposReadRepository : IReposReadRepository
    {
        private readonly HttpClient _httpClient;
        private readonly GitHubSettings _gitHubSettings;
        private readonly ReposManagerContext _context;

        public ReposReadRepository(ReposManagerContext reposManagerContext, HttpClient httpClient, IOptions<GitHubSettings> gitHubSettings)
        {
            _context = reposManagerContext;
            _httpClient = httpClient;
            _gitHubSettings = gitHubSettings.Value;
        }

        private int ExtractLastPageNumber(string linkHeader)
        {
            if (string.IsNullOrWhiteSpace(linkHeader))
            {
                throw new ArgumentException("Link header não pode ser vazio");
            }

            var links = linkHeader.Split(',');

            foreach (var link in links)
            {
                if (link.Contains("rel=\"last\""))
                {
                    var lastPageUrl = link.Substring(0, link.IndexOf(";")).Trim('<', '>', ' ');
                    var pageParam = System.Web.HttpUtility.ParseQueryString(new Uri(lastPageUrl).Query).Get("page");

                    return int.Parse(pageParam);
                }
            }
            return default;
        }

        public async Task<GetRepoByOwnerGitHubResponse> GetRepositoriesByUserAsync(string username, int page, int perPage)
        {
            int lastPage = 1;
            var apiUrl = _gitHubSettings.ApiUrlGetByOnwer
                    .Replace("username", username)
                    .Replace("pagina", page.ToString())
                    .Replace("byRequest", perPage.ToString());

            // Configuração da solicitação para a API do GitHub
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("User-Agent", "ReposManager"); // GitHub requer um User-Agent válido

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                if (response.Headers.TryGetValues("Link", out var linkHeaders))
                {
                    var linkHeader = linkHeaders.FirstOrDefault();
                    lastPage = ExtractLastPageNumber(linkHeader);
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var gitHubResponse = JsonSerializer.Deserialize<List<RepositoryGitHubResponse>>(jsonResponse);

                if (lastPage == 0)
                    lastPage = page;

                return new GetRepoByOwnerGitHubResponse(lastPage, gitHubResponse);
            }

            return default;
        }

        public async Task<int> GetByRepositoryByNameCountAsync(string repositoryName) 
        {
            var url = _gitHubSettings.ApiUrlGetByRepoNameTotalCount.Replace("repositoryName", repositoryName);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("User-Agent", "ReposManager");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            var content = await response.Content.ReadAsStringAsync();

            int totalCount = 0;
            using (var jsonDoc = JsonDocument.Parse(content))
            {
                if (jsonDoc.RootElement.TryGetProperty("total_count", out var totalCountElement))
                {
                    totalCount = totalCountElement.GetInt32();
                }
            }

            return totalCount;
        }

        public async Task<GetRepoByNameGitHubResponse> GetByRepositoryByNameAsync(string repositoryName, int page, int perPage)
        {
            var apiUrl = _gitHubSettings.ApiUrlGetByRepoName
                    .Replace("repositoryName", repositoryName)
                    .Replace("pagina", page.ToString())
                    .Replace("byRequest", perPage.ToString());

            var pagedrequest = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            pagedrequest.Headers.Add("User-Agent", "ReposManagerPaged"); 

            var pagedResponse = await _httpClient.SendAsync(pagedrequest);

            if (!pagedResponse.IsSuccessStatusCode)
            {
                return default;
            }

            var pagedContent = await pagedResponse.Content.ReadAsStringAsync();

            var gitHubResponse = JsonSerializer.Deserialize<GetRepoByNameGitHubResponse>(pagedContent);


            var repositories = gitHubResponse.Repositories.Select(repo => new RepositoryGitHubResponse
            {
                Id = repo.Id,
                Name = repo.Name,
                Description = repo.Description,
                Url = repo.Url,
                Language = repo.Language,
                Owner = repo.Owner
            }).ToList();

            return new GetRepoByNameGitHubResponse(gitHubResponse.TotalCount, repositories);
        }

        public async Task<bool> ExistsFavoriteRepoByIdAsync(long id)
        {
            return await _context.Repos.AnyAsync(e => e.Id == id);
        }

        public async Task<int> GetFavoriteReposCount()
        {
            return await _context.Repos.CountAsync();
        }

        public async Task<List<Repo>> GetFavoriteRepos(int page, int perPage)
        {
            return await _context.Repos
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync();
        }
    }
}
