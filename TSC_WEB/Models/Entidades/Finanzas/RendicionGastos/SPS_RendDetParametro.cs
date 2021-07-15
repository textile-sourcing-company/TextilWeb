using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPS_RendDetParametro
    {
        public int opcion { get; set; }
        public int idRendCompteDet { get; set; }
        public int idRendCompte { get; set; }
        public DateTime fecha { get; set; }
        public string detalle1 { get; set; }
        public string detalle2 { get; set; }
        public decimal importe { get; set; }
        public string numPlanilla { get; set; }
    }
}