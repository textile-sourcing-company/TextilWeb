﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG55_AprobFinalExcel
    {
        public int idSolicitud { get; set; }

        public int secuencia { get; set; }
        public string fecha { get; set; }
        public string codigo { get; set; }
        public string observacion { get; set; }
        public string estado { get; set; }
        public string tipo { get; set; }
        public string conceptoCab { get; set; }
        public string conceptoDet { get; set; }
        public string moneda { get; set; }

        public decimal totalsolicitado { get; set; }
        public decimal totalrendido { get; set; }

        public string serieNumero { get; set; }
        public string rucdni { get; set; }
        public string rs { get; set; }
        public string anexo { get; set; }

        public string htmlDisabledBtn { get; set; }
    }
}