using EVO.ReposManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public record GetRepoByNameQueryResponse(
           bool HasMorePages,
           int finalPage,
           List<Repo> Repositories,
           List<string> Errors
        );

}
