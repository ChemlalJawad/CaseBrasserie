﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Application.Repositories.Commands.Grossistes
{
    public class UpdateStockCommand
    {
        public int BiereId { get; set; }
        public int GrossisteId { get; set; }
        public int Stock { get; set; }
    }
}