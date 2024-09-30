using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Application.Features.Repositories.Commands.CreateFavoriteRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo
{
    public class DeleteFavoriteRepoCommandHandler : IRequestHandler<DeleteFavoriteRepoCommand, DeleteFavoriteRepoCommandResponse>
    {
        private readonly IReposWriteRepository _repository;
        private readonly DeleteFavoriteRepoCommandValidator _validator;

        public DeleteFavoriteRepoCommandHandler(IReposWriteRepository repository, DeleteFavoriteRepoCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<DeleteFavoriteRepoCommandResponse> Handle(DeleteFavoriteRepoCommand request, CancellationToken cancellationToken)
        {
            var validatorResult = _validator.Validate(request);

            if (!validatorResult.IsValid)
                return new DeleteFavoriteRepoCommandResponse(false, validatorResult.Errors.Select(e => e.ErrorMessage).ToList());

            var response = await _repository.DeleteFavoriteRepo(request.id);

            if (!response)
                return default;

            return new DeleteFavoriteRepoCommandResponse(true, default);
        }
    }
}
