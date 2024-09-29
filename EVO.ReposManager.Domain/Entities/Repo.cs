using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Domain.Entities
{
    public class Repo
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }

        public const int MIN_LENGHT = 3;
        public const int MAX_LENGHT = 30;
    }
}
