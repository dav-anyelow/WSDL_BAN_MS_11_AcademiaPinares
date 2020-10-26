using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Services;
using EasyTemplateSolution.Domain.Dto;
using DistributedServices.Service;
using DistributedServices.Client;
using DistributedServices.Cobranzas_Bancarias_API_Davivienda_ServiceReference;
using Domain.Core.Helpers;

namespace Domain.Core.AcademiaPinares.Adapters
{
    public class PagarCuotaAdapter : IAcademiaPinaresAdapter
    {
        IAcademiaPinaresClient _iAcademiaPinaresClient;
        private bool _requirided = false;
        private string _requiredField = "";
        private PagoCuotaRequest _request;

        public PagarCuotaAdapter()
        {
            _iAcademiaPinaresClient = new AcademiaPinaresClient();
            _request = new PagoCuotaRequest();
            _request.Body = new PagoCuotaRequestBody();
        }

        public Data DoProcess(ISunitpService _iSunitpService, Data data)
        {
            MapRequest(_iSunitpService, data);
            var response = new PagoCuotaResponse();
            if (_requirided)
            {
                response = setRequiredField();
            }
            else
            {
                response = PagoCuota(_iSunitpService, _request);
            }

            return MapResponse(_iSunitpService, response);
        }

        private PagoCuotaResponse PagoCuota(ISunitpService _iSunitpService, PagoCuotaRequest request)
        {
            _iAcademiaPinaresClient = new AcademiaPinaresClient();
            var response = _iAcademiaPinaresClient.PagoCuota(_iSunitpService, request);

            return response;
        }

        private PagoCuotaResponse setRequiredField()
        {
            var response = new PagoCuotaResponse();
            response.Body = new PagoCuotaResponseBody();

            var saldos = new Saldos();
            saldos.Error = new Error();
            saldos.Error.Codigo = "998";
            saldos.Error.Mensaje = "EL CAMPO (" + _requiredField + ") ES REQUERIDO"; ;
            response.Body.PagoCuotaResult = saldos;
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

        private Data MapResponse(ISunitpService _iSunitpService, PagoCuotaResponse responsePin)
        {
            if (responsePin.Body.PagoCuotaResult.Error.Codigo == null)
            {
                responsePin.Body.PagoCuotaResult.Error.Codigo = "00";
                responsePin.Body.PagoCuotaResult.Error.Mensaje = "CODIGO_PROCESO_EXITOSO";
            }
            _iSunitpService.SetCoreErrors(responsePin.Body.PagoCuotaResult.Error.Codigo, responsePin.Body.PagoCuotaResult.Error.Mensaje);

            var response = new Data();
            response.Field = "ACADEMIA PINARES RESPONSE";
            response.HasData = true;
            response.DataList = new List<Data>();

            var Error = new Data();
            Error.Field = "Error";
            Error.HasData = true;
            Error.DataList = new List<Data>();

            var Codigo = new Data();
            Codigo.Field = "Codigo";
            Codigo.HasData = true;
            Codigo.Value = responsePin.Body.PagoCuotaResult.Error.Codigo;
            Error.DataList.Add(Codigo);

            var Mensaje = new Data();
            Mensaje.Field = "Mensaje";
            Mensaje.HasData = true;
            Mensaje.Value = responsePin.Body.PagoCuotaResult.Error.Mensaje;
            Error.DataList.Add(Mensaje);

            response.DataList.Add(Error);
            

            return response;
        }

    }
}
