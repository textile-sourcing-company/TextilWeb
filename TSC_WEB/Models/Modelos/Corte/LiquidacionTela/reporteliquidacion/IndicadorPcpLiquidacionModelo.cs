using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela.reporteliquidacion;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela.reporteliquidacion
{
    public class IndicadorPcpLiquidacionModelo
    {
        DBAccess conexion = new DBAccess();

        // REPORTE PCP 
        public IndicadorPcpLiquidacionEntidad getReportePCP(string partida, int combo, int version, string tipotela)
        {

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GET_VALIDACIONPCP_LIQUIDACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_partida", partida));
            comando.Parameters.Add(new OracleParameter("i_combo",   combo));
            comando.Parameters.Add(new OracleParameter("i_version", version));
            comando.Parameters.Add(new OracleParameter("i_tipotela",   tipotela));

            //CURSOR DATOS GENERALES
            comando.Parameters.Add(new OracleParameter("o_datageneral", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            //CURSOR DATOS FICHA
            comando.Parameters.Add(new OracleParameter("o_dataficha", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            //CURSOR DATOS TENDIDO (ETAPAS)
            comando.Parameters.Add(new OracleParameter("o_datatendido", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            DataSet resultado = new DataSet();
            OracleDataAdapter adapter = new OracleDataAdapter(comando);
            adapter.Fill(resultado);
            conexion.Desconectar();

            // OBJETO QUE DEVOLVEREMOS
            IndicadorPcpLiquidacionEntidad objRetornar = new IndicadorPcpLiquidacionEntidad();

            // OBJETOS QUE DEVOLVEREMOS (CURSORES)
            datosgeneralesEntidad objDataGeneral = new datosgeneralesEntidad();
            List<datosfichaEntidad> objDataFichas = new List<datosfichaEntidad>();
            List<datostendidoEntidad> objDataTendido = new List<datostendidoEntidad>();

            //CONVIRTIENDO PRIMERA LISTA (DATOS GENERALES)
            objDataGeneral.partida  = resultado.Tables[0].Rows[0]["partida"].ToString();
            objDataGeneral.combo    = Convert.ToInt16(resultado.Tables[0].Rows[0]["combo"].ToString());
            objDataGeneral.version  = Convert.ToInt16(resultado.Tables[0].Rows[0]["version"].ToString());
            objDataGeneral.tipotela = resultado.Tables[0].Rows[0]["tipotela"].ToString();


            objDataGeneral.anchotelaprogramado = Convert.ToDouble(resultado.Tables[0].Rows[0]["anchotelaprogramado"].ToString());
            objDataGeneral.anchotelareal = Convert.ToDouble(resultado.Tables[0].Rows[0]["anchotelareal"].ToString());
            objDataGeneral.densidadprogramado = Convert.ToDouble(resultado.Tables[0].Rows[0]["densidadprogramado"].ToString());
            objDataGeneral.densidadreal = Convert.ToDouble(resultado.Tables[0].Rows[0]["densidadreal"].ToString());
            objDataGeneral.telaprogramado = Convert.ToDouble(resultado.Tables[0].Rows[0]["telaprogramado"].ToString());
            objDataGeneral.telaprogramado_new = Convert.ToDouble(resultado.Tables[0].Rows[0]["telaprogramado_new"].ToString());

            objDataGeneral.telareal = Convert.ToDouble(resultado.Tables[0].Rows[0]["telareal"].ToString());
            objDataGeneral.eficienciatizadosprogramado = Convert.ToDouble(resultado.Tables[0].Rows[0]["eficienciatizadosprogramado"].ToString());
            objDataGeneral.devolucionesprimera = Convert.ToDouble(resultado.Tables[0].Rows[0]["devolucionesprimera"].ToString());
            objDataGeneral.devolucionessegunda = Convert.ToDouble(resultado.Tables[0].Rows[0]["devolucionessegunda"].ToString());
            objDataGeneral.mermasentrecorte = Convert.ToDouble(resultado.Tables[0].Rows[0]["mermasentrecorte"].ToString());
            objDataGeneral.adicional = Convert.ToDouble(resultado.Tables[0].Rows[0]["adicional"].ToString());
            objDataGeneral.collaretas = Convert.ToDouble(resultado.Tables[0].Rows[0]["collaretas"].ToString());
            objDataGeneral.puntasmermas = Convert.ToDouble(resultado.Tables[0].Rows[0]["puntasmermas"].ToString());
            objDataGeneral.retazosmas = Convert.ToDouble(resultado.Tables[0].Rows[0]["retazosmas"].ToString());
            objDataGeneral.retazosmen = Convert.ToDouble(resultado.Tables[0].Rows[0]["retazosmen"].ToString());
            objDataGeneral.empalmes = Convert.ToDouble(resultado.Tables[0].Rows[0]["empalmes"].ToString());
            objDataGeneral.conos = Convert.ToDouble(resultado.Tables[0].Rows[0]["conos"].ToString());
            objDataGeneral.bolsas = Convert.ToDouble(resultado.Tables[0].Rows[0]["bolsas"].ToString());
            objDataGeneral.totalmetrosbruto = Convert.ToDouble(resultado.Tables[0].Rows[0]["totalmetrosbruto"].ToString());
            objDataGeneral.totalkilosbruto = Convert.ToDouble(resultado.Tables[0].Rows[0]["totalkilosbruto"].ToString());
            objDataGeneral.consumotizadosgeneral = Convert.ToDouble(resultado.Tables[0].Rows[0]["consumotizadosgeneral"].ToString());
            objDataGeneral.estadotendido = resultado.Tables[0].Rows[0]["estadotendido"].ToString();
            objDataGeneral.estadocorte = resultado.Tables[0].Rows[0]["estadocorte"].ToString();
            objDataGeneral.comentario = resultado.Tables[0].Rows[0]["comentario"].ToString();


            // DATOS POR FICHA
            for (int i = 0; i < resultado.Tables[1].Rows.Count; i++)
            {
                datosfichaEntidad obj = new datosfichaEntidad();

                obj.ficha = Convert.ToInt32(resultado.Tables[1].Rows[i]["ficha"].ToString());
                obj.estilo = resultado.Tables[1].Rows[i]["estilo"].ToString();
                obj.prendasprogramado = Convert.ToDouble(resultado.Tables[1].Rows[i]["prendasprogramado"].ToString());
                obj.consumoprogramado = Convert.ToDouble(resultado.Tables[1].Rows[i]["consumoprogramado"].ToString());
                obj.kilosprogramado = Convert.ToDouble(resultado.Tables[1].Rows[i]["kilosprogramado"].ToString());
                obj.porcentajekilosprogramado = Convert.ToDouble(resultado.Tables[1].Rows[i]["porcentajekilosprogramado"].ToString());
                obj.prendastizado = Convert.ToDouble(resultado.Tables[1].Rows[i]["prendastizado"].ToString());
                obj.consumotizado = Convert.ToDouble(resultado.Tables[1].Rows[i]["consumotizado"].ToString());
                obj.kilostizado = Convert.ToDouble(resultado.Tables[1].Rows[i]["kilostizado"].ToString());
                obj.prendasneto = Convert.ToDouble(resultado.Tables[1].Rows[i]["prendasneto"].ToString());
                obj.consumoneto = Convert.ToDouble(resultado.Tables[1].Rows[i]["consumoneto"].ToString());
                obj.kilosneto = Convert.ToDouble(resultado.Tables[1].Rows[i]["kilosneto"].ToString());
                obj.consumolinealbruto = Convert.ToDouble(resultado.Tables[1].Rows[i]["consumolinealbruto"].ToString());
                obj.metrosbruto = Convert.ToDouble(resultado.Tables[1].Rows[i]["metrosbruto"].ToString());
                obj.consumopesobruto = Convert.ToDouble(resultado.Tables[1].Rows[i]["consumopesobruto"].ToString());
                obj.kilosbruto = Convert.ToDouble(resultado.Tables[1].Rows[i]["kilosbruto"].ToString());


                objDataFichas.Add(obj);
            }

            //DATOS POR TENDIDO
            for (int j = 0; j < resultado.Tables[2].Rows.Count; j++)
            {
                datostendidoEntidad obj = new datostendidoEntidad();

                obj.etapa = resultado.Tables[2].Rows[j]["etapa"].ToString();
                obj.panos = Convert.ToDouble(resultado.Tables[2].Rows[j]["panos"].ToString());
                obj.tallas = Convert.ToDouble(resultado.Tables[2].Rows[j]["tallas"].ToString());
                obj.largopano = Convert.ToDouble(resultado.Tables[2].Rows[j]["largopano"].ToString());
                obj.pesopano = Convert.ToDouble(resultado.Tables[2].Rows[j]["pesopano"].ToString());
                obj.densidad = Convert.ToDouble(resultado.Tables[2].Rows[j]["densidad"].ToString());

                objDataTendido.Add(obj);
            }

            //ASIGNAMOS OBJETOS A OBJETO
            objRetornar.DataGeneral = objDataGeneral;
            objRetornar.DataFichas = objDataFichas;
            objRetornar.DataTendido = objDataTendido;


            return objRetornar;

        }

    }
}