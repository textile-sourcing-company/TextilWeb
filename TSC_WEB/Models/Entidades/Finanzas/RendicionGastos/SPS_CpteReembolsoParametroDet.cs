using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPS_CpteReembolsoParametroDet
    {
        public int idSolicitud { get; set; }
        public int idConcepCab { get; set; }
        public int idConcepDet { get; set; }
        public string conceptoDet { get; set; }
        public int iDetalle { get; set; }
        public string fechaBD { get; set; }
        public string detalle1 { get; set; }
        public string detalle2 { get; set; }
        public string codum { get; set; }
        public decimal valorunit { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public int seccion { get; set; }
        public int codCeCo { get; set; }
        public int secuencia { get; set; }
        public int seleccionadoDet { get; set; }
        public int cantDias { get; set; }
        public decimal montoSolicitado { get; set; }
        public int idTipo { get; set; }
        
    }
}