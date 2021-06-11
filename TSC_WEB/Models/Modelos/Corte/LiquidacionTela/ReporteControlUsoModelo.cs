using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela
{
    public class ReporteControlUsoModelo
    {
        DBAccess conexion = new DBAccess();

        //LISTA REPORTE
        public List<ReporteControlUsoEntidad> Listar(string i_fechainicio,string i_fechafin,string i_partida,int? i_ficha)
        {
            List<ReporteControlUsoEntidad> objLista = new List<ReporteControlUsoEntidad>();

            OracleCommand comando = new OracleCommand("systextilrpt.spu_getcontrolusoliquidacion_n", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_fechainicio", i_fechainicio == string.Empty ? null : i_fechainicio));
            comando.Parameters.Add(new OracleParameter("i_fechafin", i_fechafin == string.Empty ? null : i_fechafin));
            comando.Parameters.Add(new OracleParameter("i_partida", i_partida == string.Empty ? null : i_partida));
            comando.Parameters.Add(new OracleParameter("i_ficha", i_ficha));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objLista.Add(
                    new ReporteControlUsoEntidad
                    {
                        partida = registros["partida"].ToString(),
                        ficha = registros["ficha"].ToString(),
                        f_registro = registros["f_registro"].ToString(),
                        tizado = registros["tizado"].ToString(),
                        tendido = registros["tendido"].ToString(),
                        corte = registros["corte"].ToString(),
                        cantidadprogramada = registros["cantidadprogramada"].ToString(),
                        versiones = registros["versiones"].ToString(),
                        combo = registros["combo"].ToString(),
                        maximo = registros["maximo"].ToString(),


                    }
                );
            }

            conexion.Desconectar();

            return objLista;

        }

        //LISTA REPORTE COMSUMO 2
        public DataSet Listar2(int semana1, int semana2)
        {
            OracleCommand comando = new OracleCommand("systextilrpt.spu_getcontrolusoliquidacion2", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();


            comando.Parameters.Add(new OracleParameter("i_semanainicio",semana1));
            comando.Parameters.Add(new OracleParameter("i_semanafin", semana2));
            comando.Parameters.Add(new OracleParameter("o_cursor1", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("o_cursor2", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            DataSet resultado = new DataSet();
            OracleDataAdapter adapter = new OracleDataAdapter(comando);
            adapter.Fill(resultado);
            conexion.Desconectar();

            return resultado;
        }

        //MOSTRAR INDICADOR DE USO
        public DataSet getIndicadorUso(int? anio,int? mes,int? semana,string fecha)
        {
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_GETINDICADORLIQUIDACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();


            comando.Parameters.Add(new OracleParameter("I_ANIO", anio));
            comando.Parameters.Add(new OracleParameter("I_MES", mes));
            comando.Parameters.Add(new OracleParameter("I_SEMANA", semana));
            comando.Parameters.Add(new OracleParameter("I_FECHA", fecha));

            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("o_cursoranio", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("o_cursormes", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            

            //DataTable dt = new DataTable();
            //dt.Load( comando.ExecuteReader());

            //conexion.Desconectar();

            DataSet ds = new DataSet();
            OracleDataAdapter adapter = new OracleDataAdapter(comando);
            adapter.Fill(ds);
            conexion.Desconectar();

            return ds;
        }

    }
}