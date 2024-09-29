using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetRepositories
{
    public record GetReposQuery(string UserName) : IRequest<GetReposQueryResponse>;
}
