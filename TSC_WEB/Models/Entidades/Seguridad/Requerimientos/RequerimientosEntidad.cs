using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Seguridad.Requerimientos
{
    public class RequerimientosEntidad
    {

            public string IDREQUERIMIENTO { get; set; }
            public string REQUERIMIENTO { get; set; }
            public string FECHA_REQ { get; set; }
            public string SOLICITANTE { get; set; }
            public string NOMBRE_AREA { get; set; }
            public string DESC_GER { get; set; }
            public string RESPONSABLE { get; set; }
            public string FECHA_INI_REQ { get; set; }
            public string FECHA_FIN_REQ { get; set; }
            public string FECHA_INICIO_REAL { get; set; }
            public string OBSERVACIONES { get; set; }
            public string ESTADO_REQ { get; set; }

    }
}