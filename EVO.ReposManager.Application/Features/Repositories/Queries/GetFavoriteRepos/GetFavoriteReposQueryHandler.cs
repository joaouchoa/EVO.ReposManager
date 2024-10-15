using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Application.Features.Repositories.Queries.GetRepoByName;
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
        private readonly GetFavoriteReposQueryValidator _validator;

        public GetFavoriteReposQueryHandler(IReposReadRepository repository, GetFavoriteReposQueryValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<GetFavoriteReposQueryResponse> Handle(GetFavoriteReposQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GetFavoriteReposQueryResponse(false, default, default, validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            var repositorieCount = await _repository.GetFavoriteReposCount();

            int totalCount = repositorieCount;
            int totalPages = (int)Math.Ceiling((double)totalCount / request.perPage);

            int page = request.page > totalPages ? totalPages : request.page;

            var repositories = await _repository.GetFavoriteRepos(page, request.perPage);

            if (repositories is null)
                return default;

            bool hasMorePages = page < totalPages;

            var SucessResponse = new GetFavoriteReposQueryResponse(hasMorePages, totalPages, repositories, default);
            return SucessResponse;
        }
    }
}
