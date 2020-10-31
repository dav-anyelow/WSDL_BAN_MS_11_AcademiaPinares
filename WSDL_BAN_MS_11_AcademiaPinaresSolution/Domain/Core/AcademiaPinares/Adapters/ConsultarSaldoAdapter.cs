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
    public class ConsultarSaldoAdapter : IAcademiaPinaresAdapter
    {
        IAcademiaPinaresClient _iAcademiaPinaresClient;
        private bool _requirided = false;
        private string _requiredField = "";
        private string _Sbank = "";
        private string _Spass = "";
        private string _Scodigo = "";
        private string _guid = "";
        public ConsultarSaldoAdapter()
        {
            _iAcademiaPinaresClient = new AcademiaPinaresClient();
        }

        public Data DoProcess(ISunitpService _iSunitpService, Data data)
        {
            MapRequest(_iSunitpService, data);
            var response  = new ConsultaSaldoResponse();
            if (_requirided)
            {
                response = setRequiredField();
            }
            else
            {
                response = ConsultarSaldo(_iSunitpService);
            }

            return MapResponse(_iSunitpService, response);
        }

        private ConsultaSaldoResponse ConsultarSaldo(ISunitpService _iSunitpService)
        {
            var request = new ConsultaSaldoRequest();
            request.Body = new ConsultaSaldoRequestBody();
            request.Body.sBank = _Sbank;
            request.Body.sPass = _Spass;
            request.Body.sCodigo = _Scodigo;

            _iAcademiaPinaresClient = new AcademiaPinaresClient();
            var response = _iAcademiaPinaresClient.ConsultaSaldo(_iSunitpService, request);

            return response;
        }

        private ConsultaSaldoResponse setRequiredField()
        {
            var response = new ConsultaSaldoResponse();
            response.Body = new ConsultaSaldoResponseBody();

            var saldos = new Saldos();
            saldos.Error = new Error();
            saldos.Error.Codigo = "998";
            saldos.Error.Mensaje = "EL CAMPO (" + _requiredField + ") ES REQUERIDO"; ;
            response.Body.ConsultaSaldoResult = saldos;
            return response;
        }

        private void MapRequest(ISunitpService _iSunitpService, Data data)
       {
            var isRequired = false;
            var idUsuario = "";
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
                    _Sbank = item.Value;
                }
                if (item.Field.Equals("Spass"))
                {
                    isRequired = true;
                    _Spass = item.Value;
                }
                if (item.Field.Equals("Scodigo"))
                {
                    isRequired = true;
                    _Scodigo = item.Value;
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

            _iSunitpService.SetCoreReferences(idUsuario + "-" + _Scodigo);

        }

        private Data MapResponse(ISunitpService _iSunitpService, ConsultaSaldoResponse responsePin)
        {
            if(responsePin.Body.ConsultaSaldoResult.Error.Codigo == null)
            {
                responsePin.Body.ConsultaSaldoResult.Error.Codigo = "00";
                responsePin.Body.ConsultaSaldoResult.Error.Mensaje = "CODIGO_PROCESO_EXITOSO";
            }
            _iSunitpService.SetCoreErrors(responsePin.Body.ConsultaSaldoResult.Error.Codigo, responsePin.Body.ConsultaSaldoResult.Error.Mensaje);

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
            Codigo.Value = responsePin.Body.ConsultaSaldoResult.Error.Codigo;
            Error.DataList.Add(Codigo);

            var Mensaje = new Data();
            Mensaje.Field = "Mensaje";
            Mensaje.HasData = true;
            Mensaje.Value = responsePin.Body.ConsultaSaldoResult.Error.Mensaje;
            Error.DataList.Add(Mensaje);

            response.DataList.Add(Error);

            if (responsePin.Body.ConsultaSaldoResult.Error.Codigo.Equals("00"))
            {
               
                var Familia = new Data();
                Familia.Field = "Familia";
                Familia.HasData = true;

                Familia.DataList = new List<Data>();

                var Nucleo = new Data();
                Nucleo.Field = "Nucleo";
                Nucleo.HasData = true;
                Nucleo.Value = responsePin.Body.ConsultaSaldoResult.Familia.Nucleo;
                Familia.DataList.Add(Nucleo);

                response.DataList.Add(Familia);

                var Saldos = new Data();
                Saldos.Field = "Saldos";
                Saldos.HasData = true;
                Saldos.DataList = new List<Data>();

                foreach (var saldoMember in responsePin.Body.ConsultaSaldoResult.SaldosMember)
                {
                    var Saldo = new Data();
                    Saldo.Field = "Saldo";
                    Saldo.HasData = true;
                    Saldo.DataList = new List<Data>();

                    var Tipo = new Data();
                    Tipo.Field = "Tipo";
                    Tipo.HasData = true;
                    Tipo.Value = saldoMember.Tipo;
                    Saldo.DataList.Add(Tipo);

                    var Concepto = new Data();
                    Concepto.Field = "Concepto";
                    Concepto.HasData = true;
                    Concepto.Value = saldoMember.Concepto;
                    Saldo.DataList.Add(Concepto);

                    var NumFactura = new Data();
                    NumFactura.Field = "NumFactura";
                    NumFactura.HasData = true;
                    NumFactura.Value = saldoMember.NumFactura;
                    Saldo.DataList.Add(NumFactura);

                    var Cuota = new Data();
                    Cuota.Field = "Cuota";
                    Cuota.HasData = true;
                    Cuota.Value = saldoMember.Cuota;
                    Saldo.DataList.Add(Cuota);

                    var Valor = new Data();
                    Valor.Field = "Valor";
                    Valor.HasData = true;
                    Valor.Value = saldoMember.Valor;
                    Saldo.DataList.Add(Valor);

                    var Moneda = new Data();
                    Moneda.Field = "Moneda";
                    Moneda.HasData = true;
                    Moneda.Value = saldoMember.Moneda;
                    Saldo.DataList.Add(Moneda);

                    var Vence = new Data();
                    Vence.Field = "Vence";
                    Vence.HasData = true;
                    Vence.Value = saldoMember.Vence;
                    Saldo.DataList.Add(Vence);

                    var Periodo = new Data();
                    Periodo.Field = "Periodo";
                    Periodo.HasData = true;
                    Periodo.Value = saldoMember.Periodo;
                    Saldo.DataList.Add(Periodo);

                    Saldos.DataList.Add(Saldo);
                }

                response.DataList.Add(Saldos);
            }
            
            return response;
        }


    }
}
