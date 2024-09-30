using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Domain.Entities;
using EVO.ReposManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Infrastructure.Repositories
{
    public class ReposWriteRepository : IReposWriteRepository
    {
        private readonly IReposReadRepository _reposReadRepository;
        private readonly ReposManagerContext _context;

        public ReposWriteRepository(IReposReadRepository reposReadRepository, ReposManagerContext context)
        {
            _reposReadRepository = reposReadRepository;
            _context = context;
        }

        public async Task<bool> CreateFavoriteRepo(Repo entity)
        {
            var exists = await _reposReadRepository.ExistsFavoriteRepoByIdAsync(entity.Id);
            
            if(exists)
                return false;

            var response = await _context.Repos.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFavoriteRepo(long id)
        {
            await _context.Repos.Where(d => d.Id == id)
                                    .ExecuteDeleteAsync();
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
