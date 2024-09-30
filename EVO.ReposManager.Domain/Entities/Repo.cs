using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Domain.Entities
{
    public class Repo
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }

        public const int MAX_LENGHT = 39;

        public Repo() { } // Construtor padrão para permitir inicialização por propriedades

        public Repo(long id, string name, string description, string url, string language)
        {
            Id = id;
            Name = name;
            Description = description;
            Url = url;
            Language = language;
        }
    }
}
