using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Comercial.ReportePenalidades
{
    public class ReportePenalidadesEntidad
    {
        public string cctaco { get; set; }
        public string mes { get; set; }
        public string fecha { get; set; }
        public string cliente { get; set; }
        public string numerodocumento { get; set; }
        public string moneda { get; set; }
        public string ndocumento { get; set; }
        public string concepto { get; set; }
        public string descripcion { get; set; }
        public string centrocosto { get; set; }
        public string usuarioregistro { get; set; }
        //public string areacausantecentrodecosto { get; set; }
    }
}