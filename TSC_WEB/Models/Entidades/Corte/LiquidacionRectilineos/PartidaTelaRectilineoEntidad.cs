using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class PartidaTelaRectilineoEntidad
    {
        public int idpartidarectilineo { get; set; }
        public string partidarectilineo { get; set; }
        public string tiporectilineo { get; set; }
        public string partidatela { get; set; }
        public string fechacarga { get; set; }
        public string fechareg { get; set; }
        public string usuarioreg { get; set; }
        public string usuariomod { get; set; }
        public string fechamod { get; set; }
        public string observacion { get; set; }

        public decimal kilostotales { get; set; }
        public decimal totalprendas { get; set; }
        public decimal consumototal {
            get {
                return totalprendas > 0 ? kilostotales / totalprendas : 0;
            }
        }




        //IDPARTIDARECTILINEO, PARTIDARECTILINEO, TIPORECTILINEO, PARTIDATELA,
        //FECHACARGA, FECHAREG, USUARIOREG, USUARIOMOD, FECHAMOD, OBSERVACION
    }
}