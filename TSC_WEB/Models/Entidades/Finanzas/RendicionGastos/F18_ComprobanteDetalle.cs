﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class F18_ComprobanteDetalle
    {
        public int idRendCompteDet { get; set; }
        public string fecha { get; set; }
        public string detalle1 { get; set; }
        public string detalle2 { get; set; }
        public string codum { get; set; }
        public decimal valorunit { get; set; }
        public int cantidad { get; set; }
    }
}