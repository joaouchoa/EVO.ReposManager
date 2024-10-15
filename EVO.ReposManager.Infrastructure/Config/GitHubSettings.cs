using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Infrastructure.Config
{
    public class GitHubSettings
    {
        public string ApiUrlGetByOnwer { get; set; }
        public string ApiUrlGetByRepoName { get; set; }
        public string ApiUrlGetTotalCount { get; set; }
        public string ApiUrlGetByRepoNameTotalCount { get; set; }
    }
}
