using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using System.Data;
using TSC_WEB.Models.Entidades.Corte;
using TSC_WEB.Models.Entidades.Sistema;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela.registrotendido;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela.tablasgenerales;
// EXPORTAR
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using System.Net.Http.Headers;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela
{
    public class FichasModelo
    {
        DBAccess con = null;

        public FichasModelo()
        {
            con = new DBAccess();
        }


        #region BUSCAR LIQUIDACION
        public List<Corte_001Entidad> ListarVersiones(string ficha, string version, string tipotela,string combo, string op)
        {
            //listar versiones
            List<Corte_001Entidad> Objx = new List<Corte_001Entidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                Objx.Add(new Corte_001Entidad()
                {
                    PARTIDA = Convert.ToString(registros["PARTIDA"].ToString()),
                    VERSIONES = Convert.ToInt32(registros["VERSIONES"].ToString()),

                    F_REGISTRO = Convert.ToString(registros["F_REGISTRO"].ToString()),
                    U_REGISTRO = Convert.ToString(registros["U_REGISTRO"].ToString()),
                    TIPO_TELA = Convert.ToString(registros["TIPO_TELA"].ToString()),
                    //AGREGADO
                    contenido =     Convert.ToInt32(registros["contenido"].ToString()),
                    combo       = registros["combo"].ToString()
                });
            }
            con.Desconectar();
            return Objx;
        }
        public Corte_001Entidad ListarCabeceraCort001(string ficha, string version, string tipotela, string op,string combo)
        {
            //LISTA
            Corte_001Entidad ObjFichasLista = new Corte_001Entidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.PARTIDA = Convert.ToString(registros["PARTIDA"].ToString());
                ObjFichasLista.CLIENTES = Convert.ToString(registros["CLIENTES"].ToString());
                ObjFichasLista.PROGRAMAS = Convert.ToString(registros["PROGRAMAS"].ToString());
                ObjFichasLista.PEDIDOS = Convert.ToString(registros["PEDIDOS"].ToString());
                ObjFichasLista.ESTILOS = Convert.ToString(registros["ESTILOS"].ToString());
                ObjFichasLista.ENV_TELA_MTS = Convert.ToDecimal(registros["ENV_TELA_MTS"].ToString());
                ObjFichasLista.ENV_TELA_KG = Convert.ToDecimal(registros["ENV_TELA_KG"].ToString());
                ObjFichasLista.TOTAL_RESULT = Convert.ToInt32(registros["TOTAL_RESULT"].ToString());
                ObjFichasLista.RESULTADO = Convert.ToInt32(registros["RESULTADO"].ToString());
                ObjFichasLista.TOTAL_TALLXCONSU = Convert.ToDecimal(registros["TOTAL_TALLXCONSU"].ToString());
                ObjFichasLista.VARIACION = Convert.ToDecimal(registros["VARIACION"].ToString());
                ObjFichasLista.KG_ASIGNADO = Convert.ToDecimal(registros["KG_ASIGNADO"].ToString());
                ObjFichasLista.KG_SEGUN_TIZADO = Convert.ToDecimal(registros["KG_SEGUN_TIZADO"].ToString());
                ObjFichasLista.RESUL_KG = Convert.ToDecimal(registros["RESUL_KG"].ToString());
                ObjFichasLista.LETRA_KG = Convert.ToString(registros["LETRA_KG"].ToString());
                ObjFichasLista.F_REGISTRO = Convert.ToString(registros["F_REGISTRO"].ToString());
                ObjFichasLista.U_REGISTRO = Convert.ToString(registros["U_REGISTRO"].ToString());
                ObjFichasLista.CONSU_MTS = Convert.ToDecimal(registros["CONSU_MTS"].ToString());
                ObjFichasLista.CONSU_KG = Convert.ToDecimal(registros["CONSU_KG"].ToString());
                ObjFichasLista.UTILIDAD = Convert.ToDecimal(registros["UTILIDAD"].ToString());
                ObjFichasLista.comentario = registros["comentario"].ToString();
                ObjFichasLista.despachos = registros["despachos"].ToString();
                ObjFichasLista.despachos_ser = registros["despachos_ser"].ToString();



            }
            con.Desconectar();
            return ObjFichasLista;
        }
        public ReportePivot ObjCant(string ficha, string version, string tipotela, string op,string combo)
        {
            //LISTA
            ReportePivot ObjFichasLista = new ReportePivot();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.CANTPREND = Convert.ToInt32(registros["CANTPRND"].ToString());
                ObjFichasLista.CANTPANOS = Convert.ToInt32(registros["CANTPANO"].ToString());
                ObjFichasLista.TELA = Convert.ToString(registros["TELA"].ToString());
            }
            con.Desconectar();
            return ObjFichasLista;
        }
        public List<DatosTelaEntidad> ListaDatosTela(string ficha, string version, string tipotela, string op,string combo)
        {
            //LISTA
            List<DatosTelaEntidad> ObjFichasLista = new List<DatosTelaEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.Add(new DatosTelaEntidad()
                {
                    columna1 = Convert.ToString(registros["Datos Tela"].ToString()),
                    columna2 = Convert.ToDecimal(registros["Ancho Tela"].ToString()),
                    columna3 = Convert.ToDecimal(registros["Ancho Util"].ToString()),

                });
            }
            con.Desconectar();
            return ObjFichasLista;
        }
        public DataTable ListaConsumos(string ficha, string version, string tipotela, string op,string combo)
        {
            //LISTA
            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            con.Desconectar();
            return dt;
        }
        public List<Corte_005Entidad> ListaTotal(string ficha, string version, string tipotela, string op,string combo)
        {
            //LISTA
            List<Corte_005Entidad> ObjFichasLista = new List<Corte_005Entidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.Add(new Corte_005Entidad()
                {

                    TALLAXPRENDA = Convert.ToInt32(registros["0"].ToString()),
                    LARGO_TIZADO = Convert.ToDecimal(registros["0_1"].ToString()),
                    CONSUMO_PRENDA = Convert.ToDecimal(registros["0_2"].ToString()),
                    PESO_PANO = Convert.ToDecimal(registros["0_3"].ToString()),
                    EFICIE_TIZADO = Convert.ToDecimal(registros["0_4"].ToString()),
                });
            }
            con.Desconectar();
            return ObjFichasLista;
        }
        public DataTable ListaProporciones(string ficha, string version, string tipotela, string op,string combo)
        {

            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            con.Desconectar();
            return dt;
        }

        public DataTable ListaProporcionesContraCorriente(string ficha, string version, string tipotela, string op, string combo)
        {

            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            con.Desconectar();
            return dt;
        }

        public DataTable FichasxCantidades(string ficha, string version, string tipotela, string op,string combo)
        {
            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            con.Desconectar();
            return dt;
        }

        #endregion

        #region REGISTRAR TENDIDO
        public List<FichasEntidad> Listarfichas(string ficha, string version, string tipotela, string op,string combo)
        {
            //LISTA FICHAS ASOCIADAS A LA PARTIDA
            List<FichasEntidad> ObjFichasLista = new List<FichasEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.Add(new FichasEntidad()
                {
                    FICHA = Convert.ToString(registros["ORDEM_PRODUCAO"].ToString()),
                    CANTIDAD = Convert.ToInt32(registros["QTDE_PROGRAMADA"].ToString()),
                });
            }
            con.Desconectar();
            return ObjFichasLista;
        }

        public bool RegistrarCorte007(string PARTIDA, string ETAPAS, decimal NUM_PANOS, decimal LARGO_PANOS, decimal PESO_PANOS, decimal ANCHO_TELA_REAL, decimal KXETAPAS, string U_REGISTRO, string VERSION, string TELA,int TURNO,string TONO,string CELULA,int combo)
        {
            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.CORT_007_GRABAR_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_PARTIDA", PARTIDA));
                comando.Parameters.Add(new OracleParameter("p_ETAPAS", ETAPAS));
                comando.Parameters.Add(new OracleParameter("p_NUM_PANOS", NUM_PANOS));
                comando.Parameters.Add(new OracleParameter("p_LARGO_PANOS", LARGO_PANOS));
                comando.Parameters.Add(new OracleParameter("p_PESO_PANOS", PESO_PANOS));
                comando.Parameters.Add(new OracleParameter("p_ANCHO_TELA_REAL", ANCHO_TELA_REAL));
                comando.Parameters.Add(new OracleParameter("p_KXETAPAS", KXETAPAS));
                comando.Parameters.Add(new OracleParameter("p_U_REGISTRO", U_REGISTRO));
                comando.Parameters.Add(new OracleParameter("p_VERSIONES", VERSION));
                comando.Parameters.Add(new OracleParameter("p_TELA", TELA));
                comando.Parameters.Add(new OracleParameter("p_turno", TURNO));
                comando.Parameters.Add(new OracleParameter("p_tono", TONO));
                comando.Parameters.Add(new OracleParameter("p_celula", CELULA));
                comando.Parameters.Add(new OracleParameter("p_combo", combo));



                comando.ExecuteNonQuery();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            finally
            {
                con.Desconectar();
            }

        }

        //ELIMINAR ETAPAS
        public bool EliminarEtapa(int idetapa)
        {
            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_ELIMINAR_ETAPAS", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("i_idetapa", idetapa));
                comando.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                con.Desconectar();
            }

        }

        public List<Corte_007Entidad> ListarCorte(string ficha, string version, string tipotela, string op,string combo)
        {
            //LISTA
            List<Corte_007Entidad> ObjFichasLista = new List<Corte_007Entidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.Add(new Corte_007Entidad()
                {
                    ETAPAS = Convert.ToString(registros["ETAPAS"].ToString()),
                    NUM_PANOS = Convert.ToDecimal(registros["NUM_PANOS"].ToString()),
                    LARGO_PANOS = Convert.ToDecimal(registros["LARGO_PANOS"].ToString()),
                    PESO_PANOS = Convert.ToDecimal(registros["PESO_PANOS"].ToString()),
                    ANCHO_TELA_REAL = Convert.ToDecimal(registros["ANCHO_TELA_REAL"].ToString()),
                    KXETAPAS = Convert.ToDecimal(registros["KXETAPAS"].ToString()),
                    id = Convert.ToInt32(registros["id"].ToString()),
                    u_registro = Convert.ToString(registros["u_registro"].ToString()),
                    tono = Convert.ToString(registros["tono"].ToString()),
                    celula = Convert.ToString(registros["celula"].ToString()),
                    turno = Convert.ToString(registros["turno"].ToString()),



                });
            }
            con.Desconectar();
            return ObjFichasLista;
        }

        // REGISTRA CORT_006 (CABECERA TENDIDO)
        public bool RegistrarCorte006(cort006Entidad cort006,out string mensaje)
        {
            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.CORT_006_GRABAR", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("i_ficha", cort006.ficha));
                comando.Parameters.Add(new OracleParameter("i_combo", cort006.combo));
                comando.Parameters.Add(new OracleParameter("i_version", cort006.versiones));
                comando.Parameters.Add(new OracleParameter("i_tipotela", cort006.tipo_tela));
                comando.Parameters.Add(new OracleParameter("i_turno", cort006.turno));
                comando.Parameters.Add(new OracleParameter("i_mer_puntas", cort006.mer_puntas));
                comando.Parameters.Add(new OracleParameter("i_mer_retazosmas", cort006.mer_retazosmas));
                comando.Parameters.Add(new OracleParameter("i_mer_retazosmen", cort006.mer_retazosmen));
                comando.Parameters.Add(new OracleParameter("i_empalmes", cort006.empalmes));
                comando.Parameters.Add(new OracleParameter("i_devo_primera", cort006.devo_primera));
                comando.Parameters.Add(new OracleParameter("i_devo_segunda", cort006.devo_segunda));
                comando.Parameters.Add(new OracleParameter("i_conos", cort006.conos));
                comando.Parameters.Add(new OracleParameter("i_plastico", cort006.plastico));
                comando.Parameters.Add(new OracleParameter("i_u_registro", cort006.u_registro));
                comando.Parameters.Add(new OracleParameter("i_kgextras", cort006.kg_adicionales));
                comando.Parameters.Add(new OracleParameter("i_motadicional", cort006.mot_kgadicional));
                comando.Parameters.Add(new OracleParameter("i_kgentregados", cort006.kgentregados));
                comando.Parameters.Add(new OracleParameter("i_devo_primera_mot", cort006.devo_primera_mot));
                comando.Parameters.Add(new OracleParameter("i_devo_segunda_mot", cort006.devo_segunda_mot));
                comando.Parameters.Add(new OracleParameter("i_almacen", cort006.almacen));
                comando.Parameters.Add(new OracleParameter("i_fecha", cort006.f_registro));
                comando.Parameters.Add(new OracleParameter("i_estadotendido", cort006.estadotendido));
                comando.Parameters.Add(new OracleParameter("i_estadocorte", cort006.estadocorte));


                comando.ExecuteNonQuery();
                mensaje = "Registrado correctamente";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
            finally
            {
                con.Desconectar();
            }
        }

        // REGISTRAR CORT 011
        public bool RegistrarCorte011(string PARTIDA,  int versiones,string tipotela, int idmotivo /*, int turno*/,int combo)
        {
            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.CORT_011_GRABAR", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_PARTIDA", PARTIDA));
                comando.Parameters.Add(new OracleParameter("P_VERSIONES", versiones));
                comando.Parameters.Add(new OracleParameter("p_TIPO_TELA", tipotela));
                comando.Parameters.Add(new OracleParameter("p_KGADICIONAL", idmotivo));
                comando.Parameters.Add(new OracleParameter("P_COMBO", combo));
                
                comando.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Desconectar();
            }
        }

        public bool EliminarCorte011(string PARTIDA, int versiones, string tipotela,int combo)
        {
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.CORT_011_ELIMINAR", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_PARTIDA", PARTIDA));
                comando.Parameters.Add(new OracleParameter("P_VERSIONES", versiones));
                comando.Parameters.Add(new OracleParameter("p_TIPO_TELA", tipotela));
                comando.Parameters.Add(new OracleParameter("p_combo", combo));


                comando.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Desconectar();
            }
        }

        public Corte_006Entidad BuscarCabe(string ficha, string version, string tipotela, string op,string combo)
        {
            //LISTA
            Corte_006Entidad ObjFichasLista = new Corte_006Entidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.MER_PUNTAS = Convert.ToDecimal(registros["MER_PUNTAS"].ToString());
                ObjFichasLista.MER_RETAZOSMAS = Convert.ToDecimal(registros["MER_RETAZOSMAS"].ToString());
                ObjFichasLista.MER_RETAZOSMEN = Convert.ToDecimal(registros["MER_RETAZOSMEN"].ToString());
                ObjFichasLista.EMPALMES = Convert.ToDecimal(registros["EMPALMES"].ToString());
                ObjFichasLista.DEVO_PRIMERA = Convert.ToDecimal(registros["DEVO_PRIMERA"].ToString());
                ObjFichasLista.DEVO_SEGUNDA = Convert.ToDecimal(registros["DEVO_SEGUNDA"].ToString());
                ObjFichasLista.CONOS = Convert.ToDecimal(registros["CONOS"].ToString());
                ObjFichasLista.PLASTICO = Convert.ToDecimal(registros["PLASTICO"].ToString());
                ObjFichasLista.KG_ADICIONALES = registros["KG_ADICIONALES"].ToString();
                ObjFichasLista.MOT_KGADICIONAL = registros["MOT_KGADICIONAL"].ToString();
                ObjFichasLista.PARTIDA = registros["PARTIDA"].ToString();
                ObjFichasLista.KGENTREGADOS = Convert.ToDecimal(registros["KGENTREGADOS"].ToString());

                ObjFichasLista.DEVO_PRIMERA_MOT = registros["DEVO_PRIMERA_MOT"].ToString() == "" ? Convert.ToInt32("0") : Convert.ToInt32(registros["DEVO_PRIMERA_MOT"].ToString());
                ObjFichasLista.DEVO_SEGUNDA_MOT = registros["DEVO_SEGUNDA_MOT"].ToString() == "" ? Convert.ToInt32("0") : Convert.ToInt32(registros["DEVO_SEGUNDA_MOT"].ToString());

                ObjFichasLista.ESTADO = registros["ESTADO"].ToString();
                ObjFichasLista.fecha = registros["f_registro"].ToString();




            }
            con.Desconectar();
            return ObjFichasLista;
        }

        // BUSCAR MOTIVOS
        public string[] BuscarMotivos (string ficha, string version, string tipotela, string op,int combo)
        {
            //LISTA
            Corte_006Entidad ObjFichasLista = new Corte_006Entidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            List<string> miLista = new List<string>();

            while (registros.Read())
            {
                miLista.Add(registros["idkgadicional"].ToString());
            }

            con.Desconectar();

            return miLista.ToArray();
        }



        #endregion


        public ReportePivot BuscarExiste(string ficha, string version, string tela, string op)
        {
            //LISTA
            ReportePivot ObjFichasLista = new ReportePivot();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.EXISTE = Convert.ToInt32(registros["EXISTE"].ToString());
            }
            con.Desconectar();
            return ObjFichasLista;
        }


        #region REGISTRAR CORTE
        public ReportePivot BuscarExisteCort007(string ficha, string version, string tela, string op,string combo)
        {
            //LISTA
            ReportePivot ObjFichasLista = new ReportePivot();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.EXISTE = Convert.ToInt32(registros["EXISTE"].ToString());
            }
            con.Desconectar();
            return ObjFichasLista;
        }

        public SistemaEntidad RegistrarCorte008(cort008Entidad cort008)
        {

            SistemaEntidad objSistema = new SistemaEntidad();

            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.CORT_008_GRABAR", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("i_ficha", cort008.ficha));
                comando.Parameters.Add(new OracleParameter("i_combo", cort008.combo));
                comando.Parameters.Add(new OracleParameter("i_version", cort008.version));
                comando.Parameters.Add(new OracleParameter("i_tipotela", cort008.tipotela));
                comando.Parameters.Add(new OracleParameter("i_turno", cort008.turno));
                comando.Parameters.Add(new OracleParameter("i_orillos", cort008.orillos));
                comando.Parameters.Add(new OracleParameter("i_extremos", cort008.extremos));
                comando.Parameters.Add(new OracleParameter("i_entrecorte", cort008.entrecorte));
                comando.Parameters.Add(new OracleParameter("i_u_registro", cort008.u_registro));
                comando.Parameters.Add(new OracleParameter("i_etapa", cort008.etapa));


                comando.ExecuteNonQuery();
                //return true;
                objSistema.respuestabool = true;
                objSistema.mensajesistema = "Realizado correctamente";

            }
            catch (Exception ex)
            {
                objSistema.respuestabool = false;
                objSistema.mensajesistema = ex.Message;
            }
            finally
            {
                con.Desconectar();

            }

            return objSistema;
        }


        #endregion



        public bool ModificarCort007(int id, decimal num_panos, decimal largo_panos, decimal peso_panos, decimal ancho_tela_real, decimal kxetapas,string tono,string celula)
        {
            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.CORT_007_MODIFICAR", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_id", id));
                //comando.Parameters.Add(new OracleParameter("p_etapas", etapas));
                comando.Parameters.Add(new OracleParameter("p_num_panos", num_panos));
                comando.Parameters.Add(new OracleParameter("p_largo_panos", largo_panos));
                comando.Parameters.Add(new OracleParameter("p_peso_panos", peso_panos));
                comando.Parameters.Add(new OracleParameter("p_ancho_tela_real", ancho_tela_real));
                comando.Parameters.Add(new OracleParameter("p_kxetapas", kxetapas));
                comando.Parameters.Add(new OracleParameter("p_tono", tono));
                comando.Parameters.Add(new OracleParameter("p_celula", celula));


                comando.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Desconectar();
            }
        }

        // TRANSACCIONES REPORTE
        public List<ReporteTransaccionEntidad> getReporteTransaccion() {

            //LISTA
            List<ReporteTransaccionEntidad> ObjFichasLista = new List<ReporteTransaccionEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_LIQUIDACION_REPORTETRANC", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.Add(new ReporteTransaccionEntidad()
                {
                    ficha = Convert.ToString(registros["ficha"].ToString()),
                    partida = Convert.ToString(registros["partida"].ToString()),
                    doc_movimento = Convert.ToString(registros["doc_movimento"].ToString()),
                    almacen = Convert.ToString(registros["almacen"].ToString()),
                    cantidad = Convert.ToString(registros["cantidad"].ToString()),
                    rollo = Convert.ToString(registros["rollo"].ToString()),
                    estado = Convert.ToString(registros["estado"].ToString()),

                });
            }
            con.Desconectar();
            return ObjFichasLista;


        }


        public List<Corte_008Entidad> BuscarRegistro008(string ficha, string version, string tela, string op,string combo)
        {
            //LISTA
            List<Corte_008Entidad> ObjFichasLista = new List<Corte_008Entidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("p_combo", combo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                Corte_008Entidad ObjFichasListaE = new Corte_008Entidad();

                ObjFichasListaE.PARTIDA = Convert.ToString(registros["PARTIDA"].ToString());
                ObjFichasListaE.ORILLOS = Convert.ToDecimal( registros["ORILLOS"].ToString() == "" ? "0" : registros["ORILLOS"].ToString() );
                ObjFichasListaE.EXTREMOS = Convert.ToDecimal(registros["EXTREMOS"].ToString() == "" ? "0" : registros["EXTREMOS"].ToString());
                ObjFichasListaE.ENTRECORTE = Convert.ToDecimal(registros["ENTRECORTE"].ToString() == "" ? "0" : registros["ENTRECORTE"].ToString());

                ObjFichasListaE.PANOS = Convert.ToDecimal(registros["num_panos"].ToString() == "" ? "0" : registros["num_panos"].ToString());

                ObjFichasListaE.F_REGISTRO = Convert.ToString(registros["F_REGISTRO"].ToString());
                ObjFichasListaE.U_REGISTRO = Convert.ToString(registros["U_REGISTRO"].ToString());

                ObjFichasListaE.ETAPA = Convert.ToString(registros["ETAPAS"].ToString());

                ObjFichasLista.Add(ObjFichasListaE);
            }
            con.Desconectar();
            return ObjFichasLista;
        }


        //RETORNA COMPARACION KILOS ASIGNADOS - KILOS SEGUN TIZADO
        public comparacionkgEntidad getComparacionLiquidacion(int ficha, int version, string tela,int combo)
        {
            //LISTA
            comparacionkgEntidad objRetornar = new comparacionkgEntidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_COMPAKGTENDIDO", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
            comando.Parameters.Add(new OracleParameter("i_version", version));
            comando.Parameters.Add(new OracleParameter("i_tela", tela));
            comando.Parameters.Add(new OracleParameter("i_combo", combo));


            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objRetornar.kgliquidados            = Convert.ToDouble(registros["kgliquidados"].ToString());
                objRetornar.kgtizados               = Convert.ToDouble(registros["kgtizados"].ToString());
                objRetornar.porcentajeliquidacion   = Convert.ToDouble(registros["porcentajeliquidacion"].ToString());
                objRetornar.kgdespachadoalmacen     = Convert.ToDouble(registros["kgdespachadoalmacen"].ToString());
                objRetornar.kgadicional             = Convert.ToDouble(registros["kgadicional"].ToString());

            }
            con.Desconectar();
            return objRetornar;
        }


        //EXPORTAR REPORTE DE LIQUIDACION
        public MemoryStream ExportarReporteLiquidacion(List<ReporteLiquidacionEntidad> listado)
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Hoja1");

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Size = 8;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(1).Style.Fill.BackgroundColor.SetColor(Color.White);

                workSheet.Cells["A1"].Value = "MODULO";
                workSheet.Cells["B1"].Value = "CODIGO AUTORIZA";
                workSheet.Cells["C1"].Value = "CODIGO";
                workSheet.Cells["D1"].Value = "PROVEEDOR";
                workSheet.Cells["E1"].Value = "FECHA";
                workSheet.Cells["F1"].Value = "CENTRO DE COSTO";
                workSheet.Cells["G1"].Value = "TIPO PAGO";
                workSheet.Cells["H1"].Value = "VALOR SOLES";
                workSheet.Cells["I1"].Value = "VALOR DOLAR";
                workSheet.Cells["J1"].Value = "VALOR DOLAR - CONSUMIDO";

                workSheet.Column(1).Width = 10;     // MODULO
                workSheet.Column(2).Width = 10;     // CODIGO AUTORIZA
                workSheet.Column(3).Width = 10;     // CODIGO
                workSheet.Column(4).Width = 25;     // PROVEEDOR
                workSheet.Column(5).Width = 15;     // FECHA
                workSheet.Column(6).Width = 30;     // CENTRO DE COSTO
                workSheet.Column(7).Width = 25;     // TIPO PAGO
                workSheet.Column(8).Width = 20;     // VALOR SOLES
                workSheet.Column(9).Width = 20;     // VALOR DOLAR
                workSheet.Column(10).Width = 20;     // VALOR DOLAR

                int i = 2;
                /*
                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.MODULO;
                    workSheet.Cells[i, 2].Value = item.CODAUTORIZA;
                    workSheet.Cells[i, 3].Value = item.CODIGO;
                    workSheet.Cells[i, 4].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 5].Value = item.FECHA;
                    workSheet.Cells[i, 6].Value = item.CC;
                    workSheet.Cells[i, 7].Value = item.TIPO_PAGO;
                    workSheet.Cells[i, 8].Value = item.V_SOLES;
                    workSheet.Cells[i, 9].Value = item.V_DOLAR;
                    workSheet.Cells[i, 10].Value = item.V_CONSUMIDO;

                    i++;
                }*/

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }

        // DIVIDIR ETAPAS
        public bool DivirEtapas(int id, decimal cantidad,string usuario,out string mensaje)
        {
            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.spu_liquidacion_dividir_etapas", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_id", id));
                comando.Parameters.Add(new OracleParameter("i_cantidad", cantidad));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));

                comando.ExecuteNonQuery();
                mensaje = "Etapa dividida correctamente";
                return true;
            }
            catch(Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
            finally
            {
                con.Desconectar();
            }
        }
            
        // MOSTRAR ESTADO DE TENDIDO O DE CORTE
        public FichasEntidad getEstadoLiquidacion(int ficha,string combo, string version, string tela)
        {
            FichasEntidad objF = new FichasEntidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_LIQUI_ESTADOS_NEW", con.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            con.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.Parameters.Add(new OracleParameter("i_combo", combo));
                comando.Parameters.Add(new OracleParameter("i_version", version));
                comando.Parameters.Add(new OracleParameter("i_tipotela", tela));
                comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                OracleDataReader registros = comando.ExecuteReader();


                while (registros.Read())
                {
                    objF.estadocorte = Convert.ToInt16(registros["estadocorte"].ToString());
                    objF.estadotendido = Convert.ToInt16(registros["estadotendido"].ToString());
                }

            }
            catch (Exception ex)
            {
                ;
            }
            finally
            {
                con.Desconectar();
            }

            return objF;

        }

        

    }
}