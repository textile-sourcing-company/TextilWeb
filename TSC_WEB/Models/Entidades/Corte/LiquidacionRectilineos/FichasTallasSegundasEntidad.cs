using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class FichasTallasSegundasEntidad
    {
        public string talla { get; set; }
        public decimal realsegunda { get; set; }
        public decimal pesonetorealsegunda { get; set; }
        public int ordentalla { get; set; }
        public decimal programadosegunda { get; set; }
        public decimal? idrectilineotsegunda { get; set; }

        // DATOS CALCULADOS
        public decimal pendientes
        {
            get
            {
                return programadosegunda - realsegunda;
            }
        }



    }
}