using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class FichasTallasEntidad
    {
        public int? idrectilineoficha { get; set; }
        public int realprimera { get; set; }
        public decimal pesonetoreal { get; set; }
        public decimal consumo { get; set; }


        public int ficha { get; set; }
        public int cantidad { get; set; }

        public int orden { get; set; }
        public string talla { get; set; }
        public int pedido { get; set; }
        public string estilotsc { get; set; }
        public string estilocliente { get; set; }
        public string color { get; set; }

        public int pendiente
        {
            get
            {
                return cantidad - realprimera;
            }
        }

        public decimal pesobrutotalla
        {
            get
            {
                return cantidad * consumo;
            }
        }

        public decimal pesobrutorealtalla
        {
            get
            {
                return realprimera * consumo;
            }
        }

    }
}