using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Entidades.Logistica
{
    public class SegAvioProgEntidad : EntidadMaestra
    {
            public string Cliente { get; set; }
            public string Fecha_creacion_pedido { get; set; }
            public string Fecha_Exfactory { get; set; }
            public string Programa { get; set; }
            public string Estilo_cliente { get; set; }
            public string Pedido { get; set; }
            public string Fecha_emision_req { get; set; }
            public string Requerimiento { get; set; }
            public string Po { get; set; }
            public string Fecha_asig_avio { get; set; }
            public string Codigo_avio { get; set; }
            public string Descripcion_avio { get; set; }
            public string Orden_compra { get; set; }
            public string Fecha_Emision_Oc { get; set; }
            public string Fecha_compromiso_Oc { get; set; }
            public string Cantidad_req_programa { get; set; }
            public string Cantidad_requerimiento { get; set; }
            public string Cantidad_Oc { get; set; }
            public string Cantidad_Recibida { get; set; }
            public string Fecha_Ingreso_alm { get; set; }
            public string Periodo_booking { get; set; }
            public string Orden_Planeamiento { get; set; }

    }
}