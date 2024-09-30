using EVO.ReposManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Infrastructure.Config
{
    public class FavoriteReposConfig : IEntityTypeConfiguration<Repo>
    {
        public void Configure(EntityTypeBuilder<Repo> builder)
        {
            builder.ToTable("Repositories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Repo.MAX_LENGHT);

            builder.Property(x => x.Url)
                .IsRequired();

            builder.Property(x => x.Description);

            builder.Property(x => x.Language);
        }
    }
}
