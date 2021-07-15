using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG23_BusquedaXCod
    {
        public int idSolicitud { get; set; }
        public string fecha { get; set; }
        public string codigo { get; set; }
        public string observacion { get; set; }
        public int idEstado { get; set; }
        public string estado { get; set; }
        public string moneda { get; set; }
        public decimal total { get; set; }
        public string tipo { get; set; }
        public string ceco { get; set; }
        public int codceco { get; set; }
        public string simbolo { get; set; }
        public int idTipoMod { get; set; }
        public string colaborador { get; set; }
        public string ctpava_bnf { get; set; }
        public string canvar_bnf { get; set; }

        public IEnumerable<SPG32_DetalleSolicitud> listaDetalle { get; set; }
    }
}   