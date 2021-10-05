﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG32_DetalleSolicitud
    {
        public int idSolicitud { get; set; }
        public int secuencia { get; set; }
        public string estadoDet { get; set; }
        public string tipo { get; set; }
        public string conceptoCab { get; set; }
        public int idConceptoDet { get; set; }
        public string conceptoDet { get; set; }
        public string moneda { get; set; }
        public int idEstadoDet { get; set; }

        public int cantDias { get; set; }
        public decimal valor { get; set; }
        public decimal totalSolicitado { get; set; }
        public decimal totalRendido { get; set; }
        public decimal totalEntregado { get; set; }
        public string simbolo { get; set; }

        public int idTipoComp { get; set; }
        public string serieNumero { get; set; }
        public int idRendCompte { get; set; }

        public int countCpte { get; set; }
    }
}