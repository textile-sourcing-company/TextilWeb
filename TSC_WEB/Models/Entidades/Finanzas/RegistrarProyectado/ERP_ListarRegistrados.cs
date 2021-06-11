using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RegistrarProyectado
{
    public class ERP_ListarRegistrados
    {
        public int IdTipoAct { get; set; }
        public string Actividad { get; set; }
        public int IdReg { get; set; }
        public string CodEFx { get; set; }
        public string Concepto { get; set; }
        public string Periodo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public decimal monto { get; set; }

    }
}