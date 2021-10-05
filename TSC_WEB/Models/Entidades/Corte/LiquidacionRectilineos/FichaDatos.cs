using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class FichaDatos
    {
        // DATOS TABLA
        public int? idrectilineohead { get; set; }
        public int? lote { get; set; }
        public string fechacrea { get; set; }
        public string usuariocrea { get; set; }

        public decimal mermarecorte { get; set; }
        public decimal mermahilos { get; set; }
        //public decimal cantdespachada { get; set; }


        // DATOS ERP
        public int? ficha { get; set; }
        public string combo { get; set; }
        public string partidarectilineo { get; set; }
        public string estilotsc { get; set; }
        public string pedidos { get; set; }
        public string estilocliente { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }


    }
}