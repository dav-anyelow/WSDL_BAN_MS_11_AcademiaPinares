using DistributedServices.Cobranzas_Bancarias_API_Davivienda_ServiceReference;
using DistributedServices.Service;
using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedServices.Client
{
    public class AcademiaPinaresClient : IAcademiaPinaresClient
    {
        public ConsultaSaldoResponse ConsultaSaldo(ISunitpService _iSunitpService, ConsultaSaldoRequest request)
        {
            _iSunitpService.AddObjLog("AcademiaPinaresClient ConsultaSaldo", "00000000000000000000", "REQUEST ENVIADO.", request);

            var response = new ConsultaSaldoResponse();
            response.Body = new ConsultaSaldoResponseBody();

            try
            {
                var client = new Cobranzas_Bancarias_API_DaviviendaSoapClient();
                var saldos = client.ConsultaSaldo(request.Body.sBank, request.Body.sPass, request.Body.sCodigo);
                response.Body.ConsultaSaldoResult = saldos;
                _iSunitpService.AddObjLog("AcademiaPinaresClient ConsultaSaldo", "00000000000000000000", "PROCESO REALIZADO CORRECTAMENTE.", response);
            }
            catch(Exception ex)
            {
                var saldos = new Saldos();
                saldos.Error = new Error();
                saldos.Error.Codigo = "999";
                saldos.Error.Mensaje = "Error al momento de consultar el servicio.";

                response.Body.ConsultaSaldoResult = saldos;

               _iSunitpService.AddLog("AcademiaPinaresClient ConsultaSaldo", "10000000000000000034", "ERROR AL MOMENTO DE EJECUTAR EL POSTEO A UN SERVICIO EXTERNO.", ex.ToString());
            }

            return response;
        }

        public PagoCuotaResponse PagoCuota(ISunitpService _iSunitpService, PagoCuotaRequest request)
        {
            _iSunitpService.AddObjLog("AcademiaPinaresClient PagoCuota", "00000000000000000000", "REQUEST ENVIADO.", request);

            var response = new PagoCuotaResponse();
            response.Body = new PagoCuotaResponseBody();

            try
            {
                var client = new Cobranzas_Bancarias_API_DaviviendaSoapClient();
                var saldos = client.PagoCuota(request.Body.sBank, request.Body.sPass, request.Body.__oPago);
                response.Body.PagoCuotaResult = saldos;
                _iSunitpService.AddObjLog("AcademiaPinaresClient PagoCuota", "00000000000000000000", "PROCESO REALIZADO CORRECTAMENTE.", response);
            }
            catch (Exception ex)
            {
                var saldos = new Saldos();
                saldos.Error = new Error();
                saldos.Error.Codigo = "999";
                saldos.Error.Mensaje = "Error al momento de consultar el servicio.";

                response.Body.PagoCuotaResult = saldos;

                _iSunitpService.AddLog("AcademiaPinaresClient PagoCuota", "10000000000000000034", "ERROR AL MOMENTO DE EJECUTAR EL POSTEO A UN SERVICIO EXTERNO.", ex.ToString());
            }

            return response;
        }


        public ReversarPagoResponse ReversarPago(ISunitpService _iSunitpService, ReversarPagoRequest request)
        {
            _iSunitpService.AddObjLog("AcademiaPinaresClient ReversarPago", "00000000000000000000", "REQUEST ENVIADO.", request);

            var response = new ReversarPagoResponse();
            response.Body = new ReversarPagoResponseBody();

            try
            {
                var client = new Cobranzas_Bancarias_API_DaviviendaSoapClient();
                var saldos = client.ReversarPago(request.Body.sBank, request.Body.sPass, request.Body.__oPago);
                response.Body.ReversarPagoResult = saldos;
                _iSunitpService.AddObjLog("AcademiaPinaresClient ReversarPago", "00000000000000000000", "PROCESO REALIZADO CORRECTAMENTE.", response);
            }
            catch (Exception ex)
            {
                var saldos = new Saldos();
                saldos.Error = new Error();
                saldos.Error.Codigo = "999";
                saldos.Error.Mensaje = "Error al momento de consultar el servicio.";

                response.Body.ReversarPagoResult = saldos;

                _iSunitpService.AddLog("AcademiaPinaresClient ReversarPago", "10000000000000000034", "ERROR AL MOMENTO DE EJECUTAR EL POSTEO A UN SERVICIO EXTERNO.", ex.ToString());
            }

            return response;
        }

    }
}
