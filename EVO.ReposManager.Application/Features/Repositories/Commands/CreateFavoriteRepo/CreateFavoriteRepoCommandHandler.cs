using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Application.Features.Repositories.Queries.GetRepositories;
using EVO.ReposManager.Domain.Entities;
using EVO.ReposManager.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Commands.CreateFavoriteRepo
{
    public class CreateFavoriteRepoCommandHandler : IRequestHandler<CreateFavoriteRepoCommand, CreateFavoriteRepoCommandResponse>
    {
        private readonly IReposWriteRepository _repository;
        private readonly CreateFavoriteRepoCommandValidator _validator;

        public CreateFavoriteRepoCommandHandler(IReposWriteRepository repository, CreateFavoriteRepoCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CreateFavoriteRepoCommandResponse> Handle(CreateFavoriteRepoCommand request, CancellationToken cancellationToken)
        {
            var validatorResult = _validator.Validate(request);

            if (!validatorResult.IsValid)
                return new CreateFavoriteRepoCommandResponse(false, validatorResult.Errors.Select(e => e.ErrorMessage).ToList());

            var favorite = new Repo(request.Id, request.Name, request.Description, request.Url, request.Language, request.Owner);
            
            var result = await _repository.CreateFavoriteRepo(favorite);

            if (result != ERepoCreationStatus.Success)
                return new CreateFavoriteRepoCommandResponse(false, new List<string> { result.ToMessage() });

            return new CreateFavoriteRepoCommandResponse(true, default);
        }
    }
}
