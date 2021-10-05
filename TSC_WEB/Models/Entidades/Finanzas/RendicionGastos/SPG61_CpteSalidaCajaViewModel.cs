using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG61_CpteSalidaCajaViewModel
    {
        public decimal totalEntregado { get; set; }
        public string totalLetras { get; set; }
        public string colaborador { get; set; }
        public string observacion { get; set; }
        public string fecha { get; set; }
        public string dni { get; set; }
        public string usuarioAprob30 { get; set; }
        public string codigo { get; set; }
        public string simbolo { get; set; }
        public string serienumcpte { get; set; }
        public string sede { get; set; }
        public string ceco { get; set; }
        public string tipoDescripcion { get; set; }
        public int idTipo { get; set; }
    }
}