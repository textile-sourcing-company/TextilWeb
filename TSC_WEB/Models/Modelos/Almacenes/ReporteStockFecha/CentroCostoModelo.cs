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
using TSC_WEB.Models.Entidades.Almacenes.ReporteStockFecha;


namespace TSC_WEB.Models.Modelos.Almacenes.ReporteStockFecha
{
    public class CentroCostoModelo
    {
        DBAccess conexion = new DBAccess();
        //METODO PARA CARGA LOS CENTROS DE COSTO EN EL COMBO
        public List<CentroCosto> ListarCentrosCosto()
        {
            List<CentroCosto> objLista = new List<CentroCosto>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GET_CCOSTO_BALANCE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registro = comando.ExecuteReader();

            while (registro.Read())
            {
                objLista.Add(
                        new CentroCosto
                        {
                            CODCENTROCOSTO = registro["CENTRO_COSTO"].ToString(),
                            DECRIPCENTROCOSTO = registro["DESCRICAO"].ToString()     
                        }
                    );
            }
            conexion.Desconectar();
            return objLista;
        }



        public List<CentroCosto> ListarCentrosCostoTotal(string v_centrocosto)
        {
            List<CentroCosto> objLista = new List<CentroCosto>();

           
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GET_CENTROCOSTO", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("P_CENTROCOSTO", v_centrocosto));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registro = comando.ExecuteReader();

            while (registro.Read())
            {
                objLista.Add(
                        new CentroCosto
                        {
                            CODCENTROCOSTO2 = registro["CENTROCOSTO"].ToString(),
                            DECRIPCENTROCOSTO2 = registro["DESCRIP"].ToString()
                        }
                    );
            }
            conexion.Desconectar();
            return objLista;
        }
    }
}