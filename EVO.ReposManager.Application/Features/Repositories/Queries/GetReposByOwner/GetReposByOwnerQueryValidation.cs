using EVO.ReposManager.Application.ValidationMessages;
using EVO.ReposManager.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetRepositories
{
    public class GetReposByOwnerQueryValidation : AbstractValidator<GetReposByOwnerQuery>
    {
        public GetReposByOwnerQueryValidation()
        {
            RuleFor(d => d.UserName)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE)

                .MaximumLength(Repo.MAX_LENGHT).WithMessage(RepoValidationMessages.MAX_LENGTH_ERROR_MESSAGE)
                
                .Matches("^[a-zA-Z0-9-]+$").WithMessage(RepoValidationMessages.MATCHES_ERROR_MESSAGE)
                
                .Must(name => !name.StartsWith("-") && !name.EndsWith("-"))
                .WithMessage(RepoValidationMessages.START_END_HYPHEN_ERROR_MESSAGE)

                .Must(name => !name.Contains("--"))
                .WithMessage(RepoValidationMessages.CONSECUTIVE_HYPHENS_ERROR_MESSAGE);

            RuleFor(d => d.page)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE)
                .GreaterThan(0).WithMessage(RepoValidationMessages.NEGATIVE_NUMBER_ERROR_MESSAGE);

            RuleFor(d => d.perPage)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE)
                .GreaterThan(0).WithMessage(RepoValidationMessages.NEGATIVE_NUMBER_ERROR_MESSAGE)
                .LessThan(31).WithMessage(RepoValidationMessages.LIMITE_PER_PAGE_ERROR_MESSAGE);
        }
    }
}
