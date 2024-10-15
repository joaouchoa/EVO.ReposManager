using EVO.ReposManager.Application.Features.Repositories.DTOs;
using EVO.ReposManager.Domain.Entities;
using EVO.ReposManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Contracts
{
    public interface IReposWriteRepository
    {
        Task<ERepoCreationStatus> CreateFavoriteRepo(Repo repository);
        Task<bool> DeleteFavoriteRepo(long id);
    }
}
