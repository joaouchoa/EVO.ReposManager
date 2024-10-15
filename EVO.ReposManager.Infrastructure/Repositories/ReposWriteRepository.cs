using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Domain.Entities;
using EVO.ReposManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using EVO.ReposManager.Application.DTOs;
using EVO.ReposManager.Application.Features.Repositories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVO.ReposManager.Domain.Enums;

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

        public async Task<ERepoCreationStatus> CreateFavoriteRepo(Repo entity)
        {
            var exists = await _reposReadRepository.ExistsFavoriteRepoByIdAsync(entity.Id);

            if (exists) 
            {
                return ERepoCreationStatus.AlreadyExists;
            }

            await _context.Repos.AddAsync(entity);
            var success = await _context.SaveChangesAsync() > 0;

            if(!success)
                return ERepoCreationStatus.Failure;

            return ERepoCreationStatus.Success;
        }

        public async Task<bool> DeleteFavoriteRepo(long id)
        {
            var deletedCount = await _context.Repos
                 .Where(d => d.Id == id)
                 .ExecuteDeleteAsync();

            return deletedCount > 0;
        }
    }
}
