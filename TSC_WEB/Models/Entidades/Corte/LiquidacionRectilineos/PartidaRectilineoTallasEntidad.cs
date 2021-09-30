using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class PartidaRectilineoTallasEntidad
    {

        public int idpartrectitalla { get; set; }
        public int idpartidarectilineo { get; set; }
        public int orden { get; set; }
        public string talla { get; set; }
        public decimal cantidadprimera{ get; set; }
        public decimal cantidadsegunda { get; set; }

        // TOTAL DE CANTIDADES (PRIMERAS + SEGUNDAS)
        public decimal totalcantidades {
            get {
                return cantidadprimera + cantidadsegunda;
            }
        }

        public decimal consumokilos {get;set; }

        public decimal kgportalla
        {
            get
            {
                return totalcantidades * consumokilos;
            }
        }

        //IDPARTRECTITALLA, IDPARTIDARECTILINEO, TALLA, CANTIDADPRIMERA, CANTIDADSEGUNDA
    }
}