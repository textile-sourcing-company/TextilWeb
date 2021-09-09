using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class IngresoRectilineosCompletoEntidad
    {
        public PartidaTelaEntidad Partida { get; set; }
        public List<PartidaTelaRectilineoEntidad> PartidaRectilineos { get; set; }
        public List<PartidaRectilineoTallasEntidad> PartidaRectilineosTallas { get; set; }
        public List<TallasEntidad> Tallas { get; set; }


    }
}