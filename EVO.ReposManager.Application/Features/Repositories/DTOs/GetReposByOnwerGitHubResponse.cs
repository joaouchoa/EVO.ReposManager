using EVO.ReposManager.Application.Features.Repositories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.DTOs
{
    public record GetReposByOnwerGitHubResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("description")]
        public string Description { get; init; }

        [JsonPropertyName("html_url")]
        public string Url { get; init; }

        [JsonPropertyName("language")]
        public string Language { get; init; }

        [JsonPropertyName("owner")]
        public Owner Owner { get; init; } // Aqui está o objeto Owner
    }
}
