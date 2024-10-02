using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Application.DTOs;
using EVO.ReposManager.Application.Features.Repositories.Queries.GetFavoriteRepos;
using EVO.ReposManager.Application.Features.Repositories.Queries.GetRepositories;
using EVO.ReposManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public class GetRepoByNameQueryHandler : IRequestHandler<GetRepoByNameQuery, GetRepoByNameQueryResponse>
    {
        private readonly GetRepoByNameQueryValidation _validator;
        private readonly IReposReadRepository _repository;

        public GetRepoByNameQueryHandler(IReposReadRepository repository, GetRepoByNameQueryValidation validator)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<GetRepoByNameQueryResponse> Handle(GetRepoByNameQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GetRepoByNameQueryResponse(default, validationResult.Errors.Select(e => e.ErrorMessage).ToList(), false);

            var response = await _repository.GetByRepositoryByNameAsync(request.repoName, request.page, request.perPage);

            if (response is null || response.Repositories.Count == 0)
                return default;

            // Calcula o total de páginas
            int totalCount = response.TotalCount;
            int totalPages = (int)Math.Ceiling((double)totalCount / request.perPage);

            // Ajusta o número da página se for maior que o total
            int currentPage = request.page > totalPages ? totalPages : request.page;

            var repositories = response.Repositories.Select(repo => new Repo
            {
                Id = repo.Id,
                Name = repo.Name,
                Description = repo.Description,
                Url = repo.Url,
                Language = repo.Language
            }).ToList();

            // Verifica se há mais páginas
            bool hasMorePages = currentPage < totalPages;


            var SucessResponse = new GetRepoByNameQueryResponse(repositories, default, hasMorePages);
            return SucessResponse;
        }
    }
}
