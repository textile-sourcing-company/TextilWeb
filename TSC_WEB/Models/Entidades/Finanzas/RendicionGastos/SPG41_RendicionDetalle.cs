using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG41_RendicionDetalle
    {

        public int idRendicionDet { get; set; }
        public string fecha { get; set; }
        public string detalle1 { get; set; }
        public string detalle2 { get; set; }
        public decimal importe { get; set; }
    }
}