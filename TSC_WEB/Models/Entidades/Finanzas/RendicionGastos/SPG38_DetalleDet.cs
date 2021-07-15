using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG38_DetalleDet
    {
        public string tipo { get; set; }
        public string conceptoCab { get; set; }
        public string moneda { get; set; }
        public decimal totalSolicitado { get; set; }
        public decimal totalEntregado { get; set; }
        public decimal totalRendido { get; set; }
    }
}