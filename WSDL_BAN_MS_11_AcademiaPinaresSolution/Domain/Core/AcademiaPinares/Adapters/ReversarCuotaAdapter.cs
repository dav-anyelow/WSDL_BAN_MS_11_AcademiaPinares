using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Services;
using EasyTemplateSolution.Domain.Dto;
using DistributedServices.Service;
using DistributedServices.Cobranzas_Bancarias_API_Davivienda_ServiceReference;
using DistributedServices.Client;
using Domain.Core.Helpers;

namespace Domain.Core.AcademiaPinares.Adapters
{
    public class ReversarCuotaAdapter : IAcademiaPinaresAdapter
    {
        IAcademiaPinaresClient _iAcademiaPinaresClient;
        private bool _requirided = false;
        private string _requiredField = "";
        private string _guid = "";
        private ReversarPagoRequest _request;

        public ReversarCuotaAdapter()
        {
            _iAcademiaPinaresClient = new AcademiaPinaresClient();
            _request = new ReversarPagoRequest();
            _request.Body = new ReversarPagoRequestBody();
        }

        public Data DoProcess(ISunitpService _iSunitpService, Data data)
        {
            MapRequest(_iSunitpService, data);
            var response = new ReversarPagoResponse();
            if (_requirided)
            {
                response = setRequiredField();
            }
            else
            {
                response = ReversarPago(_iSunitpService, _request);
            }

            return MapResponse(_iSunitpService, response);
        }

        private ReversarPagoResponse ReversarPago(ISunitpService _iSunitpService, ReversarPagoRequest request)
        {
            _iAcademiaPinaresClient = new AcademiaPinaresClient();
            var response = _iAcademiaPinaresClient.ReversarPago(_iSunitpService, request);

            return response;
        }

        private ReversarPagoResponse setRequiredField()
        {
            var response = new ReversarPagoResponse();
            response.Body = new ReversarPagoResponseBody();

            var saldos = new Saldos();
            saldos.Error = new Error();
            saldos.Error.Codigo = "998";
            saldos.Error.Mensaje = "EL CAMPO (" + _requiredField + ") ES REQUERIDO"; ;
            response.Body.ReversarPagoResult = saldos;
            return response;
        }

        private void MapRequest(ISunitpService _iSunitpService, Data data)
        {
            var oPago = new __oPago();

            var isRequired = false;
            var idUsuario = "";
            var codigoFamilia = "";
            foreach (var item in data.DataList)
            {
                if (item.Field.Equals("Guid"))
                {
                    isRequired = true;
                    _guid = item.Value;
                }
                if (item.Field.Equals("Fecha"))
                {
                    isRequired = true;
                }
                if (item.Field.Equals("Hora"))
                {
                    isRequired = true;
                }
                if (item.Field.Equals("Cajero"))
                {
                    isRequired = true;
                }
                if (item.Field.Equals("IdBanco"))
                {
                    isRequired = true;
                }
                if (item.Field.Equals("IdUsuario"))
                {
                    isRequired = true;
                    idUsuario = item.Value;
                }
                if (item.Field.Equals("Sucursal"))
                {
                    isRequired = true;
                }
                if (item.Field.Equals("Terminal"))
                {
                    isRequired = true;
                }
                if (item.Field.Equals("Agencia"))
                {
                    isRequired = true;
                }
                if (item.Field.Equals("MetodoEjecutado"))
                {
                    isRequired = true;
                }
                if (item.Field.Equals("Sbank"))
                {
                    isRequired = true;
                    _request.Body.sBank = item.Value;
                }
                if (item.Field.Equals("Spass"))
                {
                    isRequired = true;
                    _request.Body.sPass = item.Value;
                }
                if (item.Field.Equals("Recibo"))
                {
                    isRequired = true;
                    oPago.Recibo = item.Value;
                }
                if (item.Field.Equals("CajeroPinares"))
                {
                    isRequired = true;
                    oPago.Cajero = item.Value;
                }
                if (item.Field.Equals("CodigoFamilia"))
                {
                    isRequired = true;
                    oPago.CodigoFamilia = item.Value;
                    codigoFamilia = item.Value;
                }
                if (item.Field.Equals("FechaPago"))
                {
                    isRequired = true;
                    oPago.FechaPago = item.Value;
                }
                if (item.Field.Equals("Valor"))
                {
                    isRequired = true;
                    oPago.Valor = item.Value;
                }
                if (item.Field.Equals("Periodo"))
                {
                    isRequired = true;
                    oPago.Periodo = item.Value;
                }
                if (item.Field.Equals("NumFactura"))
                {
                    isRequired = true;
                    oPago.NumFactura = item.Value;
                }
                if (item.Field.Equals("Cuota"))
                {
                    isRequired = true;
                    oPago.Cuota = item.Value;
                }
                if (item.Field.Equals("CodigoTransaccion"))
                {
                    isRequired = true;
                    oPago.CodigoTransaccion = item.Value;
                }

                if (isRequired)
                {
                    if (item.Value.Equals("") || item.Value == null)
                    {
                        _requirided = true;
                        _requiredField = item.Field;
                    }
                }
                isRequired = false;
            }
            var oPagoHelper = new OPagoHelper(oPago);
            _request.Body.__oPago = oPagoHelper.GetOPago();

            _iSunitpService.SetCoreReferences(idUsuario + "-" + codigoFamilia);

        }

        private Data MapResponse(ISunitpService _iSunitpService, ReversarPagoResponse responsePin)
        {
            if (responsePin.Body.ReversarPagoResult.Error.Codigo == null)
            {
                responsePin.Body.ReversarPagoResult.Error.Codigo = "00";
                responsePin.Body.ReversarPagoResult.Error.Mensaje = "CODIGO_PROCESO_EXITOSO";
            }
            _iSunitpService.SetCoreErrors(responsePin.Body.ReversarPagoResult.Error.Codigo, responsePin.Body.ReversarPagoResult.Error.Mensaje);

            var response = new Data();
            response.Field = "ACADEMIA PINARES RESPONSE";
            response.HasData = true;
            response.DataList = new List<Data>();

            var Guid = new Data();
            Guid.Field = "Guid";
            Guid.HasData = true;
            Guid.Value = _guid;
            response.DataList.Add(Guid);

            var Error = new Data();
            Error.Field = "Error";
            Error.HasData = true;
            Error.DataList = new List<Data>();

            var Codigo = new Data();
            Codigo.Field = "Codigo";
            Codigo.HasData = true;
            Codigo.Value = responsePin.Body.ReversarPagoResult.Error.Codigo;
            Error.DataList.Add(Codigo);

            var Mensaje = new Data();
            Mensaje.Field = "Mensaje";
            Mensaje.HasData = true;
            Mensaje.Value = responsePin.Body.ReversarPagoResult.Error.Mensaje;
            Error.DataList.Add(Mensaje);

            response.DataList.Add(Error);


            return response;
        }
    }
}
