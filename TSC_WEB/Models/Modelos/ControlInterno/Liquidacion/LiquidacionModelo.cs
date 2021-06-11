using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.ControlInterno.Liquidacion;
using TSC_WEB.Models.Entidades.Corte;
using System.Data;

namespace TSC_WEB.Models.Modelos.ControlInterno.Liquidacion
{
    public class LiquidacionModelo
    {

        DBAccess conexion = new DBAccess();

        // LISTA FICHAS PENDIENTES
        public List<FichasPendientesEntidad> GetFichasPendientes(/*out string mensaje*/)
        {
            List<FichasPendientesEntidad> objLista = new List<FichasPendientesEntidad>();

            OracleCommand comando = new OracleCommand("USYSTEX.SP_CONSULTA_FICHAS_LIQUI", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            try
            {

                comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                OracleDataReader registros = comando.ExecuteReader();


                while (registros.Read())
                {
                    FichasPendientesEntidad obj = new FichasPendientesEntidad();

                    obj.pedido = Convert.ToInt32(registros["pedido"].ToString());
                    obj.ficha = Convert.ToInt32(registros["ficha"].ToString());
                    obj.fechadespacho = registros["fechadespacho"].ToString();
                    obj.confirmado = registros["confirmado"].ToString();
                    obj.cliente = registros["cliente"].ToString();
                    obj.kilo = registros["kilo"].ToString();



                    objLista.Add(obj);
                }

                //mensaje = "Reporte generado";

            }
            catch (Exception ex)
            {
                //mensaje = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return objLista;

        }
        
        // REGISTRAS FICHAS EN EL TEMPORAL
        public bool SetFichasTmp(string ficha,out string mensaje)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.SPU_INSERT_TMP_LIQUI", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            try
            {
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.ExecuteNonQuery();

                mensaje = "Registrado correctamente";
                return true;
            }
            catch(Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }
        }
        
        // GENERA STOCK INICIAL
        public bool SetStockInicial(string usuario,string ficha,out string mensaje)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.SP_CONFIRMAR_STOCK_INICIAL", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            try
            {
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));

                comando.ExecuteNonQuery();

                mensaje = "Realizado correctamente";
                return true;

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }

        }

        // LISTAR STOCK CONFIRMADO
        public List<FichasEntidad> GetFichasStock()
        {
            List<FichasEntidad> objLista = new List<FichasEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_STOCK_CONFIRMADO_LIQUI", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {

                comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                OracleDataReader registros = comando.ExecuteReader();


                while (registros.Read())
                {

                    FichasEntidad obj = new FichasEntidad();

                    obj.FICHA = registros["i_ficha"].ToString();
                    obj.fecha = registros["dt_fecha"].ToString();
                    obj.tipo = registros["ch_tipo"].ToString();
                    obj.usuario = registros["ch_usuario"].ToString();

                    objLista.Add(obj);

                }

            }
            catch (Exception ex)
            {
                ;
            }
            finally
            {
                conexion.Desconectar();
            }

            return objLista;
        }

    }
}