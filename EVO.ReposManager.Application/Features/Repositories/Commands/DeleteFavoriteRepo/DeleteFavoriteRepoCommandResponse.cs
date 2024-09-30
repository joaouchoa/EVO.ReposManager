using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo
{
    public record DeleteFavoriteRepoCommandResponse(
            bool created,
            List<string> Errors
        );
}
