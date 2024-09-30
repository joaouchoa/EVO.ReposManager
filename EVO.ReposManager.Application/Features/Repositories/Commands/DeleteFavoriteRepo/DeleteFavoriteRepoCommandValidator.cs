using EVO.ReposManager.Application.ValidationMessages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo
{
    public class DeleteFavoriteRepoCommandValidator : AbstractValidator<DeleteFavoriteRepoCommand>
    {
        public DeleteFavoriteRepoCommandValidator()
        {
            RuleFor(d => d.id)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE)
                .GreaterThan(0).WithMessage(RepoValidationMessages.ID_MATCHES_ERROR_MESSAGE);
        }
    }
}
