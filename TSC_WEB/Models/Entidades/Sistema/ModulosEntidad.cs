using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Sistema
{
    public class ModulosEntidad
    {
        public int codcargo { get; set; }
        public int idtag { get; set; }
        public int codopcion { get; set; }

        public string nombretag { get; set; }
        public string detnombretag { get; set; }
        public string ruta { get; set; }
        public string icono { get; set; }
        public string tipoopcion { get; set; }

        public string idsubmodulo { get; set; }

        public int orden { get; set; }
        public string modulopadre { get; set; }
        public string nombresubmodulo { get; set; }

    }
}