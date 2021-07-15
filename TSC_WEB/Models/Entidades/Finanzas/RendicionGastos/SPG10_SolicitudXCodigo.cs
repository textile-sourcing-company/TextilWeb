using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG10_SolicitudXCodigo
    {
        public int idSolicitud { get; set; }
        public string codigo { get; set; }
        public string fecha { get; set; }
        public int idEmpresa { get; set; }
        public int idEstado { get; set; }
        public string estado { get; set; }
        public string usuSolicitante { get; set; }
        public int idTipoMod { get; set; }
        public string observacion { get; set; }
        public int idTipo { get; set; }
        public string ctpava_bnf { get; set; }
        public string canvar_bnf { get; set; }
        public string colaborador { get; set; }

        public List<SPG7_SolicitudDet> listaDetalle { get; set; }
    }
}