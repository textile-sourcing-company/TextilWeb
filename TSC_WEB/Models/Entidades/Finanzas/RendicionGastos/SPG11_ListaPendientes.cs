﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG11_ListaPendientes
    {
        public int idSolicitud { get; set; }
        public string fecha { get; set; }
        public string codigo { get; set; }
        public string observacion { get; set; }
        public string estado { get; set; }
        public string tipo { get; set; }
        public string moneda { get; set; }
        public string nivelAprobacion { get; set; }
        public decimal total { get; set; }
    }
}