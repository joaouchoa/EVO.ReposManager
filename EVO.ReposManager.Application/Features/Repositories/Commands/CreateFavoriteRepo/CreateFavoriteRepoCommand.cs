using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Commands.CreateFavoriteRepo
{
    public record CreateFavoriteRepoCommand(
            long Id,
            string Name,
            string Description,
            string Url,
            string Language) : IRequest<CreateFavoriteRepoCommandResponse>;
}
