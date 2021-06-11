using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace TSC_WEB.Config
{
    public class DBAccess
    {
        //CONEXION
        OracleConnection conexion = new OracleConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        //ACCEDER A LA CONEXION
        public OracleConnection Acceder()
        {
            return conexion;
        }

        //ABRE CONEXION
        public void Conectar()
        {
            conexion.Open();
        }

        //CIERRA CONEXION
        public void Desconectar()
        {
            conexion.Close();
        }

        //OBTENER ORACLE_CONNECTION
        public static OracleConnection ObtenerConexion()
        {
            try
            {
                return new OracleConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static SqlConnection ObtenerConexionSQL()
        {
            try
            {
                return new SqlConnection(WebConfigurationManager.ConnectionStrings["consql"].ConnectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}