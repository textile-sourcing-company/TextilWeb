using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG75_CptesRegistrados
    {
        public string numserie { get; set; }
        public string rs { get; set; }
        public decimal total { get; set; }
        public string simbolo { get; set; }
        public int idRendCompte { get; set; }
        public int idTipoComp { get; set; }
    }
}