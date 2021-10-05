using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG81_CpteDetalle
    {
        public int idRendCompteDet { get; set; }
        public string fecha { get; set; }
        public string fechaBD { get; set; }
        public string detalle1 { get; set; }
        public string detalle2 { get; set; }
        public int cantidad { get; set; }
        public string codum { get; set; }
        public decimal valorunit { get; set; }
        public decimal porcentajeIGV { get; set; }
        public decimal total { get; set; }
        public string um { get; set; }
        public int seccion { get; set; }
        public string conceptoDet { get; set; }

        public int idConceptoDet { get; set; }
        public int idConceptoCab { get; set; }
        public int idSolicitud { get; set; }
        public int idTipo { get; set; }
    }
}