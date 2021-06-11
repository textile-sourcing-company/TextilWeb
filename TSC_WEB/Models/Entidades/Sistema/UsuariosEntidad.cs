using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Sistema
{
    public class UsuariosEntidad : EntidadMaestra
    {
        public string nombre { get; set; }
        public int codigo_cargo { get; set; }
        public string desc_cargo { get; set; }
        public int cod_funcionario { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string e_mail { get; set; }

        public string dni { get; set; }
        public string  ccosto { get; set; }
        public string FOTOSHECK { get; set; }
        public string pass { get; set; }
        public int accesos { get; set; }


    }
}