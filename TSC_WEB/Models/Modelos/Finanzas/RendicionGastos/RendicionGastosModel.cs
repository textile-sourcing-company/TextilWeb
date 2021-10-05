using Dapper;
using OfficeOpenXml;
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
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Finanzas;
using TSC_WEB.Models.Entidades.Finanzas.RendicionGastos;

namespace TSC_WEB.Models.Modelos.Finanzas.RendicionGastos
{
    public class RendicionGastosModel
    {

        public StatusResponse SetRendicionGastos(SPS_Parametro parametro)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", parametro.opcion, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", parametro.idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@codigo", parametro.codigo, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idEmpresa", parametro.idEmpresa, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", parametro.idEstado, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idAnulado", parametro.idAnulado, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoMod", parametro.idTipoMod, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@observacion", parametro.observacion, DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codCeCo", parametro.codCeCo, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoDet", parametro.idConceptoDet, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", parametro.secuencia, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@usuario", parametro.usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@usuarioCompleto", parametro.usuarioCompleto, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", parametro.nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", parametro.idTipo, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@seleccionadoDet", parametro.seleccionadoDet, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", parametro.idConceptoCab, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@cantDias", parametro.cantDias, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@montoSolicitado", parametro.montoSolicitado, DbType.Decimal, ParameterDirection.Input);
                sp_parametros.Add("@observacionDet", parametro.observacionDet, DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@ctpava_bnf", parametro.ctpava_bnf, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@canvar_bnf", parametro.canvar_bnf, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@colaborador", parametro.colaborador, DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codSede", parametro.codSede, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaAplicacion", parametro.fechaAplicacion, DbType.String, ParameterDirection.Input);

                return conexion.QuerySingle<StatusResponse>("dbo.uspSetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public StatusResponse AprobRendicionGastos(SPA_Parametro parametro)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", parametro.opcion, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@opcionTipoAprob", parametro.opcionTipoAprob, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", parametro.idSolicitud, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", parametro.secuencia, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@valorEntrega", parametro.valorEntrega, DbType.Decimal, ParameterDirection.Input);
                sp_parametros.Add("@usuario", parametro.usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", parametro.nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idAnulado", parametro.idAnulado, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idAnuladoDet", parametro.idAnuladoDet, DbType.Int32, ParameterDirection.Input);

                return conexion.QuerySingle<StatusResponse>("dbo.uspSetRendicionAprobacion",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<IEnumerable<SPG4_PermisoXCeCo>> PermisosXCeCo(int nivelInterfaz, string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 4, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG4_PermisoXCeCo>("dbo.uspGetRendicionGastos",
                                                                    sp_parametros,
                                                                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG1_ConceptoCab>> ConceptosCab(int idSolicitud, int idTipo)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 1, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", idTipo, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG1_ConceptoCab>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG65_ConceptoCab>> ConceptosCabReembolso()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 65, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input); 
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG65_ConceptoCab>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<G01_TipoMoneda>> TiposDeMoneda()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 5, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<G01_TipoMoneda>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }



        public SPG6_SolicitudCab SolicitudCab(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 6, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<SPG6_SolicitudCab>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG7_SolicitudDet>> SolicitudDet(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 7, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG7_SolicitudDet>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<SPG8_SolicitudDetalle> IdSolicitudDetalle(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 8, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryFirstOrDefaultAsync<SPG8_SolicitudDetalle>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG9_ListaHistorial>> ListaHistorial(string fechaInicio, string fechaFin)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 9, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", fechaInicio, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", fechaFin, DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG9_ListaHistorial>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<SPG10_SolicitudXCodigo> SolicitudXCodigo(string codigo, string tipoInterfaz)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 10, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", codigo, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", tipoInterfaz, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryFirstOrDefaultAsync<SPG10_SolicitudXCodigo>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG11_ListaPendientes>> PendientesAprobacion20(int nivelInterfaz, string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 11, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG11_ListaPendientes>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<SPG12_SolicitudCab> PendAprob20Cab(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 12, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryFirstOrDefaultAsync<SPG12_SolicitudCab>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }




        public List<SPG13_SolicitudDet> PendAprob20Det(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 13, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG13_SolicitudDet>("dbo.uspGetRendicionGastos",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<IEnumerable<SPG14_Historial20>> Historial20(string fechaInicio, string fechaFin, string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 14, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", fechaInicio, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", fechaFin, DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG14_Historial20>("dbo.uspGetRendicionGastos",
                                                                    sp_parametros,
                                                                    commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<IEnumerable<SPG15_ListaPendientes30>> PendientesAprobacion30(int nivelInterfaz, string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 15, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG15_ListaPendientes30>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<SPG16_SolicitudCab> Pendientes30Cab(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 16, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryFirstOrDefaultAsync<SPG16_SolicitudCab>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG17_SolicitudDet> Pendientes30Det(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 17, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG17_SolicitudDet>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG18_Historial>> HistorialRendiciones(string fechaInicio, string fechaFin, string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 18, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", fechaInicio, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", fechaFin, DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG18_Historial>("dbo.uspGetRendicionGastos",
                                                                    sp_parametros,
                                                                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG19_Tipos>> Tipos()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 19, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG19_Tipos>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG20_ConceptoDetalle>> ConceptosDetalleInsertar(int idConceptoCab, int idTipoComp)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 20, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", idConceptoCab, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", idTipoComp, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG20_ConceptoDetalle>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG21_Estados>> EstadosSolcitud()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 21, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG21_Estados>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<IEnumerable<SPG22_TipoComprobante>> TipoComprobantes()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 22, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG22_TipoComprobante>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public SPG23_BusquedaXCod SolBusquedaXCod(string codigo)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 23, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", codigo, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<SPG23_BusquedaXCod>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }



        public List<SPG25_ConceptoDetalle> ConceptosDetalleVisualizar(int idSolicitud, int idConceptoCab)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 25, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", idConceptoCab, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG25_ConceptoDetalle>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<SPG30_ConceptoDetalle> ConceptosDetalleEditar(int idSolicitud, int idConceptoCab)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 30, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", idConceptoCab, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG30_ConceptoDetalle>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure).ToList();
            }
        }



        public List<SPG26_SolDetConceptos> SolicitudDetalleConcepto(int idSolicitud, int idConceptoCab)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 26, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", idConceptoCab, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG26_SolDetConceptos>("dbo.uspGetRendicionGastos",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure).ToList();
            }
        }


        public async Task<IEnumerable<SPG27_Dias>> Dias()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 27, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG27_Dias>("dbo.uspGetRendicionGastos",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<SPG28_DetalleEditar> DetalleEditar(int idSolicitud, int idConceptoCab)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 28, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", idConceptoCab, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryFirstOrDefaultAsync<SPG28_DetalleEditar>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG29_ConceptoCabEdit>> ConceptosCabEdit(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 29, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG29_ConceptoCabEdit>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<SPS1_Proveedor> SetProveedorValidar(SPS_Proveedor parametro)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", parametro.opcion, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@tipoPersona", parametro.tipoPersona, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", parametro.ruc, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@rs", parametro.rs, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@direccion", parametro.direccion, DbType.String, ParameterDirection.Input);

                return await conexion.QuerySingleOrDefaultAsync<SPS1_Proveedor>("dbo.uspSetRendicionGastosProv",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<SPS2_Proveedor> SetProveedorRegistrar(SPS_Proveedor parametro)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", parametro.opcion, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@tipoPersona", parametro.tipoPersona, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", parametro.ruc, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@rs", parametro.rs, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@direccion", parametro.direccion, DbType.String, ParameterDirection.Input);

                return await conexion.QuerySingleOrDefaultAsync<SPS2_Proveedor>("dbo.uspSetRendicionGastosProv",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<IEnumerable<SPG31_PendientesRendicion>> PendientesRendicion(string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 31, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG31_PendientesRendicion>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG32_DetalleSolicitud> SolicitudDetalleRendicion(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 32, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG32_DetalleSolicitud>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public StatusResponse SetRendicionComprobante(SPS_ComprobanteParametro parametro)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", parametro.opcion, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", parametro.idRendCompte, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", parametro.idTipoComp, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@nserie", parametro.nserie, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ndocof", parametro.ndocof, DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@rucDNI", parametro.rucDNI, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@rs", parametro.rs, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigoPC", parametro.codigoPC, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@obs", parametro.obs, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaEmision", parametro.fechaEmision, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@periodo", parametro.periodo, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ceco", parametro.ceco, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoMod", parametro.idTipoMod, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@porcentajeIGV", parametro.porcentajeIGV, DbType.Decimal, ParameterDirection.Input);
                sp_parametros.Add("@obs1", parametro.obs1, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@obs2", parametro.obs2, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fecha1", parametro.fecha1, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fecha2", parametro.fecha2, DbType.String, ParameterDirection.Input);
                
                sp_parametros.Add("@idSolicitud", parametro.idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", parametro.secuencia, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idRendCompteDet", parametro.idRendCompteDet, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fecha", parametro.fecha, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@detalle1", parametro.detalle1, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@detalle2", parametro.detalle2, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codum", parametro.codum, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@valorunit", parametro.valorunit, DbType.Decimal, ParameterDirection.Input);
                sp_parametros.Add("@cantidad", parametro.cantidad, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@seccion", parametro.seccion, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@montoDevolucion", parametro.montoDevolucion, DbType.Decimal, ParameterDirection.Input);

                return conexion.QuerySingleOrDefault<StatusResponse>("dbo.uspSetRendicionComprobante",
                                                                    sp_parametros,
                                                                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG33_PendAprobRendGastos>> PendientesAprobRendGastos(int nivelInterfaz, string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 33, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG33_PendAprobRendGastos>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }

        public SPG34_PendApbRdGstsCab PendientesAprobRendGastosCab(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 34, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<SPG34_PendApbRdGstsCab>("dbo.uspGetRendicionGastos",
                                                    sp_parametros,
                                                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<SPG35_PendApbRdGstsDet> PendientesAprobRendGastosDet(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 35, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG35_PendApbRdGstsDet>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG36_RndGstsAprobGstsFnzas>> ListaRendicionFinal(int nivelInterfaz, string usuario, string fechaInicio, string fechaFin, string codigo)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 36, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", fechaInicio, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", fechaFin, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", codigo, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG36_RndGstsAprobGstsFnzas>("dbo.uspGetRendicionGastos",
                                                                            sp_parametros,
                                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public SPG37_DetalleCab RendGstsPend30Cab(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 37, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<SPG37_DetalleCab>("dbo.uspGetRendicionGastos",
                                                    sp_parametros,
                                                    commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG38_DetalleDet> RendGstsPend30Det(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 38, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG38_DetalleDet>("dbo.uspGetRendicionGastos",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG39_Tipos>> TiposAprobGstsFnzs()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 39, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG39_Tipos>("dbo.uspGetRendicionGastos",
                                                    sp_parametros,
                                                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG40_Colaborador>> ColaboradorSofya(string filtro, string usuario, int nivelInterfaz)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 40, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@filtro", filtro.ToUpper(), DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG40_Colaborador>("dbo.uspGetRendicionGastos",
                                            sp_parametros,
                                            commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG41_RendicionDetalle>> RendicionDetalle(int idRendCompte)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 41, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", idRendCompte, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG41_RendicionDetalle>("dbo.uspGetRendicionGastos",
                                            sp_parametros,
                                            commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<SPG42_ColaboradorSofya> BuscarColaboradorXDNI(string filtro)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 42, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@filtro", filtro, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryFirstOrDefaultAsync<SPG42_ColaboradorSofya>("dbo.uspGetRendicionGastos",
                                            sp_parametros,
                                            commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<SPG43_EstadoSolicitud> EstadoSolicitud(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 43, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryFirstOrDefaultAsync<SPG43_EstadoSolicitud>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG44_UnidadesMedida>> UnidadesMedida()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 44, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG44_UnidadesMedida>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG45_SolicitudDetalle>> SolicitudDetReembolso(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 45, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG45_SolicitudDetalle>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG46_CmptsRegistrados>> ComprobantesRegistrados(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 46, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG46_CmptsRegistrados>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }

        public SPG47_CmpbteCabecera ComprobateCabecera(int idRendCompte)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 47, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", idRendCompte, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<SPG47_CmpbteCabecera>("dbo.uspGetRendicionGastos",
                                                                            sp_parametros,
                                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG48_CmpbteDetalle> ComprobateDetalle(int idRendCompte)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 48, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", idRendCompte, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG48_CmpbteDetalle>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<SPG49_ComprobanteCab> RpteComprobanteIndivualCab(int idSolicitud, int secuencia)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 49, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", secuencia, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG49_ComprobanteCab>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public int CodigoComprobante(int idSolicitud, int secuencia)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 50, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", secuencia, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<int>("dbo.uspGetRendicionGastos",
                    sp_parametros,
                    commandType: CommandType.StoredProcedure);
            }
        }


        public SPG51_DecJurCab RpteCpbteIndivualDJCab(int idRendCompte)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 51, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", idRendCompte, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QuerySingleOrDefault<SPG51_DecJurCab>("dbo.uspGetRendicionGastos",
                                                sp_parametros,
                                                commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<SPG52_DecJurDet> RpteCpbteIndivualDJDet(int idRendCompte)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 52, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", idRendCompte, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG52_DecJurDet>("dbo.uspGetRendicionGastos",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
            }
        }




        public SPG53_RpteCabIndividual RpteCpbteIndivualCab(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 53, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<SPG53_RpteCabIndividual>("dbo.uspGetRendicionGastos",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG54_RpteDetIndividual> RpteCpbteIndivualDet(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 54, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG54_RpteDetIndividual>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG55_AprobFinalExcel> AprobacionFinalLista(int nivelInterfaz, string usuario, string fechaInicio, string fechaFin, string codigo)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 55, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", fechaInicio, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", fechaFin, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", codigo, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG55_AprobFinalExcel>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG56_Periodos>> Periodos()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 56, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG56_Periodos>("dbo.uspGetRendicionGastos",
                                                                    sp_parametros,
                                                                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG57_Aprobados30>> Aprobados30Cab(string usuario, int nivelInterfaz, string fechaInicio, string fechaFin)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 57, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", fechaInicio, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", fechaFin, DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG57_Aprobados30>("dbo.uspGetRendicionGastos",
                                                                            sp_parametros,
                                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG58_Rechazados30>> Rechazados30Cab(string usuario, int nivelInterfaz, string fechaInicio, string fechaFin)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 58, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", fechaInicio, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", fechaFin, DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG58_Rechazados30>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<IEnumerable<SPG59_TiposAnulacionCab>> TiposAnulacionCabecera()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 59, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG59_TiposAnulacionCab>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG60_TiposAnulacionDet>> TiposAnulacionDetalle()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 60, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG60_TiposAnulacionDet>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG62_SolicitudCpte>> SolicitudComprobante(int idRendCompte)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 62, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", idRendCompte, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);


                return await conexion.QueryAsync<SPG62_SolicitudCpte>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG63_Sede>> Sedes()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 63, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG63_Sede>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG67_PendienteCierre>> ListaPendienteCierre(int nivelInterfaz, string usuario, string fechaInicio, string fechaFin)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 67, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", fechaInicio, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", fechaFin, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG67_PendienteCierre>("dbo.uspGetRendicionGastos",
                                                                            sp_parametros,
                                                                            commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG68_PendAprobRendGer>> PendientesAprobRendGerencia(int nivelInterfaz, string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 68, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG68_PendAprobRendGer>("dbo.uspGetRendicionGastos",
                                                sp_parametros,
                                                commandType: CommandType.StoredProcedure);
            }
        }


        public SPG69_DetalleCabecera AprobJefSolCabecera(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 69, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<SPG69_DetalleCabecera>("dbo.uspGetRendicionGastos",
                                                    sp_parametros,
                                                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<SPG70_SolicitudDetalle> AprobJefSolDetalle(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 70, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG70_SolicitudDetalle>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG71_TipoAprob> tipoAprobacionRend(string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 71, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);
                
                return conexion.Query<SPG71_TipoAprob>("dbo.uspGetRendicionGastos",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
            }
        }




        public SPG72_DetalleCabecera AprobFinalSolCabecera(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 72, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<SPG72_DetalleCabecera>("dbo.uspGetRendicionGastos",
                                                    sp_parametros,
                                                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<SPG73_SolicitudDetalle> AprobFinalSolDetalle(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 73, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG73_SolicitudDetalle>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG33_PendAprobRendGastos>> PendientesAprobRendGastosGer(int nivelInterfaz, string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 74, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG33_PendAprobRendGastos>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<IEnumerable<SPG75_CptesRegistrados>> CptesRegistrados(int idsolicitud, int secuencia)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 75, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idsolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", secuencia, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG75_CptesRegistrados>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG20_ConceptoDetalle>> ConceptosDetalleInsertar(int idConceptoCab)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 76, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", idConceptoCab, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG20_ConceptoDetalle>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<string> TienePendienteRend(string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 77, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryFirstOrDefaultAsync<string>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }




        public async Task<IEnumerable<SPG78_PendientesAprobacion>> PendientesAprobacionGeneral(int nivelAprobacion, string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 78, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelAprobacion, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return await conexion.QueryAsync<SPG78_PendientesAprobacion>("dbo.uspGetRendicionGastos",
                                                                            sp_parametros,
                                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG79_TipoAprobacion> tipoAprobacionGeneral(string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 79, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG79_TipoAprobacion>("dbo.uspGetRendicionGastos",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
            }
        }



        public SPG80_CpteCabecera ReembolsoCpteCabecera(int idRendCompte)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 80, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", idRendCompte, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<SPG80_CpteCabecera>("dbo.uspGetRendicionGastos",
                                                                            sp_parametros,
                                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG81_CpteDetalle> ReembolsoCpteDetalle(int idRendCompte)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 81, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", idRendCompte, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG81_CpteDetalle>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }



        public IEnumerable<SPG82_CptesRegistrados> DetalleComprobantes(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 82, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG82_CptesRegistrados>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }











        public SPG61_CpteSalidaCajaViewModel CpteSalidaCaja(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 61, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@ruc", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipo", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.QueryFirstOrDefault<SPG61_CpteSalidaCajaViewModel>("dbo.uspGetRendicionGastos",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
            }
        }


        // REPORTES EXCEL

        public MemoryStream AprobacionFinalExcel(List<SPG55_AprobFinalExcel> listado)
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Hoja1");

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Size = 9;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                workSheet.Cells[1, 1, 1, 24].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                workSheet.Cells[1, 1, 1, 24].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 1, 1, 24].Style.Fill.BackgroundColor.SetColor(Color.White);
                workSheet.Cells[1, 1, 1, 24].Style.Font.Color.SetColor(Color.Black);

                workSheet.Cells["A1"].Value = "Fecha";
                workSheet.Cells["B1"].Value = "Codigo";

                workSheet.Cells["C1"].Value = "Num. Serie";
                workSheet.Cells["D1"].Value = "RUC / DNI";
                workSheet.Cells["E1"].Value = "Razón Social";
                workSheet.Cells["F1"].Value = "Anexo";

                workSheet.Cells["G1"].Value = "Glosa";
                workSheet.Cells["H1"].Value = "Estado";
                workSheet.Cells["I1"].Value = "Tipo";
                workSheet.Cells["J1"].Value = "Concepto";
                workSheet.Cells["K1"].Value = "Detalle";
                workSheet.Cells["L1"].Value = "Moneda";
                workSheet.Cells["M1"].Value = "Total Solicitado";
                workSheet.Cells["N1"].Value = "Total Rendido";

                workSheet.View.FreezePanes(2, 1);

                int i = 2;

                foreach (var item in listado)
                {
                    workSheet.Row(i).Style.Font.Size = 9;

                    workSheet.Cells[i, 1].Value = item.fecha;
                    workSheet.Cells[i, 2].Value = item.codigo;

                    workSheet.Cells[i, 3].Value = item.serieNumero;
                    workSheet.Cells[i, 4].Value = item.rucdni;
                    workSheet.Cells[i, 5].Value = item.rs;
                    workSheet.Cells[i, 6].Value = item.anexo;

                    workSheet.Cells[i, 7].Value = item.observacion;
                    workSheet.Cells[i, 8].Value = item.estado;
                    workSheet.Cells[i, 9].Value = item.tipo;
                    workSheet.Cells[i, 10].Value = item.conceptoCab;
                    workSheet.Cells[i, 11].Value = item.conceptoDet;
                    workSheet.Cells[i, 12].Value = item.moneda;
                    workSheet.Cells[i, 13].Value = item.totalsolicitado;
                    workSheet.Cells[i, 14].Value = item.totalrendido;

                    workSheet.Cells[i, 13].Style.Numberformat.Format = "#,##0.00";
                    workSheet.Cells[i, 14].Style.Numberformat.Format = "#,##0.00";

                    i++;
                }

                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                package.Save();

                stream.Position = 0;
            }
            return stream;
        }






    }
}