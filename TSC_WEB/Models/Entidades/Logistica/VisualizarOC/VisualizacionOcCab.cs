using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.VisualizacionOC
{
    public class VisualizacionOcCab
    {
        public int? I_ORDEM_COMPRA { get; set; }
        public string CH_PROVEEDOR { get; set; }
        
        public string CH_RUC { get; set; }
        public string I_OC_REFERENCIA { get; set; }
        public string CH_DIRECCION { get; set; }
        public string CH_CONTACTO { get; set; }
        public string CH_MONEDA { get; set; }
        public string CH_CONDICION_PAGO { get; set; }
        public string DT_FECHA_EMISION { get; set; }
        public string DT_FECHA_ENTREGA_PREV { get; set; }
        public string CH_FIRMANTE_01 { get; set; }
        public string CH_FIRMANTE_02 { get; set; }
        public string CH_FIRMANTE_03 { get; set; }
        public string CH_FIRMANTE_01_USU { get; set; }
        public string CH_FIRMANTE_02_USU { get; set; }
        public string CH_FIRMANTE_03_USU { get; set; }
        public string CH_USUARIO_SECCION { get; set; }
        public string CH_SUB_TOTAL { get; set; }
        public string CH_IGV { get; set; }
        public string CH_TOTAL { get; set; }
        public string CH_MONTO_LETRAS { get; set; }
        public string CH_REQUERIMIENTOS { get; set; }
        public string CH_CC_COSTO { get; set; }
        public string CH_OSERVACION { get; set; }
        public string CH_RUTA_BORRADOR { get; set; }

    }
}