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
                sp_parametros.Add("@valorEntrega", parametro.valorEntrega, DbType.Int32, ParameterDirection.Input);

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

                return conexion.QuerySingle<StatusResponse>("dbo.uspSetRendicionGastos",
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

                return await conexion.QueryAsync<SPG7_SolicitudDet>("dbo.uspGetRendicionGastos",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<SPG8_SolicitudDetalle> IdSolicitudDetalle(int idSolicitud, int idConceptoCab)
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
                sp_parametros.Add("@idConceptoCab", idConceptoCab, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

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

                return await conexion.QueryFirstOrDefaultAsync<SPG10_SolicitudXCodigo>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<SPG11_ListaPendientes>> PendientesAprobacion20(string fechaInicio, string fechaFin, int nivelInterfaz, string usuario)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 11, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", fechaInicio, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", fechaFin, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

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

                return await conexion.QueryAsync<SPG14_Historial20>("dbo.uspGetRendicionGastos",
                                                                    sp_parametros,
                                                                    commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<IEnumerable<SPG15_ListaPendientes30>> PendientesAprobacion30(string fechaInicio, string fechaFin, int nivelInterfaz, string usuario, int idEstado, string codigo)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 15, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", nivelInterfaz, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@fechaInicio", fechaInicio, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", fechaFin, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@codigo", codigo, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", idEstado, DbType.Int32, ParameterDirection.Input);

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

                return await conexion.QueryAsync<SPG22_TipoComprobante>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<SPG23_BusquedaXCod> SolBusquedaXCod(string codigo)
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

                return await conexion.QueryFirstOrDefaultAsync<SPG23_BusquedaXCod>("dbo.uspGetRendicionGastos",
                                                                sp_parametros,
                                                                commandType: CommandType.StoredProcedure);
            }
        }



        public List<SPG25_ConceptoDetalle> ConceptosDetalleVisualizar(int idConceptoCab)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", 25, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@nivelInterfaz", 0, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@usuario", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", idConceptoCab, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", 0, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@fechaInicio", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@fechaFin", "", DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@codigo", "", DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idConceptoCab", idConceptoCab, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idEstado", 0, DbType.Int32, ParameterDirection.Input);

                return conexion.Query<SPG25_ConceptoDetalle>("dbo.uspGetRendicionGastos",
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

                return conexion.Query<SPG26_SolDetConceptos>("dbo.uspGetRendicionGastos",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure).ToList();
            }
        }


    }
}