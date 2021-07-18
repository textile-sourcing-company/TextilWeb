using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela.reporteliquidacion
{
    public class datosgeneralesEntidad
    {
        public string partida { get; set; }
        public int combo { get; set; }
        public int version { get; set; }
        public string tipotela { get; set; }


        public double anchotelaprogramado { get; set; }
        public double anchotelareal { get; set; }
        public double densidadprogramado { get; set; }
        public double densidadreal { get; set; }
        public double telaprogramado { get; set; }
        public double telaprogramado_new { get; set; }
        public double telareal { get; set; }
        public double eficienciatizadosprogramado { get; set; }
        public double devolucionesprimera { get; set; }
        public double devolucionessegunda { get; set; }
        public double mermasentrecorte { get; set; }
        public double adicional { get; set; }
        public double collaretas { get; set; }
        public double puntasmermas { get; set; }
        public double retazosmas { get; set; }
        public double retazosmen { get; set; }
        public double empalmes { get; set; }
        public double conos { get; set; }
        public double bolsas { get; set; }
        public double totalmetrosbruto { get; set; }
        public double totalkilosbruto { get; set; }
        public double consumotizadosgeneral { get; set; }

        public string estadotendido { get; set; }
        public string estadocorte { get; set; }

        public string comentario { get; set; }


        //CALCULADOS
        public double anchoteladiferencia
        {
            get
            {
                return (anchotelareal - anchotelaprogramado) / anchotelaprogramado;
            }
        }

        public double teladiferencia
        {
            get
            {
                return (telareal - telaprogramado_new);
            }
        }

        public double eficienciatizadosreal
        {
            get
            {
                return 1 - (mermasentrecorte  / telareal);
            }
        }

        public double eficienciatizadosdiferencia
        {
            get
            {
                return ((eficienciatizadosprogramado / 100) - eficienciatizadosreal) / (eficienciatizadosprogramado / 100);
            }
        }

        public double devolucionesporcentaje
        {
            get
            {
                return (devolucionesprimera + devolucionessegunda) / telareal;
            }
        }

        public double totalmerma
        {
            get
            {
                return (puntasmermas + retazosmas + retazosmen + empalmes + conos + bolsas);
            }
        }

        public double mermastendido
        {
            get
            {
                return (totalmerma / telareal);
            }
        }
    }
}