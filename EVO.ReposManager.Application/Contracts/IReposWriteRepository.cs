using EVO.ReposManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Contracts
{
    public interface IReposWriteRepository
    {
        Task<bool> CreateFavoriteRepo(Repo repository);
        Task<bool> DeleteFavoriteRepo(long id);
    }
}
