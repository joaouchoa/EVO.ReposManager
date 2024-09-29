using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Domain.Entities;
using EVO.ReposManager.Infrastructure.IntegrationResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EVO.ReposManager.Infrastructure.Repositories
{
    public class ReposQueryRepository : IReposQueryRepository
    {
        private readonly HttpClient _httpClient;

        public ReposQueryRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Repo>> GetRepositoriesByUserAsync(string username)
        {
            var apiUrl = $"https://api.github.com/users/{username}/repos";

            // Configuração da solicitação para a API do GitHub
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("User-Agent", "SeuAppName"); // GitHub requer um User-Agent válido

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var gitHubResponses = JsonSerializer.Deserialize<List<GitHubApiResponse>>(jsonResponse);

                // Converter de GitHubResponse para Repo
                var repositories = gitHubResponses?.Select(gitHubRepo => new Repo
                {
                    Id = gitHubRepo.Id.ToString(),
                    Name = gitHubRepo.Name,
                    Description = gitHubRepo.Description,
                    Url = gitHubRepo.Url,
                    Language = gitHubRepo.Language
                }).ToList() ?? new List<Repo>();

                return repositories;
            }

            return new List<Repo>();
        }
    }
}
