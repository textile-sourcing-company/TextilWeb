using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG18_Historial
    {
        public int idSolicitud { get; set; }
        public string fecha { get; set; }
        public string codigo { get; set; }
        public string observacion { get; set; }
        public string estado { get; set; }
        public string tipo { get; set; }
        public string nivelAprobacion { get; set; }
        public string moneda { get; set; }

        public decimal totalsolicitud { get; set; }
        public decimal totalrendido { get; set; }
        public string ceco { get; set; }

        public int countCpte { get; set; }

        public string usuSolicitante { get; set; }
        public string fechaFinanza { get; set; }
        public string fechaRend { get; set; }
        public decimal totalentregado { get; set; }

        public string tipoSustento { get; set; }
        public string colaborador { get; set; }
    }
}