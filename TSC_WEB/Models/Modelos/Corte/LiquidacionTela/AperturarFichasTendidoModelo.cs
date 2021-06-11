using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Net.Http.Headers;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela
{
    public class AperturarFichasTendidoModelo
    {
        DBAccess conexion = new DBAccess();
            
        // BUSCAR PARA APERTURAR
        public DataTable BuscarFichasAperturar(string i_ficha,string i_partida, string i_estadotendido,string i_estadocorte)
        {

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_GETFICHASAPERTURA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_ficha", i_ficha));
            comando.Parameters.Add(new OracleParameter("i_partida", i_partida));
            comando.Parameters.Add(new OracleParameter("i_estadotendido", i_estadotendido));
            comando.Parameters.Add(new OracleParameter("i_estadocorte", i_estadocorte));

            comando.Parameters.Add(new OracleParameter("o_cursos", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            DataTable dt = new DataTable();
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();

            return dt;

        }

        // APERTURAR Y RECHAZAR
        public bool AperturarRechazar(int operacion,string partida,int combo,int version,string tipotela,string usuario)
        {
            bool response = true;

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_ADFICHASTENDIDO", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            try
            {
                comando.Parameters.Add(new OracleParameter("i_operacion", operacion));
                comando.Parameters.Add(new OracleParameter("i_partida", partida));
                comando.Parameters.Add(new OracleParameter("i_combo", combo));
                comando.Parameters.Add(new OracleParameter("i_version", version));
                comando.Parameters.Add(new OracleParameter("i_tipotela", tipotela));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));


                comando.ExecuteReader();

                response = true;

            }
            catch (Exception ex)
            {
                response = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return response;

        }

    }
}