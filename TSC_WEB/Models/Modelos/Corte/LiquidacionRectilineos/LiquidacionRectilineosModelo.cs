using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionRectilineos
{
    public class LiquidacionRectilineosModelo
    {
        DBAccess conexion = new DBAccess();

        // BUSCAMOS FICHAS PARA APERTURAR
        public List<TallasEntidad> getTallasFicha(int? ficha)
        {
            List<TallasEntidad> objLista = new List<TallasEntidad>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_GETTALLASRECTILINEOS", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                TallasEntidad obj = new TallasEntidad();

                obj.orden = Convert.ToInt32(registros["orden"].ToString());
                obj.talla = registros["talla"].ToString();  

                objLista.Add(obj);
            }

            conexion.Desconectar();
            return objLista;
        }

    }
}