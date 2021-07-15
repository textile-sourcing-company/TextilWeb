using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class LiquidacionRectilineosEntidad
    {
        public List<FichasTallasEntidad> FichaTallas { get; set; }
        public FichaDatos FichaCabecera { get; set; }

    }

}