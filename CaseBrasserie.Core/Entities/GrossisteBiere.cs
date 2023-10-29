using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Core.Entities
{
    public class GrossisteBiere
    {
        public int BiereId { get; set; }
        public Biere Biere { get; set; }

        public int GrossisteId { get; set; }
        public Grossiste Grossiste { get; set; }

        public int Stock { get; set; }
    }

}
