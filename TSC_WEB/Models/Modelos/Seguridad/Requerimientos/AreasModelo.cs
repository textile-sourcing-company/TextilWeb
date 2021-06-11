using Dapper;
using Dapper.Oracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Seguridad.Requerimientos;

namespace TSC_WEB.Models.Modelos.Seguridad.Requerimientos
{
    public class AreasModelo
    {
        DBAccess conexion = new DBAccess();

        public List<AreasEntidad> ListarAreas()
        {
            List<AreasEntidad> objLista = new List<AreasEntidad>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GET_AREAS_REQ", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objLista.Add(
                        new AreasEntidad
                        {
                            IDAREA = Convert.ToInt32(registros["IDAREA"].ToString()),
                            NOMBRE_AREA = registros["NOMBRE_AREA"].ToString()
                        }
                );
            }

            conexion.Desconectar();
            return objLista;
        }

    }
}