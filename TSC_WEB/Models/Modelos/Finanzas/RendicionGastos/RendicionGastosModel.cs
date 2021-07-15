using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

                return conexion.QuerySingle<StatusResponse>("dbo.uspSetRendicionAprobacion",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public StatusResponse ValidacionRendGastos(SPS_ParametroValid parametro)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", parametro.opcion, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", parametro.idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@codigo", parametro.codigo, DbType.String, ParameterDirection.Input);

                return conexion.QuerySingle<StatusResponse>("dbo.uspSetRendicionValidacion",
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

                return await conexion.QueryAsync<SPG4_PermisoXCeCo>("dbo.uspGetRendicionGastos",
                                                                    sp_parametros,
                                                                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG1_ConceptoCab>> ConceptosCab(int idSolicitud)
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

                return await conexion.QueryAsync<SPG1_ConceptoCab>("dbo.uspGetRendicionGastos",
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

                return await conexion.QueryAsync<G01_TipoMoneda>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<SPG6_SolicitudCab> SolicitudCab(int idSolicitud)
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

                return await conexion.QueryFirstOrDefaultAsync<SPG6_SolicitudCab>("dbo.uspGetRendicionGastos",
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

                return await conexion.QueryAsync<SPG9_ListaHistorial>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<SPG10_SolicitudXCodigo> SolicitudXCodigo(string codigo)
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
                sp_parametros.Add("@filtro", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", 0, DbType.Int32, ParameterDirection.Input);

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

                return await conexion.QueryAsync<SPG15_ListaPendientes30>("dbo.uspGetRendicionGastos",
                                                                        sp_parametros,
                                                                        commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<SPG16_SolicitudCab> PendAprob30Cab(int idSolicitud)
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

                return await conexion.QueryFirstOrDefaultAsync<SPG16_SolicitudCab>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG17_SolicitudDet> PendAprob30Det(int idSolicitud)
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

                return conexion.Query<SPG17_SolicitudDet>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG18_Historial30>> Historial30(string fechaInicio, string fechaFin, string usuario)
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

                return await conexion.QueryAsync<SPG18_Historial30>("dbo.uspGetRendicionGastos",
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

                return await conexion.QueryAsync<SPG19_Tipos>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG20_ConceptoDetalle>> ConceptosDetalleInsertar(int idConceptoCab)
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



        public async Task<IEnumerable<SPG31_PendientesRendicion>> PendientesRendicion()
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 31, DbType.Int32, ParameterDirection.Input);
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

                return await conexion.QueryAsync<SPG31_PendientesRendicion>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public IEnumerable<SPG32_DetalleSolicitud> SolicitudDetalleRendicion(string codigo)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 32, DbType.Int32, ParameterDirection.Input);
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

                return conexion.Query<SPG32_DetalleSolicitud>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public StatusResponse SetRendicionComprobanteCab(SPS_ComprobanteParametro parametro)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", parametro.opcion, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", parametro.idRendCompte, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", parametro.idTipoComp, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@serieNumero", parametro.serieNumero, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@rucDNI", parametro.rucDNI, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@rs", parametro.rs, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigoPC", parametro.codigoPC, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@obs", parametro.obs, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaEmision", parametro.fechaEmision.Date, DbType.Date, ParameterDirection.Input);
                sp_parametros.Add("@periodo", parametro.periodo, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ceco", parametro.ceco, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idTipoMod", parametro.idTipoMod, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idSolicitud", parametro.idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", parametro.secuencia, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@idRendCompteDet", parametro.idRendCompteDet, DbType.Int32, ParameterDirection.Input);
                
                sp_parametros.Add("@fecha", parametro.fecha, DbType.Date, ParameterDirection.Input);
                sp_parametros.Add("@detalle1", parametro.detalle1, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@detalle2", parametro.detalle2, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codum", parametro.codum, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@valorunit", parametro.valorunit, DbType.Decimal, ParameterDirection.Input);
                sp_parametros.Add("@cantidad", parametro.cantidad, DbType.Int32, ParameterDirection.Input);

                return conexion.QuerySingleOrDefault<StatusResponse>("dbo.uspSetRendicionComprobante",
                                                                    sp_parametros,
                                                                    commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<SPV2_EstadoSolicitud> EstadoSolicitudRendGast(int idSolicitud)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 2, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);

                return await conexion.QueryFirstOrDefaultAsync<SPV2_EstadoSolicitud>("dbo.uspSetRendicionValidacion",
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

                return conexion.Query<SPG35_PendApbRdGstsDet>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG36_RndGstsAprobGstsFnzas>> ListaRendicionFinanzas(int nivelInterfaz, string usuario, string fechaInicio, string fechaFin, string codigo)
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

                return await conexion.QueryAsync<SPG39_Tipos>("dbo.uspGetRendicionGastos",
                                                    sp_parametros,
                                                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<SPG40_Colaborador>> ColaboradorSofya(string filtro)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 40, DbType.Int32, ParameterDirection.Input);
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

                sp_parametros.Add("@filtro", filtro.ToUpper(), DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte",0, DbType.Int32, ParameterDirection.Input);

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

                return await conexion.QueryAsync<SPG45_SolicitudDetalle>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }


    }
}