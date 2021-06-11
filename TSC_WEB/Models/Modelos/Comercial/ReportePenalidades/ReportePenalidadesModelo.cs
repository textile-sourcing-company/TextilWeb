using Dapper;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Comercial.ReportePenalidades;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Modelos.Comercial.ReportePenalidades
{
    public class ReportePenalidadesModelo
    {
       

        public async Task<IEnumerable<ReportePenalidadesEntidad>> ListarPenalidades(string Cliente, string Documento, string Fechainicio, string Fechafin)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@V_CLIENTE", Cliente, DbType.String, ParameterDirection.Input);
                    sp_parametros.Add("@V_DOCUMENTO", Documento, DbType.String, ParameterDirection.Input);
                    sp_parametros.Add("@V_FECHAINICIO", Fechainicio, DbType.String, ParameterDirection.Input);
                    sp_parametros.Add("@V_FECHAFIN", Fechafin, DbType.String, ParameterDirection.Input);
                    var resultado = await conexion.QueryAsync<ReportePenalidadesEntidad>("usp_listapenalidades",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void InsertarDatosPenalidades(string documento, string concepto, string descripcion, string centrocosto, string usuarioregistro)
        {
            UsuariosEntidad objuusarios = new UsuariosEntidad();
            DBAccess conexion = new DBAccess();
            ReportePenalidadesEntidad objpe = new ReportePenalidadesEntidad();
            try
            {
                //SqlCommand comando = new SqlCommand("UspInsertaActualizaCome001",DBAccess.ObtenerConexionSQL());
                //DBAccess.ObtenerConexionSQL().Open();                
                //comando.CommandType = CommandType.StoredProcedure;
                //comando.Parameters.AddWithValue("@ndocumento", documento);
                //comando.Parameters.AddWithValue("@concepto", concepto);
                //comando.Parameters.AddWithValue("@descripcion", descripcion);
                //comando.Parameters.AddWithValue("@centrocosto", centrocosto);
                //comando.Parameters.AddWithValue("@usuarioreg", usuarioregistro);                
                //comando.ExecuteNonQuery();
                SqlConnection con = new SqlConnection("Data Source=172.16.87.9;Initial Catalog =bd_tsc;User Id=sa;Password=$tscafl.2020");
                con.Open();
                SqlCommand com = new SqlCommand(); // Create a object of SqlCommand class
                com.Connection = con; //Pass the connection object to Command
                com.CommandType = CommandType.StoredProcedure; // We will use stored procedure.
                com.CommandText = "UspInsertaActualizaCome001"; //Stored Procedure Name
                com.Parameters.AddWithValue("@ndocumento", documento);
                com.Parameters.AddWithValue("@concepto", concepto);
                com.Parameters.AddWithValue("@descripcion", descripcion);
                com.Parameters.AddWithValue("@centrocosto", centrocosto);
                com.Parameters.AddWithValue("@usuarioreg", usuarioregistro);
                //com.Parameters.AddWithValue("@usuarioreg", objuusarios.usuario.ToString() );
                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //string Message = e.Message;
                
            }
            DBAccess.ObtenerConexionSQL().Close();
            conexion.Desconectar();
        }
        //public void InsertarDatosPenalidades(ReportePenalidadesEntidad rptpenalidades)
        //{
            
        //    try
        //    {
        //        SqlCommand comando = new SqlCommand("UspInsertaActualizaCome001", DBAccess.ObtenerConexionSQL());
        //        comando.CommandType = CommandType.StoredProcedure;
        //        comando.Parameters.AddWithValue("@ndocumento", rptpenalidades.documento);
        //        comando.Parameters.AddWithValue("@concepto", rptpenalidades.concepto);
        //        comando.Parameters.AddWithValue("@descripcion", rptpenalidades.descripcion);
        //        comando.Parameters.AddWithValue("@centrocosto", rptpenalidades.centrocosto);
        //        comando.Parameters.AddWithValue("@usuarioreg", rptpenalidades.usuarioregistro);
        //        DBAccess.ObtenerConexionSQL().Open();
        //        comando.ExecuteNonQuery();
        //        DBAccess.ObtenerConexionSQL().Close();
           
        //    }
        //    catch (Exception e)
        //    {
        //        if (DBAccess.ObtenerConexionSQL().State == ConnectionState.Open) 
        //        {
        //            DBAccess.ObtenerConexionSQL().Close();
        //        }
     
        //    }
        //}
    }
}