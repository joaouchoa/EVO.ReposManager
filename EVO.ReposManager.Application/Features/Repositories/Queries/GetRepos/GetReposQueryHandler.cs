using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetRepositories
{
    public class GetReposQueryHandler : IRequestHandler<GetReposQuery, GetReposQueryResponse>
    {
        private readonly IReposQueryRepository _repository;

        public GetReposQueryHandler(IReposQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetReposQueryResponse> Handle(GetReposQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserName))
                return default;

            var repos = await _repository.GetRepositoriesByUserAsync(request.UserName);

            if (repos is null)
                return default;

            var list = new GetReposQueryResponse(repos);

            return list;
        }
    }
}
