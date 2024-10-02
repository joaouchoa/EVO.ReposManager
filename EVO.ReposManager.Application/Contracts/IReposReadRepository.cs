﻿using EVO.ReposManager.Application.DTOs;
using EVO.ReposManager.Application.Features.Repositories.DTOs;
using EVO.ReposManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Contracts
{
    public interface IReposReadRepository
    {
        Task<List<GetReposByOnwerGitHubResponse>> GetRepositoriesByUserAsync(string username);

        Task<GetRepoByNameGitHubResponse> GetByRepositoryByNameAsync(string repoName, int page, int perPage);

        Task<bool> ExistsFavoriteRepoByIdAsync(long id);

        Task<List<Repo>> GetFavoriteRepos();

    }
}
