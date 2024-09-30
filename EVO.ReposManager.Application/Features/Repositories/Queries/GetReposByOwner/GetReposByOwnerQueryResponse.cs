using EVO.ReposManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetRepositories
{
    public record GetReposByOwnerQueryResponse(
           List<Repo> Repositories,
           List<string> Errors
        );
}
