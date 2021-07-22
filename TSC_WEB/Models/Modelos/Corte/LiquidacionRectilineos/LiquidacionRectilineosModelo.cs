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
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETRECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_opcion", opcion));
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    FichasTallasEntidad obj = new FichasTallasEntidad();

                    if (registros["idrectilineoficha"].ToString() != string.Empty)
                        obj.idrectilineoficha = Convert.ToInt32(registros["idrectilineoficha"].ToString());
                    else
                        obj.idrectilineoficha = null;
                    obj.ficha = Convert.ToInt32(registros["ficha"].ToString());
                    obj.cantidad = Convert.ToInt32(registros["cantidad"].ToString());
                    obj.orden = Convert.ToInt32(registros["orden"].ToString());
                    obj.talla = registros["talla"].ToString();
                    obj.realprimera = Convert.ToInt32(registros["realprimera"].ToString());
                    obj.pesonetoreal = Convert.ToDecimal(registros["pesonetoreal"].ToString());



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
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETRECTILINEOS", conexion.Acceder());
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

                    obj.mermarecorte = Convert.ToDecimal(registros["mermarecorte"].ToString());
                    obj.mermahilos = Convert.ToDecimal(registros["mermahilos"].ToString());



                }

                conexion.Desconectar();
            }

            
            return obj;
        }
    
        // REGISTRAMOS CABECERA
        public bool saveHead(string partida,string usuario,int lote, decimal mermarecorte, decimal mermahilos, out string mensaje)
        {

            //FichaDatos obj = new FichaDatos();
            bool retornar = false;
            string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_HEADREGISTRO", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_partida", partida));
                comando.Parameters.Add(new OracleParameter("i_lote", lote));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
                comando.Parameters.Add(new OracleParameter("i_mermarecorte", mermarecorte));
                comando.Parameters.Add(new OracleParameter("i_mermahilos", mermahilos));


                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    //obj.idrectilineohead = Convert.ToInt32(registros["idrectilineohead"].ToString());
                    id = registros["idrectilineohead"].ToString();
                }
                mensaje = id;
                retornar = true;
            }
            catch (Exception ex)
            {
                retornar = false;
                mensaje = ex.Message;
            }

            conexion.Desconectar();


            return retornar;

        }

        // REGISTRAMOS FICHAS
        public bool saveFichas(int? idrectilineoficha,int? idrectilineohead,int ficha, string usuario , out string mensaje)
        {

            //FichaDatos obj = new FichaDatos();
            bool retornar = false;
            string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_FICHASREGISTRO", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_idrectilineoficha", idrectilineoficha));
                comando.Parameters.Add(new OracleParameter("i_idrectilineohead", idrectilineohead));
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    //obj.idrectilineohead = Convert.ToInt32(registros["idrectilineohead"].ToString());
                    id = registros["IDRECTILINEOFICHA"].ToString();
                }
                mensaje = id;
                retornar = true;
            }
            catch (Exception ex)
            {
                retornar = false;
                mensaje = ex.Message;
            }

            conexion.Desconectar();


            return retornar;

        }

        // REGISTRO DE TALLAS
        public bool saveTallas(int? idrectilineoficha, string talla, int realprimera,decimal pesoneto  ,out string mensaje)
        {

            //FichaDatos obj = new FichaDatos();
            bool retornar = false;
            //string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_TALLASREGISTRO", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_idrectilineoficha", idrectilineoficha));
                comando.Parameters.Add(new OracleParameter("i_talla", talla));
                comando.Parameters.Add(new OracleParameter("i_realprimera", realprimera));
                comando.Parameters.Add(new OracleParameter("i_pesoneto", pesoneto));
                //comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
                //comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                comando.ExecuteNonQuery();

                //OracleDataReader registros = comando.ExecuteReader();

                //while (registros.Read())
                //{
                //    //obj.idrectilineohead = Convert.ToInt32(registros["idrectilineohead"].ToString());
                //    id = registros["IDRECTILINEOFICHA"].ToString();
                //}
                mensaje = "Registrado correctamente";
                retornar = true;
            }
            catch (Exception ex)
            {
                retornar = false;
                mensaje = ex.Message;
            }

            conexion.Desconectar();


            return retornar;

        }

    }
}