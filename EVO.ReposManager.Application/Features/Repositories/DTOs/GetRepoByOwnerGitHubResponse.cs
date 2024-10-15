using EVO.ReposManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.DTOs
{
    public record GetRepoByOwnerGitHubResponse(
        [property: JsonPropertyName("total_count")] int finalPage,
        [property: JsonPropertyName("items")] List<RepositoryGitHubResponse> Repositories
    );
}
