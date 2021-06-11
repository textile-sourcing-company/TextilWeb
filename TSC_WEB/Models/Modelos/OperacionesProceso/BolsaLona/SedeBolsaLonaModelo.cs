using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config; 
using TSC_WEB.Models.Entidades.OperacionesProceso.BolsaLona;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.BolsaLona
{
    public class SedeBolsaLonaModelo
    {
        DBAccess conexion = new DBAccess();

        public List<SedeBolsaLonaEntidad> ListarSedeLona()
        {
            List<SedeBolsaLonaEntidad> SedeLonaLista = new List<SedeBolsaLonaEntidad>();
            OracleCommand comando = new OracleCommand("USYSTEX.SPSET_PROD_SEDE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                SedeBolsaLonaEntidad objSedeBolsaLonaE = new SedeBolsaLonaEntidad();
                objSedeBolsaLonaE.Codigo_Sede = registros["CODIGO"].ToString();
                objSedeBolsaLonaE.Nombre_Sede = registros["SEDE"].ToString();

                SedeLonaLista.Add(objSedeBolsaLonaE);
            }
            conexion.Desconectar();

            return SedeLonaLista;
        }
    }
}