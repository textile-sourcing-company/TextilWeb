using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TSC_WEB.Models.Entidades.Gerencia.ReporteCambio
{
    public class Cambio
    {
        public string CLIENTE { get; set; }
        public string PEDIDO { get; set; }
        public string COLOR { get; set; }
        public decimal ANTIGUO_PRECIO { get; set; }
        public decimal NUEVO_PRECIO { get; set; }
        public string ESTADO { get; set; }
        public string USUARIO_SOLICITUD { get; set; }
        public string FECHA_SOLICITUD { get; set; }
        public string MOTIVO_SOLICITUD { get; set; }
        public string MOTIVO_APROBACION_RECHAZO { get; set; } 
    }
}