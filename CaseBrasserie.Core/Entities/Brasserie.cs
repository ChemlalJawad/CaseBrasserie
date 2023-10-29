using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Core.Entities
{
    public class Brasserie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<Biere> Bieres { get; set; } = new HashSet<Biere>();
    }

}
