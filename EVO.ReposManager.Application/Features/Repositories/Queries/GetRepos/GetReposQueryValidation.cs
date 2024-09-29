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
    public class GetReposQueryValidation : AbstractValidator<GetReposQuery>
    {
        public GetReposQueryValidation()
        {
            RuleFor(d => d.UserName)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE)
                
                .MinimumLength(Repo.MIN_LENGHT).WithMessage(RepoValidationMessages.MIN_LENGTH_ERROR_MESSAGE)
                
                .MaximumLength(Repo.MAX_LENGHT).WithMessage(RepoValidationMessages.MAX_LENGTH_ERROR_MESSAGE)
                
                .Matches("^[a-zA-Z0-9-]+$").WithMessage(RepoValidationMessages.MATCHES_ERROR_MESSAGE)
                
                .Must(name => !name.StartsWith("-") && !name.EndsWith("-"))
                .WithMessage(RepoValidationMessages.START_END_HYPHEN_ERROR_MESSAGE)

                .Must(name => !name.Contains("--"))
                .WithMessage(RepoValidationMessages.CONSECUTIVE_HYPHENS_ERROR_MESSAGE);
        }
    }
}
