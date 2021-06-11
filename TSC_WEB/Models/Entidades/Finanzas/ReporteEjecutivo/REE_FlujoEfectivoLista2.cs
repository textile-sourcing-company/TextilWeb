using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.ReporteEjecutivo
{
    public class REE_FlujoEfectivoLista2
    {
        public int efIdTipoAct { get; set; }
        public string efActividad { get; set; }
        public string efCodEFx { get; set; }
        public string efDescCodEf { get; set; }
        public string efperiodo { get; set; }
        public int efperorden { get; set; }

        public decimal msoles { get; set; }
        public decimal mdolar { get; set; }
    }
}