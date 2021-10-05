﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG73_SolicitudDetalle
    {
        public string codigo { get; set; }
        public int secuencia { get; set; }
        public string fecha { get; set; }
        public string ceco { get; set; }
        public string tipo { get; set; }
        public string conceptoCab { get; set; }
        public string conceptoDet { get; set; }
        public string moneda { get; set; }

        public int cantDias { get; set; }
        public decimal totalsolicitado { get; set; }
        public decimal totalentregado { get; set; }
        public decimal totalrendido { get; set; }
        public decimal totalsaldo { get; set; }
        public string simbolo { get; set; }
    }
}