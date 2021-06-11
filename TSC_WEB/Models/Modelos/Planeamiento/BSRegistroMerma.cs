using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Planeamiento;
using TSC_WEB.Models.Entidades.Comercial;

namespace TSC_WEB.Models.Modelos.Planeamiento
{
    public class BSRegistroMerma
    {
        DBAccess conexion = new DBAccess();

        public void GenerarPorcMerma(string pcgc9, string pcgc4, string pcgc2, int pseq,int pqtdini, int pqtdfin, int pqtdperd , decimal pporcperd, string pproceso,out string poutresp,out string perror)
        {
            try
            {
                OracleCommand comando = new OracleCommand("USYSTEX.PACK_PCP_PROCESOS.SPGET_PLAN_GENERARMERMA", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();
                comando.Parameters.Add(new OracleParameter("PCGC9", pcgc9));
                comando.Parameters.Add(new OracleParameter("PCGC4", pcgc4));
                comando.Parameters.Add(new OracleParameter("PCGC2", pcgc2));
                comando.Parameters.Add(new OracleParameter("PSEQ", pseq));
                comando.Parameters.Add(new OracleParameter("PQTDINI", pqtdini));
                comando.Parameters.Add(new OracleParameter("PQTDFIN", pqtdfin));
                comando.Parameters.Add(new OracleParameter("PQTDPERD", pqtdperd));
                comando.Parameters.Add(new OracleParameter("PPORPERD", pporcperd));
                comando.Parameters.Add(new OracleParameter("PPROCESO", pproceso)); 
                comando.Parameters.Add(new OracleParameter("PRESPUES", OracleDbType.Varchar2,200)).Direction = ParameterDirection.Output;
                comando.Parameters.Add(new OracleParameter("PERROR", OracleDbType.Varchar2,200 )).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                poutresp = comando.Parameters["PRESPUES"].Value.ToString();
                perror = comando.Parameters["PERROR"].Value.ToString();
          
              
            }
            catch(Exception e){
                poutresp = e.Message;
                perror = "F";
            }
            
            conexion.Desconectar();
        }
        public void GenerarPorcMermaMasiva(int pseq, int pqtdini, int pqtdfin, int pqtdperd, decimal pporcperd, string pproceso, out string poutresp, out string perror, string pusuario)
        {
            try
            {
                OracleCommand comando = new OracleCommand("USYSTEX.PACK_PCP_PROCESOS.SPGET_PLAN_GENERARMERMAMASIVO", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();
                comando.Parameters.Add(new OracleParameter("PQTDINI", pqtdini));
                comando.Parameters.Add(new OracleParameter("PQTDFIN", pqtdfin));
                comando.Parameters.Add(new OracleParameter("PQTDPERD", pqtdperd));
                comando.Parameters.Add(new OracleParameter("PPORPERD", pporcperd));
                comando.Parameters.Add(new OracleParameter("PPROCESO", pproceso));
                comando.Parameters.Add(new OracleParameter("PUSUARIO", pusuario));
                comando.Parameters.Add(new OracleParameter("PRESPUES", OracleDbType.Varchar2, 200)).Direction = ParameterDirection.Output;
                comando.Parameters.Add(new OracleParameter("PERROR", OracleDbType.Varchar2, 200)).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                poutresp = comando.Parameters["PRESPUES"].Value.ToString();
                perror = comando.Parameters["PERROR"].Value.ToString();
            }
            catch (Exception e)
            {
                poutresp = e.Message;
                perror = "F";
            }

            conexion.Desconectar();
        }
        public List<EBMermasCliente> ListarMermaCliente(string pcgc9, string pcgc4, string pcgc2)
        {
            //try
            //{
                List<EBMermasCliente> objmermaLista = new List<EBMermasCliente>();

                OracleCommand comando = new OracleCommand("USYSTEX.PACK_PCP_PROCESOS.SPGET_PLAN_MERMACLIENTE", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();
                comando.Parameters.Add(new OracleParameter("PCGC9", pcgc9));
                comando.Parameters.Add(new OracleParameter("PCGC4", pcgc4));
                comando.Parameters.Add(new OracleParameter("PCGC2", pcgc2));
                comando.Parameters.Add(new OracleParameter("PCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                OracleDataReader registros = comando.ExecuteReader();
                while (registros.Read())
                {

                    EBMermasCliente objmerma = new EBMermasCliente();
                    objmerma.CLIENTE = Convert.ToString(registros["CLIENTE"].ToString());
                    objmerma.CGC9 = Convert.ToString(registros["CNPJ_CLIENTE9"].ToString());
                    objmerma.CGC4 = Convert.ToString(registros["CNPJ_CLIENTE4"].ToString());
                    objmerma.CGC2 = Convert.ToString(registros["CNPJ_CLIENTE2"].ToString());
                    objmerma.SEQ_LIMITE = int.Parse(registros["SEQ_LIMITE"].ToString());                    
                    objmerma.LIMITE_INFERIOR = int.Parse(registros["LIMITE_INFERIOR"].ToString());
                    objmerma.LIMITE_SUPERIOR = int.Parse(registros["LIMITE_SUPERIOR"].ToString());
                    objmerma.QTDE_PERDA = int.Parse(registros["QTDE_PERDA"].ToString());
                    objmerma.PERC_PERDA = Convert.ToDecimal(registros["PERC_PERDA"].ToString());
                    objmermaLista.Add(objmerma);
                    
                }
                conexion.Desconectar();
                return objmermaLista;
            //}
            //catch 
            //{
                
            //}

          
        }
        public void AgrDelPedidosMermaCliente(string pcgc9, string pcgc4, string pcgc2, string pusuario, string pproceso, out string poutresp, out string perror)
        {
            //try
            //{
                //AGREGAR O ELIMINAR PEDIDOS PARA REALIZAR GENERACION MASIVA DE MERMA
                 OracleCommand comando = new OracleCommand("USYSTEX.PACK_PCP_PROCESOS.SPSET_PLAN_AGREGARCLIENTE", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();
                comando.Parameters.Add(new OracleParameter("PCGC9", pcgc9));
                comando.Parameters.Add(new OracleParameter("PCGC4", pcgc4));
                comando.Parameters.Add(new OracleParameter("PCGC2", pcgc2));
                comando.Parameters.Add(new OracleParameter("PUSUARIO", pusuario));
                comando.Parameters.Add(new OracleParameter("PPROCESO", pproceso)); 
                comando.Parameters.Add(new OracleParameter("PRESPUES", OracleDbType.Varchar2,200)).Direction = ParameterDirection.Output;
                comando.Parameters.Add(new OracleParameter("PERROR", OracleDbType.Varchar2,200 )).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                poutresp = comando.Parameters["PRESPUES"].Value.ToString();
                perror = comando.Parameters["PERROR"].Value.ToString();  
                conexion.Desconectar();
            //}
            //catch 
            //{
                
            //}

        }
        public List<EBMermasCliente> ListarClienteMasiva(string pcgc9, string pcgc4, string pcgc2, string pusuario)
        {
            List<EBMermasCliente> objmermaLista = new List<EBMermasCliente>();

            OracleCommand comando = new OracleCommand("USYSTEX.PACK_PCP_PROCESOS.SPSET_PLAN_LISTARCLIENTE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("PCGC9", pcgc9));
            comando.Parameters.Add(new OracleParameter("PCGC4", pcgc4));
            comando.Parameters.Add(new OracleParameter("PCGC2", pcgc2));
            comando.Parameters.Add(new OracleParameter("PUSUARIO", pusuario));
            comando.Parameters.Add(new OracleParameter("PCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {

                EBMermasCliente objmerma = new EBMermasCliente();
                objmerma.CLIENTE = Convert.ToString(registros["CLIENTE"].ToString());
                objmerma.CGC9 = Convert.ToString(registros["CH_CG9"].ToString());
                objmerma.CGC4 = Convert.ToString(registros["CH_CG4"].ToString());
                objmerma.CGC2 = Convert.ToString(registros["CH_CG2"].ToString());
                objmerma.SEQ_LIMITE = int.Parse(registros["I_SEQ"].ToString());
                objmermaLista.Add(objmerma);

            }
            conexion.Desconectar();
            return objmermaLista;
        }
        public void DeleteClienteMasivo(int pseq,out string poutresp, out string perror,string pusurio)
        {
            try
            {
                OracleCommand comando = new OracleCommand("USYSTEX.PACK_PCP_PROCESOS.SPSET_PLAN_DELETECLIENTEMASIVO", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();
                comando.Parameters.Add(new OracleParameter("PSEQ", pseq));
                comando.Parameters.Add(new OracleParameter("PUSUARIO", pusurio));                
                comando.Parameters.Add(new OracleParameter("PRESPUES", OracleDbType.Varchar2, 200)).Direction = ParameterDirection.Output;
                comando.Parameters.Add(new OracleParameter("PERROR", OracleDbType.Varchar2, 200)).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                poutresp = comando.Parameters["PRESPUES"].Value.ToString();
                perror = comando.Parameters["PERROR"].Value.ToString();


            }
            catch (Exception e)
            {
                poutresp = e.Message;
                perror = "F";
            }

            conexion.Desconectar();
        }
    }
}