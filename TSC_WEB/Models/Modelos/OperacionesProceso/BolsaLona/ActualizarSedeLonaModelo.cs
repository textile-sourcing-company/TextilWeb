using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.OperacionesProceso.BolsaLona;
using TSC_WEB.Models.Modelos.Sistema;
using System.Data;
using System.Net.Mail;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;


namespace TSC_WEB.Models.Modelos.OperacionesProceso.BolsaLona
{
    public class ActualizarSedeLonaModelo
    {
        DBAccess conexion = new DBAccess();
        //VALIDA SI EXISTE FICHA
        public int ExisteLona (string v_codlona)
        {
            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("USYSTEX.GET_VALIDA_EXISTE_LONA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("cod_lona", v_codlona));
            //comando.Parameters.Add(new OracleParameter("cod_lona", objFile.Codigo_Lona));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();
            int respuesta = dt.Rows.Count;

            return respuesta;
        }

        //Actualizar Lona
        public bool GetActualizarBolsaLona(ActualizarSedeLonaEntidad _obj)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.SETSYS_LOGI_ACTUALIZATOCKLONA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("PBOLSA", _obj.Codigo_Lona));
                comando.Parameters.Add(new OracleParameter("PSEDE", _obj.Codigo_Sede)); 
                comando.ExecuteNonQuery();
                return true; 
            }
            catch
            {
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }
        }
        
    }
}