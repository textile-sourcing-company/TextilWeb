using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPS_Parametro
    {
        public int opcion { get; set; }
        public int idSolicitud { get; set; }
        public string codigo { get; set; }
        public int idEmpresa { get; set; }
        public int idEstado { get; set; }
        public int idAnulado { get; set; }
        public int idTipoMod { get; set; }
        public string observacion { get; set; }

        public int codCeCo { get; set; }
        public int idConceptoDet { get; set; }
        public decimal valor { get; set; }

        public int secuencia { get; set; }
        public string usuario { get; set; }
        public string usuarioCompleto { get; set; }
        public int nivelInterfaz { get; set; }
        
        public int idTipo { get; set; }
        public int seleccionadoDet { get; set; }
        public int idConceptoCab { get; set; }

        public int cantDias { get; set; }
        public decimal montoSolicitado { get; set; }
        public string observacionDet { get; set; }

        public string ctpava_bnf { get; set; }
        public string canvar_bnf { get; set; }
        public string colaborador { get; set; }
    }
}