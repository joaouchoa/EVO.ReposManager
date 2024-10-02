using EVO.ReposManager.Application.ValidationMessages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public class GetRepoByNameQueryValidation : AbstractValidator<GetRepoByNameQuery>
    {
        public GetRepoByNameQueryValidation()
        {
            RuleFor(d => d.repoName)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE);

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
