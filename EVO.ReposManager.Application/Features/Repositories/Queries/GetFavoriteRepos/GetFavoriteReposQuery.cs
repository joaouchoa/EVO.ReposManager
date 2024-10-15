using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetFavoriteRepos
{
    public record GetFavoriteReposQuery(int page, int perPage) : IRequest<GetFavoriteReposQueryResponse>;
}
