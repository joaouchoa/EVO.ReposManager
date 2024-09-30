using EVO.ReposManager.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Infrastructure.Context
{
    public class ReposManagerContext : DbContext
    {
        public ReposManagerContext()
        {

        }

        public ReposManagerContext(DbContextOptions<ReposManagerContext> options) : base(options)
        {

        }

        public DbSet<Repo> Repos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReposManagerContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
