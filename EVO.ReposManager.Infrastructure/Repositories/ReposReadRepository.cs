using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Application.DTOs;
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

        public async Task<List<GitHubApiResponse>> GetRepositoriesByUserAsync(string username)
        {
            var apiUrl = _gitHubSettings.ApiUrl.Replace("username", username);

            // Configuração da solicitação para a API do GitHub
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("User-Agent", "SeuAppName"); // GitHub requer um User-Agent válido

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var gitHubResponse = JsonSerializer.Deserialize<List<GitHubApiResponse>>(jsonResponse);

                return gitHubResponse;
            }

            return new List<GitHubApiResponse>();
        }

        public async Task<bool> ExistsFavoriteRepoByIdAsync(long id)
        {
            return await _context.Repos.AnyAsync(e => e.Id == id);
        }
    }
}
