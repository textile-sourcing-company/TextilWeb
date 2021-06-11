using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using System.Data;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela
{
    public class CaidasModelo
    {
        DBAccess conexion = new DBAccess();

        // LISTA CAIDAS
        public List<CaidasEntidad> getCaidas(string pendiente,string realizado,string ficha)
        {
            List<CaidasEntidad> objLista = new List<CaidasEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_LIQUIDACION_GETCAIDA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_pendiente", pendiente == string.Empty ? null : pendiente));
            comando.Parameters.Add(new OracleParameter("i_realizado", realizado == string.Empty ? null : realizado));
            comando.Parameters.Add(new OracleParameter("i_ficha", ficha == string.Empty ? null : ficha));

            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {

                CaidasEntidad obj = new CaidasEntidad();
                obj.id = Convert.ToInt32(registros["id"].ToString());
                obj.partida = registros["partida"].ToString();
                obj.combo = Convert.ToInt32(registros["combo"].ToString());
                obj.version = Convert.ToInt32(registros["version"].ToString());
                obj.tipo_tela = registros["tipo_tela"].ToString();
                obj.usuario_tendido = registros["usuario_tendido"].ToString();
                obj.fecha_tendido = registros["fecha_tendido"].ToString();
                obj.ficha = Convert.ToInt32(registros["ficha"].ToString());
                obj.cantidadprogramada = Convert.ToDouble(registros["cantidadprogramada"].ToString());
                obj.cantidadreal = Convert.ToDouble(registros["cantidadreal"].ToString());
                obj.estado = registros["estado"].ToString();
                obj.fichacaida = registros["fichacaida"].ToString();
                obj.cantidadcaida = registros["cantidadcaida"].ToString();


                objLista.Add(obj);
            }
            conexion.Desconectar();
            return objLista;
        }

        // REGISTRA CAIDAS
        public bool setCaidas(int id, int ficha,out string  mensaje)
        {
     
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_LIQUIDACION_CAIDAS_MOD", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            

            try
            {
                comando.Parameters.Add(new OracleParameter("i_id", id));
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));

                comando.ExecuteNonQuery();
                mensaje = "Realizado correctamnte";
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
    }
}