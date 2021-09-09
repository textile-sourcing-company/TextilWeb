using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class PartidaTelaEntidad
    {
        public string partidatela { get; set; }
        public string codtela { get; set; }
        public string fechacarga { get; set; }
        public string fecharegistro { get; set; }
        public string usuarioregistro { get; set; }
        public string observacion { get; set; }
        public bool busqueda { get; set; }


        //PARTIDATELA, CODTELA, FECHACARGA, FECHAREGISTRO, USUARIOREGISTRO, OBSERVACION
    }
}