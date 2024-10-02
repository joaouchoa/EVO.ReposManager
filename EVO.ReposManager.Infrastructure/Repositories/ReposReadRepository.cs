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

        public async Task<List<GetReposByOnwerGitHubResponse>> GetRepositoriesByUserAsync(string username)
        {
            var apiUrl = _gitHubSettings.ApiUrlGetByOnwer.Replace("username", username);

            // Configuração da solicitação para a API do GitHub
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("User-Agent", "ReposManager"); // GitHub requer um User-Agent válido

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var gitHubResponse = JsonSerializer.Deserialize<List<GetReposByOnwerGitHubResponse>>(jsonResponse);

                return gitHubResponse;
            }

            return new List<GetReposByOnwerGitHubResponse>();
        }

        public async Task<GetRepoByNameGitHubResponse> GetByRepositoryByNameAsync(string repositoryName, int page, int perPage)
        {
            //var url = $"https://api.github.com/search/repositories?q={repositoryName}&per_page=1";
            var url = _gitHubSettings.ApiUrlGetTotalCount.Replace("repositoryName", repositoryName);

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

            int totalPages = (int)Math.Ceiling((double)totalCount / perPage);

            if (page > totalPages && totalPages > 0)
            {
                page = totalPages;
            }

            var pagedUrl = $"https://api.github.com/search/repositories?q={repositoryName}&page={page}&per_page={perPage}";
            var apiUrl = _gitHubSettings.ApiUrlGetByRepoName
                    .Replace("username", repositoryName)
                    .Replace("pagina", page.ToString())
                    .Replace("byRequest", perPage.ToString());

            var pagedrequest = new HttpRequestMessage(HttpMethod.Get, pagedUrl);
            pagedrequest.Headers.Add("User-Agent", "ReposManagerPaged"); 

            var pagedResponse = await _httpClient.SendAsync(pagedrequest);

            if (!pagedResponse.IsSuccessStatusCode)
            {
                return default;
            }

            var pagedContent = await response.Content.ReadAsStringAsync();

            var gitHubResponse = JsonSerializer.Deserialize<GetRepoByNameGitHubResponse>(pagedContent);

            var repositories = gitHubResponse.Repositories.Select(repo => new GetReposByOnwerGitHubResponse
            {
                Id = repo.Id,
                Name = repo.Name,
                Description = repo.Description,
                Url = repo.Url,
                Language = repo.Language
            }).ToList();

            return new GetRepoByNameGitHubResponse(gitHubResponse.TotalCount, repositories);
        }

        public async Task<bool> ExistsFavoriteRepoByIdAsync(long id)
        {
            return await _context.Repos.AnyAsync(e => e.Id == id);
        }

        public async Task<List<Repo>> GetFavoriteRepos()
        {
            return await _context.Repos.ToListAsync();
        }
    }
}
