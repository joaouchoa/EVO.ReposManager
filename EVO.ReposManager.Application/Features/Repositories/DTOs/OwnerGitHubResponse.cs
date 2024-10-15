using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.DTOs
{
    public record OwnerGitHubResponse
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }
    }
}
