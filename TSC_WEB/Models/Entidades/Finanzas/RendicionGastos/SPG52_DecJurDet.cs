using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG52_DecJurDet
    {
        public int idRendCompteDet { get; set; }
        public string fecha { get; set; }
        public string moneda { get; set; }
        public decimal monto { get; set; }
        public int seccion { get; set; }
    }
}