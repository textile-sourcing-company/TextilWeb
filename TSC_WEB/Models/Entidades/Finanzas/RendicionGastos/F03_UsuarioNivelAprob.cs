using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class F03_UsuarioNivelAprob
    {
        public int idPermisoAprob { get; set; }
        public int idEmpresa { get; set; }
        public string codUsuario { get; set; }
        public int idNivelAprob { get; set; }
    }
}