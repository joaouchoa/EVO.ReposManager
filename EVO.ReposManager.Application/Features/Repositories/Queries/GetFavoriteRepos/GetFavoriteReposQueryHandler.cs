using EVO.ReposManager.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetFavoriteRepos
{
    public class GetFavoriteReposQueryHandler : IRequestHandler<GetFavoriteReposQuery, GetFavoriteReposQueryResponse>
    {
        private readonly IReposReadRepository _repository;

        public GetFavoriteReposQueryHandler(IReposReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetFavoriteReposQueryResponse> Handle(GetFavoriteReposQuery request, CancellationToken cancellationToken)
        {
            var repositories = await _repository.GetFavoriteRepos();

            if (repositories is null)
                return default;

            var SucessResponse = new GetFavoriteReposQueryResponse(repositories, default);
            return SucessResponse;
        }
    }
}
