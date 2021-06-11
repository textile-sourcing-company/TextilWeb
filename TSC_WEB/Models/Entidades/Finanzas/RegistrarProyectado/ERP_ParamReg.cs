using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RegistrarProyectado
{
    public class ERP_ParamReg
    {
        public int idRSx { get; set; }
        public int idTipoPeriodo { get; set; }
        public int idPeriodo { get; set; }
        public decimal monto { get; set; }
    }
}