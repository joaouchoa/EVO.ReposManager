﻿using EVO.ReposManager.Application.ValidationMessages;
using EVO.ReposManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Commands.CreateFavoriteRepo
{
    public static class CreateFavoriteRepoCommandStatusExtensions
    {
        public static string ToMessage(this ERepoCreationStatus status)
        {
            return status switch
            {
                ERepoCreationStatus.Success => RepoValidationMessages.REPO_CREATED_MESSAGE,
                ERepoCreationStatus.Failure => RepoValidationMessages.REPO_DONT_CREATED_ERROR_MESSAGE,
                ERepoCreationStatus.AlreadyExists => RepoValidationMessages.REPO_ALREADY_EXISTS_ERROR_MESSAGE,
                _ => RepoValidationMessages.REPO_ALREADY_EXISTS_ERROR_MESSAGE
            };
        }
    }
}
