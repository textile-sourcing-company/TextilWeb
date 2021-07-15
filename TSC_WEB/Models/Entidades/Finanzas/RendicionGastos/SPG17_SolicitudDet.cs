using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG17_SolicitudDet
    {
        public int idSolicitud { get; set; }
        public string codigo { get; set; }
        public int secuencia { get; set; }
        public string fecha { get; set; }
        public string ceco { get; set; }
        public string estadoDet { get; set; }
        public string conceptoDet { get; set; }
        public string moneda { get; set; }
        public decimal valor { get; set; }
        public string colorBgRow { get; set; }
        public string conceptoCab { get; set; }
        public string tipo { get; set; }
        public int cantDias { get; set; }
        public string simbolo { get; set; }
        public decimal total { get; set; }
    }
}