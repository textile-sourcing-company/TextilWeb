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
        public List<FichasTallasEntidad> getTallasFicha(int opcion,int? ficha)
        {
            List<FichasTallasEntidad> objLista = new List<FichasTallasEntidad>();

            if (ficha != null)
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_GETRECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_opcion", opcion));
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    FichasTallasEntidad obj = new FichasTallasEntidad();

                    obj.ficha = Convert.ToInt32(registros["ficha"].ToString());
                    obj.cantidad = Convert.ToInt32(registros["cantidad"].ToString());
                    obj.orden = Convert.ToInt32(registros["orden"].ToString());
                    obj.talla = registros["talla"].ToString();

                    objLista.Add(obj);
                }

                conexion.Desconectar();
            }

          
            return objLista;
        }


        // BUSCAMOS DATOS DE LA FICHA (CABECERA)
        public FichaDatos getDatosFicha(int opcion, int? ficha)
        {
            FichaDatos obj = new FichaDatos();

            if (ficha != null)
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_GETRECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_opcion", opcion));
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    // DATOS TABLA
                    if (registros["idrectilineohead"].ToString() != string.Empty)
                      obj.idrectilineohead = Convert.ToInt32(registros["idrectilineohead"].ToString());
                    else
                        obj.idrectilineohead = null;

                    if (registros["lote"].ToString() != string.Empty)
                        obj.lote = Convert.ToInt32(registros["lote"].ToString());
                    else
                        obj.lote = null;

                    // USUARIO
                    obj.usuariocrea = registros["usuariocrea"].ToString();
                    // FECHA
                    if (registros["fechacrea"].ToString() != string.Empty)
                    {
                        obj.fechacrea = Convert.ToDateTime(registros["fechacrea"].ToString()).ToShortDateString();
                    }
                    else
                    {
                        obj.fechacrea = null;
                    }

                    // DATOS ERP
                    obj.ficha = Convert.ToInt32(registros["ficha"].ToString());
                    obj.combo = registros["combo"].ToString();
                    obj.partidarectilineo = registros["partidarectilineo"].ToString();
                    obj.estilotsc = registros["estilotsc"].ToString();
                    obj.pedidos = registros["pedidos"].ToString();
                    obj.estilocliente = registros["estilocliente"].ToString();
                }

                conexion.Desconectar();
            }

            
            return obj;
        }
    }
}