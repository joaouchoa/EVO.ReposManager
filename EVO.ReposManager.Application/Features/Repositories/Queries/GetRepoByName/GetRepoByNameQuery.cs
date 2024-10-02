using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public record GetRepoByNameQuery(string repoName, int page, int perPage) : IRequest<GetRepoByNameQueryResponse>;
}
