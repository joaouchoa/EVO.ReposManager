using EVO.ReposManager.Application.DTOs;
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
        Task<List<GitHubApiResponse>> GetRepositoriesByUserAsync(string username);

        Task<bool> ExistsFavoriteRepoByIdAsync(long id);
    }
}
