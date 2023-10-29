using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Application.Repositories.Commands.Grossistes
{
    public class ItemCommand
    {
        public int BiereId { get; set; }
        public int Quantite { get; set; }
    }
}
