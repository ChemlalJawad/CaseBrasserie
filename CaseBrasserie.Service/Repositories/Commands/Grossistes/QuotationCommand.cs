using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Application.Repositories.Commands.Grossistes
{
    public class QuotationCommand
    {
        public int GrossisteId { get; set; }
        public double PrixTotal { get; set; }
        public IEnumerable<ItemCommand> Items { get; set; } = new HashSet<ItemCommand>();
    }
}
