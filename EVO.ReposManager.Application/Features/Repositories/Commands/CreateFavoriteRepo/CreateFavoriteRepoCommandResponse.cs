using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Commands.CreateFavoriteRepo
{
    public record CreateFavoriteRepoCommandResponse(
            bool created,
            List<string> Errors
        );

}
