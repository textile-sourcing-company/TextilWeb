using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using TSC_WEB.Config;
using System.Drawing;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using TSC_WEB.Util.Sistema;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionRectilineos
{
    public class LiquidacionRectilineosModelo : Excel
    {
        DBAccess conexion = new DBAccess();
        //ExcelWorksheet workSheet;


        // BUSCAMOS FICHAS PARA APERTURAR
        public List<FichasTallasEntidad> getTallasFicha(int opcion, int? ficha, string tipo)
        {
            List<FichasTallasEntidad> objLista = new List<FichasTallasEntidad>();

            if (ficha != null)
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETRECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_opcion", opcion));
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.Parameters.Add(new OracleParameter("i_tipo", tipo));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    FichasTallasEntidad obj = new FichasTallasEntidad();

                    // ID RECTILINEO DE FICHA
                    if (registros["idrectilineoficha"].ToString() != string.Empty)
                        obj.idrectilineoficha = Convert.ToInt32(registros["idrectilineoficha"].ToString());
                    else
                        obj.idrectilineoficha = null;
                    // FICHA
                    obj.ficha = Convert.ToInt32(registros["ficha"].ToString());
                    // CANTIDAD PRIMERAS PROGRAMADAS y REGISTRADAS EN EL ERP
                    if (registros["cantidadprimeraliquidadaerp"].ToString() != string.Empty)
                    {
                        decimal cantidad = Convert.ToDecimal(registros["cantidadprimeraliquidadaerp"].ToString());
                        if (cantidad > 0)
                        {
                            obj.cantidadprimeraprogramada = Convert.ToDecimal(registros["cantidadprimeraliquidadaerp"].ToString());
                            obj.cantliquidadaestado = true;
                        }
                        else
                        {
                            obj.cantidadprimeraprogramada = Convert.ToDecimal(registros["cantidadprimeraprogramada"].ToString());
                            obj.cantliquidadaestado = false;
                        }
                    }
                    else
                    {
                        obj.cantidadprimeraprogramada = Convert.ToDecimal(registros["cantidadprimeraprogramada"].ToString());
                        obj.cantliquidadaestado = false;
                    }

                    // ORDEN DE LA TALLA
                    obj.orden = Convert.ToInt32(registros["orden"].ToString());
                    // TALLA
                    obj.talla = registros["talla"].ToString();
                    // REAL PRIMERA
                    obj.realprimera = Convert.ToInt32(registros["realprimera"].ToString());
                    // PESO NETO REAL
                    obj.pesonetoreal = Convert.ToDecimal(registros["pesonetoreal"].ToString());

                    // PESO DESPACHADO
                    if (registros["pesodespachado"].ToString() != string.Empty)
                        obj.pesodespachado = Convert.ToDecimal(registros["pesodespachado"].ToString());
                    else
                        obj.pesodespachado = 0;

                    // PEDIDO
                    obj.pedido = Convert.ToInt32(registros["pedido"].ToString());

                    // ESTILO TSC
                    obj.estilotsc = registros["estilotsc"].ToString();
                    // ESTILO CLIENTE
                    obj.estilocliente = registros["estilocliente"].ToString();
                    // COLOR
                    obj.color = registros["color"].ToString();
                    // CONSUMO PROGRAMADO
                    if (registros["consumoprogramado"].ToString() != string.Empty)
                        obj.consumoprogramado = Convert.ToDecimal(registros["consumoprogramado"].ToString());
                    else
                        obj.consumoprogramado = 0;

                    // PRIMERAS DESPACHADAS
                    obj.cantidadprimeradespachada = Convert.ToDecimal(registros["cantidadprimeradespachada"].ToString());

                    // SEGUNDAS DESPACHADAS
                    obj.cantidadsegundadespachada = Convert.ToDecimal(registros["cantidadsegundadespachada"].ToString());



                    objLista.Add(obj);
                }

                conexion.Desconectar();
            }


            return objLista;
        }

        // BUSCAMOS FICHAS PARA SEGUNDAS
        public List<FichasTallasSegundasEntidad> getTallasFichaSegundas(int opcion, int? ficha, string tipo)
        {
            List<FichasTallasSegundasEntidad> objLista = new List<FichasTallasSegundasEntidad>();

            if (ficha != null)
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETRECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_opcion", opcion));
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.Parameters.Add(new OracleParameter("i_tipo", tipo));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    FichasTallasSegundasEntidad obj = new FichasTallasSegundasEntidad();

                    if (registros["idrectilineotsegunda"].ToString() != string.Empty)
                        obj.idrectilineotsegunda = Convert.ToInt32(registros["idrectilineotsegunda"].ToString());
                    else
                        obj.idrectilineotsegunda = null;
                    obj.realsegunda = Convert.ToDecimal(registros["realsegunda"].ToString());
                    obj.ordentalla = Convert.ToInt32(registros["ordentalla"].ToString());
                    obj.talla = registros["talla"].ToString();
                    obj.pesonetorealsegunda = Convert.ToDecimal(registros["pesonetorealsegunda"].ToString());

                    if (registros["programadosegunda"].ToString() != string.Empty)
                        obj.programadosegunda = Convert.ToDecimal(registros["programadosegunda"].ToString());
                    else
                        obj.programadosegunda = 0;


                    objLista.Add(obj);
                }

                conexion.Desconectar();
            }


            return objLista;
        }

        // BUSCAMOS DATOS DE LA FICHA (CABECERA)
        public FichaDatos getDatosFicha(int opcion, int? ficha, string tipo)
        {
            FichaDatos obj = new FichaDatos();

            if (ficha != null)
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETRECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_opcion", opcion));
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.Parameters.Add(new OracleParameter("i_tipo", tipo));
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

                    obj.tipo = tipo;

                    obj.estado = registros["estado"].ToString();




                }

                conexion.Desconectar();
            }


            return obj;
        }

        // REGISTRAMOS CABECERA
        public bool saveHead(string partida, string usuario/*,int lote*/, decimal mermarecorte, decimal mermahilos, string tipo, string estado, out string mensaje)
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
                //comando.Parameters.Add(new OracleParameter("i_lote", lote));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
                comando.Parameters.Add(new OracleParameter("i_mermarecorte", mermarecorte));
                comando.Parameters.Add(new OracleParameter("i_mermahilos", mermahilos));
                comando.Parameters.Add(new OracleParameter("i_tipo", tipo));
                comando.Parameters.Add(new OracleParameter("i_estado", estado));



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
        public bool saveFichas(int? idrectilineoficha, int? idrectilineohead, int ficha, string usuario, int pedido, string estilotsc, string estilocliente, string combo, string tipo, out string mensaje)
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
                comando.Parameters.Add(new OracleParameter("i_pedido", pedido));
                comando.Parameters.Add(new OracleParameter("i_estilotsc", estilotsc));
                comando.Parameters.Add(new OracleParameter("i_estilocliente", estilocliente));
                comando.Parameters.Add(new OracleParameter("i_combo", combo));
                comando.Parameters.Add(new OracleParameter("i_tipo", tipo));
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
        public bool saveTallas(int? idrectilineoficha, string talla, int realprimera, decimal pesoneto, decimal programado, int orden, decimal pesoprogramado, string tipo, out string mensaje)
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
                comando.Parameters.Add(new OracleParameter("i_programado", programado));
                comando.Parameters.Add(new OracleParameter("i_orden", orden));
                comando.Parameters.Add(new OracleParameter("i_pesoprogramado", pesoprogramado));
                comando.Parameters.Add(new OracleParameter("i_tipo", tipo));

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

        // REGISTRO DE TALLAS SEGUNDAS
        public bool saveTallasSegundas(int? idrectilineo, string talla, int realsegunda, decimal pesonetosegunda, decimal programadosegunda, int orden, out string mensaje)
        {

            //FichaDatos obj = new FichaDatos();
            bool retornar = false;
            //string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_SEGUNDASREGISTRO", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_idrectilineo", idrectilineo));
                comando.Parameters.Add(new OracleParameter("i_talla", talla));
                comando.Parameters.Add(new OracleParameter("i_realsegunda", realsegunda));
                comando.Parameters.Add(new OracleParameter("i_pesonetosegunda", pesonetosegunda));
                comando.Parameters.Add(new OracleParameter("i_programadosegunda", programadosegunda));
                comando.Parameters.Add(new OracleParameter("i_orden", orden));

                comando.ExecuteNonQuery();

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

        // REPORTE
        public List<ReporteEntidad> getReporte(string fechai, string fechaf, string ficha, string partida, string tipo)
        {
            List<ReporteEntidad> objLista = new List<ReporteEntidad>();

            if (fechai != null || fechaf != null || ficha != null || partida != null || tipo != null)
            {

                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETRECTILINEOS_REPORTE", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();


                comando.Parameters.Add(new OracleParameter("i_fechai", fechai));
                comando.Parameters.Add(new OracleParameter("i_fechaf", fechaf));
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.Parameters.Add(new OracleParameter("i_partida", partida));
                comando.Parameters.Add(new OracleParameter("i_tipo", tipo));

                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    ReporteEntidad obj = new ReporteEntidad();

                    obj.usuariocrea = registros["usuariocrea"].ToString();
                    obj.tipo = registros["tipo"].ToString();
                    obj.fechamod = registros["fechamod"].ToString() != string.Empty ? Convert.ToDateTime(registros["fechamod"].ToString()).ToShortDateString() : "";
                    obj.ficha = Convert.ToInt32(registros["ficha"].ToString());
                    obj.partida = registros["partida"].ToString();
                    obj.pedido = Convert.ToInt32(registros["pedido"].ToString());
                    obj.combo = registros["combo"].ToString();
                    obj.estilotsc = registros["estilotsc"].ToString();
                    obj.estilocliente = registros["estilocliente"].ToString();
                    obj.talla = registros["talla"].ToString();
                    obj.realprimera = Convert.ToInt32(registros["realprimera"].ToString());
                    obj.programado = Convert.ToInt32(registros["programado"].ToString());
                    obj.pesonetoreal = Convert.ToDecimal(registros["pesonetoreal"].ToString());
                    obj.pesoprogramado = Convert.ToDecimal(registros["pesoprogramado"].ToString());
                    obj.ordentalla = Convert.ToInt32(registros["ordentalla"].ToString());

                    obj.mermahilos = Convert.ToDecimal(registros["mermahilos"].ToString());
                    obj.mermarecorte = Convert.ToDecimal(registros["mermarecorte"].ToString());

                    objLista.Add(obj);
                }

                conexion.Desconectar();


            }

            return objLista;
        }

        // REGISTRO DE RECTILINEOS DESPACHADOS
        public bool saveRectilineoDespacho(IngresoRectilineosEntidad objeto, out string mensaje)
        {

            //FichaDatos obj = new FichaDatos();
            bool retornar = false;
            string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_SETCANTRECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_partida", objeto.partida));
                comando.Parameters.Add(new OracleParameter("i_tiporectilineo", objeto.tiporectilineo));
                comando.Parameters.Add(new OracleParameter("i_talla", objeto.talla));
                comando.Parameters.Add(new OracleParameter("i_cantidad", objeto.cantidad));
                comando.Parameters.Add(new OracleParameter("i_cantidadsegunda", objeto.cantidadsegunda));
                comando.Parameters.Add(new OracleParameter("i_usuario", objeto.usuario));


                //comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                retornar = true;
                mensaje = "Registrado correctamente";
            }
            catch (Exception ex)
            {
                retornar = false;
                mensaje = ex.Message;
            }

            conexion.Desconectar();


            return retornar;

        }

        // REGISTRAMOS PARTIDA DE TELA
        public bool savePartidaTela(PartidaTelaEntidad objpartida, out string mensaje)
        {
            bool retornar = false;
            //string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_SETPARTIDATELA", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_partidatela", objpartida.partidatela));
                comando.Parameters.Add(new OracleParameter("i_codtela", objpartida.codtela));
                comando.Parameters.Add(new OracleParameter("i_fechacarga", objpartida.fechacarga));
                comando.Parameters.Add(new OracleParameter("i_usuarioregistro", objpartida.usuarioregistro));
                comando.Parameters.Add(new OracleParameter("i_observacion", objpartida.observacion));

                comando.ExecuteNonQuery();
                retornar = true;
                mensaje = "Registrado correctamente";
            }
            catch (Exception ex)
            {
                retornar = false;
                mensaje = ex.Message;
            }

            conexion.Desconectar();
            return retornar;
        }

        // REGISTRAMOS PARTIDA DE TELA - RECTILINEOS
        public bool savePartidaTelaRectilineos(PartidaTelaRectilineoEntidad objpartidarec, out string mensaje)
        {
            bool retornar = false;
            string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_SETPARTIDARECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_partidarectilineo", objpartidarec.partidarectilineo));
                comando.Parameters.Add(new OracleParameter("i_tiporectilineo", objpartidarec.tiporectilineo));
                comando.Parameters.Add(new OracleParameter("i_partidatela", objpartidarec.partidatela));
                comando.Parameters.Add(new OracleParameter("i_fechacarga", objpartidarec.fechacarga));
                comando.Parameters.Add(new OracleParameter("i_usuario", objpartidarec.usuarioreg));
                comando.Parameters.Add(new OracleParameter("i_observacion", objpartidarec.observacion));
                comando.Parameters.Add(new OracleParameter("i_kilostotales", objpartidarec.kilostotales));

                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;



                OracleDataReader registros = comando.ExecuteReader();
                while (registros.Read())
                {
                    id = registros["id"].ToString();
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

        // REGISTRAMOS PARTIDA DE TELA RECTILINEOS - TALLAS
        public bool savePartidaTelaRectilineos_Talla(PartidaRectilineoTallasEntidad objpartidarectalla, out string mensaje)
        {
            bool retornar = false;
            string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_SETPARTIDARECTILINEO_TALLA", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_idpartidarectilineo", objpartidarectalla.idpartidarectilineo));
                comando.Parameters.Add(new OracleParameter("i_talla", objpartidarectalla.talla));
                comando.Parameters.Add(new OracleParameter("i_cantidadprimera", objpartidarectalla.cantidadprimera));
                comando.Parameters.Add(new OracleParameter("i_cantidadsegunda", objpartidarectalla.cantidadsegunda));
                comando.Parameters.Add(new OracleParameter("i_orden", objpartidarectalla.orden));


                comando.ExecuteNonQuery();
                retornar = true;
                mensaje = "Registrado correctamente";
            }
            catch (Exception ex)
            {
                retornar = false;
                mensaje = ex.Message;
            }

            conexion.Desconectar();
            return retornar;
        }

        // REGISTRAMOS PARTIDA DE TELA RECTILINEOS - TALLAS
        public bool adddeletetallasrectilineos(int opcion, string partidatela, string talla, out string mensaje)
        {
            bool retornar = false;
            string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_ADD_DELETE_TALLA_PARTIDA", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_opcion", opcion));
                comando.Parameters.Add(new OracleParameter("i_partidatela", partidatela));
                comando.Parameters.Add(new OracleParameter("i_talla", talla));

                comando.ExecuteNonQuery();
                retornar = true;
                mensaje = "Realizado correctamente";
            }
            catch (Exception ex)
            {
                retornar = false;
                mensaje = ex.Message;
            }

            conexion.Desconectar();
            return retornar;
        }

        // ELIMINAMOS PARTIDA DE RECTILINEOS
        public bool deletepartidarectilineos(int idpartidarectilineo, out string mensaje)
        {
            bool retornar = false;
            string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_DELETEPARTIDARECTILINEO", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_idpartidarectilineo", idpartidarectilineo));

                comando.ExecuteNonQuery();
                retornar = true;
                mensaje = "Eliminado correctamente";
            }
            catch (Exception ex)
            {
                retornar = false;
                mensaje = ex.Message;
            }

            conexion.Desconectar();
            return retornar;
        }

        // BUSCAMOS PARTIDA DE TELA
        public PartidaTelaEntidad getPartidaTela(string partidatela)
        {
            PartidaTelaEntidad objretornar = new PartidaTelaEntidad();

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETPARTIDARECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_opcion", 1));
                comando.Parameters.Add(new OracleParameter("i_partidatela", partidatela));
                comando.Parameters.Add(new OracleParameter("i_idpartidarectilineo", null));
                comando.Parameters.Add(new OracleParameter("i_tiporectilineo", null));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    objretornar.partidatela = registros["partidatela"].ToString();
                    objretornar.codtela = registros["codtela"].ToString();
                    //objretornar.fechacarga = Convert.ToDateTime(registros["fechacarga"].ToString()).ToShortDateString();
                    objretornar.fechacarga = Convert.ToDateTime(registros["fechacarga"].ToString()).ToString("yyyy-MM-dd");
                    objretornar.usuarioregistro = registros["usuarioregistro"].ToString();
                    objretornar.observacion = registros["observacion"].ToString();
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Desconectar();
            }
            return objretornar;
        }

        // BUSCAMOS PARTIDA DE RECTILINEO
        public List<PartidaTelaRectilineoEntidad> getPartidaTelaRectilineo(string partidatela)
        {
            List<PartidaTelaRectilineoEntidad> objretornar = new List<PartidaTelaRectilineoEntidad>();

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETPARTIDARECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_opcion", 2));
                comando.Parameters.Add(new OracleParameter("i_partidatela", partidatela));
                comando.Parameters.Add(new OracleParameter("i_idpartidarectilineo", null));
                comando.Parameters.Add(new OracleParameter("i_tiporectilineo", null));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    PartidaTelaRectilineoEntidad obj = new PartidaTelaRectilineoEntidad();

                    obj.idpartidarectilineo = Convert.ToInt32(registros["idpartidarectilineo"].ToString());
                    obj.partidarectilineo = registros["partidarectilineo"].ToString();
                    obj.tiporectilineo = registros["tiporectilineo"].ToString();
                    obj.partidatela = registros["partidatela"].ToString();
                    //obj.fechacarga = Convert.ToDateTime(registros["fechacarga"].ToString()).ToShortDateString();
                    obj.fechacarga = Convert.ToDateTime(registros["fechacarga"].ToString()).ToString("yyyy-MM-dd");

                    obj.observacion = registros["observacion"].ToString();

                    obj.kilostotales = registros["kilostotales"].ToString() != "" ? Convert.ToDecimal(registros["kilostotales"].ToString()) : 0;


                    objretornar.Add(obj);
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Desconectar();
            }
            return objretornar;
        }

        public List<PartidaRectilineoTallasEntidad> getPartidaTelaRectilineoTallas(string partida)
        {
            List<PartidaRectilineoTallasEntidad> objretornar = new List<PartidaRectilineoTallasEntidad>();

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETPARTIDARECTILINEOS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_opcion", 3));
                comando.Parameters.Add(new OracleParameter("i_partidatela", partida));
                comando.Parameters.Add(new OracleParameter("i_idpartidarectilineo", null));
                comando.Parameters.Add(new OracleParameter("i_tiporectilineo", null));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    PartidaRectilineoTallasEntidad obj = new PartidaRectilineoTallasEntidad();


                    obj.idpartrectitalla = Convert.ToInt32(registros["idpartrectitalla"].ToString());
                    obj.idpartidarectilineo = Convert.ToInt32(registros["idpartidarectilineo"].ToString());
                    obj.talla = registros["talla"].ToString();
                    obj.cantidadprimera = Convert.ToDecimal(registros["cantidadprimera"].ToString());
                    obj.cantidadsegunda = Convert.ToDecimal(registros["cantidadsegunda"].ToString());


                    objretornar.Add(obj);
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Desconectar();
            }
            return objretornar;
        }


        //REPORTE DE RECTILINEOS INGRESADOS POR EL ALMACEN
        public List<ReporteIngresoRectilineosAlmacenEntidad> getReporteIngresoRectilineosAlmacen(
                string i_fechai, string i_fechaf, string i_cliente, string i_programa, string i_partidatela
            )
        {
            List<ReporteIngresoRectilineosAlmacenEntidad> objretornar = new List<ReporteIngresoRectilineosAlmacenEntidad>();

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETRECTIALMACEN_REPORTE", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_fechai", i_fechai));
                comando.Parameters.Add(new OracleParameter("i_fechaf", i_fechaf));
                comando.Parameters.Add(new OracleParameter("i_cliente", i_cliente));
                comando.Parameters.Add(new OracleParameter("i_programa", i_programa));
                comando.Parameters.Add(new OracleParameter("i_partidatela", i_partidatela));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    ReporteIngresoRectilineosAlmacenEntidad obj = new ReporteIngresoRectilineosAlmacenEntidad();

                    obj.partidatela = registros["partidatela"].ToString();
                    obj.partidarectilineo = registros["partidarectilineo"].ToString();
                    obj.tiporectilineo = registros["tiporectilineo"].ToString();
                    obj.partidatela = registros["partidatela"].ToString();
                    obj.observacion = registros["observacion"].ToString();
                    obj.talla = registros["talla"].ToString();
                    obj.orden = Convert.ToInt32(registros["orden"].ToString());


                    obj.cantidadprimera = Convert.ToDecimal(registros["cantidadprimera"].ToString());
                    obj.cantidadsegunda = Convert.ToDecimal(registros["cantidadsegunda"].ToString());

                    obj.kilostotales = Convert.ToDecimal(registros["kilostotales"].ToString());


                    //obj.cantidadprimera = obj.cantidadprimera == 0 ? null : obj.cantidadprimera;
                    //obj.cantidadsegunda = obj.cantidadsegunda == 0 ? null : obj.cantidadsegunda;


                    objretornar.Add(obj);
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Desconectar();
            }
            return objretornar;
        }

        // OBTENEMOS TALLAS
        public List<TallasEntidad> getTallasRegistro()
        {
            List<TallasEntidad> objretornar = new List<TallasEntidad>();

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETTALLAS_INGRESO", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();


                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    TallasEntidad obj = new TallasEntidad();

                    obj.tamanho_ref = registros["tamanho_ref"].ToString();
                    obj.descr_tamanho = registros["descr_tamanho"].ToString();
                    obj.ordem_tamanho = Convert.ToInt32(registros["ordem_tamanho"].ToString());


                    objretornar.Add(obj);
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Desconectar();
            }
            return objretornar;
        }

        // GET LIQUIDACION RECTILINEOS PCPC
        public List<LiquidacionRectilineosPCPEntidad> getLiquidacionRectilineosPCP(string fechai, string fechaf, string partida, string estado, string tipo)
        {
            List<LiquidacionRectilineosPCPEntidad> objretornar = new List<LiquidacionRectilineosPCPEntidad>();

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_GETLIQUIRECTILINEOS_PCP", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_fechai", fechai));
                comando.Parameters.Add(new OracleParameter("i_fechaf", fechai));
                comando.Parameters.Add(new OracleParameter("i_partida", partida));
                comando.Parameters.Add(new OracleParameter("i_estado", estado));
                comando.Parameters.Add(new OracleParameter("i_tipo", tipo));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    LiquidacionRectilineosPCPEntidad obj = new LiquidacionRectilineosPCPEntidad();

                    obj.idrectilineohead = Convert.ToInt32(registros["idrectilineohead"].ToString());
                    obj.partida = registros["partida"].ToString();
                    obj.tipo = registros["tipo"].ToString();
                    obj.fichas = registros["fichas"].ToString();
                    obj.estado = registros["estado"].ToString();

                    obj.fechacrea = Convert.ToDateTime(registros["fechacrea"].ToString()).ToShortDateString();
                    obj.usuariocrea = registros["usuariocrea"].ToString();

                    obj.fechaliquidacion = registros["fechaliquidacion"].ToString() != "" ? Convert.ToDateTime(registros["fechaliquidacion"].ToString()).ToShortDateString() : null;
                    obj.usuarioliquidacion = registros["usuarioliquidacion"].ToString();

                    objretornar.Add(obj);
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Desconectar();
            }
            return objretornar;
        }


        // SET ESTADO REPORTE DE RECTILINEOS
        public bool setEstadoRectilineos(string estado, int idrectilineo, out string mensaje)
        {
            bool retornar = false;
            //string id = string.Empty;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PQ_LIQUI_RECTILINEO.SPU_SETLIQUIRECTILINEOS_PCP", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_estado", estado));
                comando.Parameters.Add(new OracleParameter("i_idrectilineo", idrectilineo));


                comando.ExecuteNonQuery();
                retornar = true;
                mensaje = "Asignado correctamente";
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