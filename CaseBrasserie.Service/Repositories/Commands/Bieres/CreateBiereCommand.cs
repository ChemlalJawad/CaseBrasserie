using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Application.Repositories.Commands.Bieres
{
    public class CreateBiereCommand
    {
        public string Nom { get; set; }
        public float DegreAlcool { get; set; }
        public decimal Prix { get; set; }
        public int BrasserieId { get; set; }
    }
}
