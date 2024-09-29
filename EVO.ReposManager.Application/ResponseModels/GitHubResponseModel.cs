using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.ResponseModels
{
    public class GitHubResponseModel
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Url { get; init; }
        public string Language { get; init; }
    }
}
