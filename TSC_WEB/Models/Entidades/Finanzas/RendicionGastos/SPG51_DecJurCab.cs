using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG51_DecJurCab
    {
        public string nomapellidos { get; set; }
        public string dni { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
        public string fechaDesde { get; set; }
        public string fechaHasta { get; set; }
        public string ceco { get; set; }

        public string diaDesde { get; set; }
        public string mesDesde { get; set; }
        
        public string diaHasta { get; set; }
        public string mesHasta { get; set; }
        public string anioHasta { get; set; }

        public int dia { get; set; }
        public string mes { get; set; }
        public int anio { get; set; }

        public string dni_jefe { get; set; }
        public string nom_ape_jefe { get; set; }

        public string parrafo1 { get; set; }
        public string parrafo2 { get; set; }

        public string cargo { get; set; }
        public string colaborador { get; set; }

        public string firma_jefe { get; set; }
        public string firma_usuario { get; set; }
    }
}