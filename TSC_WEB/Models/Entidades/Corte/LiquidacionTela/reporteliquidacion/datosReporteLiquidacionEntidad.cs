using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela.reporteliquidacion
{
    public class datosReporteLiquidacionEntidad
    {
        // GENERALES
        public string fechaliquidacion { get; set; }
        public int turno { get; set; }
        public string celula { get; set; }
        public string usuario { get; set; }
        public int ficha { get; set; }
        public int pedido { get; set; }

        public string partida { get; set; }
        public int combo { get; set; }
        public int version { get; set; }

        public string programa { get; set; }
        public string estilo { get; set; }
        public string color { get; set; }
        public string codigotela { get; set; }
        public string descripciontela { get; set; }

        //public string color { get; set; }

        // DATOS STRING
        //public string consumonetorealstring { get; set; }

        // DATOS DE LA BD
        public double prendasprogramadas { get; set; }
        public double prendasliquidadas { get; set; }
        public double consumonetoreal { get; set; }
        //public double consumonetoreal { 
        //    get
        //    {
        //        return Convert.ToDouble(consumonetorealstring != null ? consumonetorealstring : "0");
        //    }
        //}



        public double consumobrutoprogramado { get; set; }
        public double consumobrutotizados { get; set; }
        public double consumobrutoreal { get; set; }
        public double telatendidakg { get; set; }
        public double telacollaretakg { get; set; }
        public double puntas { get; set; }
        public double retazos { get; set; }
        public double empalmes { get; set; }
        public double conos { get; set; }
        public double bolsas { get; set; }

        public double totalmermatendidoprogpor
        {
            get { return 0.02; }
        }

        public double totalmermatendidorealpor
        {
            get { return totalmermacombo / telarealcombo; }
        }

        public double telaadicional { get; set; }
        public double devolucionprimera { get; set; }
        public double devolucionsegunda { get; set; }
        public double telaprogramadakg { get; set; }
        public double telatizadakg { get; set; }
        public double teladespachadakg { get; set; }
        //public double adicional { get; set; }
        public double adicional { 
            get {
                return telaadicional;
            } 
        }

        public double teladespachadaadicional { 
            get {
                return teladespachadakg + adicional;
            } 
        }
        public double telaliquidada { get; set; }

        public double difteladespadictelaliqui { 
            get
            {
                return telaliquidada - teladespachadaadicional;
            }
        }
        
        public double porcentajeliquidacion
        {
            get
            {
                return teladespachadaadicional > 0 ? (telaliquidada / teladespachadaadicional) : 0;
            }
        }

        public double entrecorte { get; set; }
        public double orillos { get; set; }
        public double extremos { get; set; }
        public double totalmermacorte { 
            get
            {
                return entrecorte + orillos + extremos;
            }
        }
        public double eficienciaprogramadatizados { get; set; }
        public double eficienciarealtizados { get; set; }

        public double difefiprorealtizadospor { 
            get
            {
                return eficienciaprogramadatizados > 0 ? (eficienciaprogramadatizados - eficienciarealtizados) / eficienciaprogramadatizados : 0;
            }
        }
        public double anchototalprogramado { get; set; }
        public double anchototalreal { get; set; }
        public double difanchproreal { 
            get
            {
                return anchototalprogramado - anchototalreal;
            }
        }
        public double variacionancho
        {
            get
            {
                return anchototalprogramado > 0 ? difanchproreal / anchototalprogramado : 0;
            }
        }

        public double densidadprogramada { get; set; }
        public double densidadreal { get; set; }

        public double variaciondedensidad
        {
            get
            {
                return densidadprogramada > 0 ? (densidadreal - densidadprogramada) / densidadprogramada : 0;
            }
        }


        // DATOS CALCULADOS
        public double difprendasprog {
            get
            {
                return prendasprogramadas - prendasliquidadas;
            }
        }

        public double difprendasprogporc { 
            get
            {
                return prendasprogramadas > 0 ? (prendasliquidadas / prendasprogramadas) : 0;
            }
        }

        public double difconsbrutorealprogpor { 
            get
            {
                return consumobrutoprogramado > 0 ? (consumobrutoreal - consumobrutoprogramado) / consumobrutoprogramado : 0;                    
            }
        }

        public double totalmermatendido
        {
            get
            {
                return puntas + retazos + empalmes + conos + bolsas;
            }
        }

        public double totalmermacombo { get; set; }
        public double telarealcombo { get; set; }


        public double consumonetoprog
        {
            get
            {
                return (consumobrutoprogramado - (consumobrutoprogramado * totalmermatendidoprogpor));
            }
        }

        // DATOS AGREGADOS
        public double consumobrutocotizacion { get; set; }
        public double consumobrutoexplosion { get; set; }
        public string motivoadicional { get; set; }
        public string motivoprimera { get; set; }
        public string motivosegunda { get; set; }
        public double devolucionerp { get; set; }
        public string codigoerp { get; set; }
        public double eficienciacotizada { get; set; }
        public double eficienciacotizada_new { 
            get
            {
                return eficienciacotizada > 0 ? eficienciacotizada / 100 : 0;
            }
        }

        public double eficienciaexplosion { get; set; }
        public double anchototalcotizado { get; set; }
        public double anchototalexplosion { get; set; }
        public double densidadcotizacion { get; set; }
        public double densidadcotizacion_gramos
        {
            get
            {
                return densidadcotizacion != 0 ? densidadcotizacion / 1000 : 0;
            }
        }
        public double densidadexplosion { get; set; }
        public double densidadexplosion_gramos { 
            get
            {
                return densidadexplosion != 0 ? densidadexplosion / 1000 : 0;
            }
        }

        public double consumolinealcotizacion { get; set; }
        public double consumolinealexplosion { get; set; }
        public double consumolinealprogramado { get; set; }
        public double consumolinealreal { get; set; }


    }
}