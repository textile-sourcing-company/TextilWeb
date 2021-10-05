using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG45_SolicitudDetalle
    {
        public int idSolicitud { get; set; }
        public int idRendCompte { get; set; }
        public int secuencia { get; set; }
        public int codCeCo { get; set; }
        public string serieNumero { get; set; }
        public string ceco { get; set; }
        public string conceptoCab { get; set; }
        public decimal totalcpte { get; set; }
        public string simbolo { get; set; }
        public int idTipoComp { get; set; }
        public int idConceptoCab { get; set; }
        public string disabledhtml { get; set; }

        public int idTipo { get; set; }
        public string conceptoDet { get; set; }
    }
}