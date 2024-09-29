using EVO.ReposManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Contracts
{
    public interface IReposQueryRepository
    {
        Task<List<Repo>> GetRepositoriesByUserAsync(string username);
    }
}
