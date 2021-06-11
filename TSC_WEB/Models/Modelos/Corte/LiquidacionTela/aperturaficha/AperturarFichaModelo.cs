using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela.aperturaficha;


namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela.aperturaficha
{
    public class AperturarFichaModelo
    {
        DBAccess conexion = new DBAccess();

        // BUSCAMOS FICHAS PARA APERTURAR
        public List<AperturaFichaEntidad> getFichasApertura(int? semana,int? aniosemana,string fecha,string partida,int? ficha,string estadotendido,string estadocorte)
        {
            List<AperturaFichaEntidad> objLista = new List<AperturaFichaEntidad>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_GETFICHAAPERTURA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_semana", semana ));
            comando.Parameters.Add(new OracleParameter("i_aniosemana", aniosemana ));
            comando.Parameters.Add(new OracleParameter("i_fecha", fecha == string.Empty ? null : fecha));
            comando.Parameters.Add(new OracleParameter("i_partida", partida == string.Empty ? null : partida));
            comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
            comando.Parameters.Add(new OracleParameter("i_estadotendido", estadotendido == string.Empty ? null : estadotendido));
            comando.Parameters.Add(new OracleParameter("i_estadocorte", estadocorte == string.Empty ? null : estadocorte));

            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                AperturaFichaEntidad obj = new AperturaFichaEntidad();

                obj.partida             = registros["partida"].ToString();
                obj.combo               = Convert.ToInt32(registros["combo"].ToString());
                obj.version             = Convert.ToInt32(registros["version"].ToString());
                obj.tipotela            = registros["tipotela"].ToString();
                obj.fichaspartida       = registros["fichaspartida"].ToString();
                obj.fichasdespachada    = registros["fichasdespachada"].ToString();
                obj.estadotendido       = registros["estadotendido"].ToString();
                obj.estadocorte         = registros["estadocorte"].ToString();

                objLista.Add(obj);
            }

            conexion.Desconectar();
            return objLista;
        }
        
        // APERTURAR FICHAS
        public bool AperturaFichas(string partida,int combo,int version,string tipotela,string estado,string opcion,out string mensaje)
        {
            bool success = true;

            try
            {
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_LIQUIAPERTURAFICHA", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_partida",partida));
                comando.Parameters.Add(new OracleParameter("i_combo", combo));
                comando.Parameters.Add(new OracleParameter("i_version", version));
                comando.Parameters.Add(new OracleParameter("i_tipotela", tipotela));
                comando.Parameters.Add(new OracleParameter("i_estado", estado));
                comando.Parameters.Add(new OracleParameter("i_opcion", opcion));

                comando.ExecuteNonQuery();
                success = true;
                mensaje = "Ficha Aperturada";

            }
            catch (Exception ex)
            {
                success = false;
                mensaje = ex.Message.ToString();
            }
            finally
            {
                conexion.Desconectar();
            }

            return success;
        }


    }
}