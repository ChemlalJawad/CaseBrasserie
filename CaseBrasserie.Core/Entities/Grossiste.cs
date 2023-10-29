using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Core.Entities
{
    public class Grossiste
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<GrossisteBiere> GrossistesBieres { get; set; } = new HashSet<GrossisteBiere>();
    }

}
