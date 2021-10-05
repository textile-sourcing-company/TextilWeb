using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG53_RpteCabIndividual
    {
        public string nombreEmpresa { get; set; }
        public string rucEmpresa { get; set; }
        public int idSolicitud { get; set; }
        public string codigo { get; set; }
        public string gerencia { get; set; }
        public int codceco { get; set; }
        public string ceco { get; set; }
        public string cargo { get; set; }
        public string fecha { get; set; }
        public string periodo { get; set; }

        public string nom_ape_jefe { get; set; }
        public string dni_jefe { get; set; }

        public string dni_trabajador { get; set; }
        public string nom_ape_trabajador { get; set; }

        public decimal totalsolicitado { get; set; }
        public decimal totalrendido { get; set; }
        public decimal totalsaldo { get; set; }

        public int idTipoMod { get; set; }
        public string tipo { get; set; }

        public string estado { get; set; }
        public decimal totalentregado { get; set; }

        public string firma_jefe { get; set; }
        public string firma_usuario { get; set; }
    }
}