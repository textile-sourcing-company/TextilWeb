using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela.tablasgenerales;


namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela
{
    public class AperturarFichasCorteModelo
    {
        DBAccess conexion = new DBAccess();

        // FICHAS PARA APERTURAR CORTE
        public DataTable BuscarFichasAperturar(string i_ficha, string i_partida)
        {

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_GETFICHASAPERTURACORTE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_ficha", i_ficha));
            comando.Parameters.Add(new OracleParameter("i_partida", i_partida));
            comando.Parameters.Add(new OracleParameter("o_cursos", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            DataTable dt = new DataTable();
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();

            return dt;

        }

        // APERTURAR Y RECHAZAR
        public bool AperturarRechazar(int operacion, string partida, int combo, int version, string tipotela, string usuario)
        {
            bool response = true;

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_ADFICHASCORTE", conexion.Acceder());
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

        // BUSCAR ESTADO DE APROBACION DE CORTE
        public string getestadocorte(string partida, int combo, int version)
        {

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.spu_liquidacion_estadocorte", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_partida", partida));
            comando.Parameters.Add(new OracleParameter("i_combo", combo));
            comando.Parameters.Add(new OracleParameter("i_version", version));

            comando.Parameters.Add(new OracleParameter("o_cursos", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            DataTable dt = new DataTable();
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();

            string valor = string.Empty;

            if (dt.Rows.Count > 0)
            {
                valor =  dt.Rows[0][0].ToString();
            }
            else
            {
                valor = "vacio";
            }

            valor = valor == "" ? "vacio" : valor;
            return valor;
        }


        // CAMBIA ESTADO CORTE
        public bool CerrarRegistroMerma(cort008Entidad cort008 , out string mensaje)
        {

            bool response = true;

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_LIQUI_SETESTADOCORTE_NEW", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("i_ficha", cort008.ficha));
                comando.Parameters.Add(new OracleParameter("i_combo", cort008.combo));
                comando.Parameters.Add(new OracleParameter("i_version", cort008.version));
                comando.Parameters.Add(new OracleParameter("i_tipotela", cort008.tipotela));
                comando.Parameters.Add(new OracleParameter("i_turno", cort008.turno));
                comando.Parameters.Add(new OracleParameter("i_estadotendido", cort008.estadotendido));
                comando.Parameters.Add(new OracleParameter("i_estadocorte", cort008.estadocorte));


                comando.ExecuteNonQuery();
                response = true;
                mensaje = "Correcto";

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                response = false;
            }
            finally{
                conexion.Desconectar();
            }

            return response;

        }

    }
}