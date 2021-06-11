using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class F07_Archivos
    {
        public int idSolicitud { get; set; }
        public string descripcion { get; set; }
        public string nombreArchivo { get; set; }
        public string rutaWeb { get; set; }
    }
}