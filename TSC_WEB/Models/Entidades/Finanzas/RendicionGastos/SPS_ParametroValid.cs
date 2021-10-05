using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas
{
    public class SPS_ParametroValid
    {
        public int opcion { get; set; }
        public int idSolicitud { get; set; }
        public string codigo { get; set; }
        public int secuencia { get; set; }
        public int idRendCompte { get; set; }
        
        public string nserie { get; set; }
        public string ndocof { get; set; }

        public string rucDNI { get; set; }
        public int idTipoComp { get; set; }
    }
}