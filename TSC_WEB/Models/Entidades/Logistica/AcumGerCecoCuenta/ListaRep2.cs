using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AcumGerCecoCuenta
{
    public class ListaRep2
    {
        //public string PERIODO { get; set; }
        public string GERENCIA { get; set; }
        public string SIMBOLO { get; set; }
        public string CODCC { get; set; }
        public string CC { get; set; }
        public string PRESUPUESTO { get; set; }
        public string CONSUMIDO { get; set; }
        public string DISPONIBLE { get; set; }

        public string PRESUPUESTO_TOTAL { get; set; }
        public string CONSUMIDO_TOTAL { get; set; }
        public string DISPONIBLE_TOTAL { get; set; }
    }
}