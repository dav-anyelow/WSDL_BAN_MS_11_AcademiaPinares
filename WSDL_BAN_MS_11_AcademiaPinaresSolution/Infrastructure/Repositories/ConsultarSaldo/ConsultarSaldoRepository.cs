using Domain.Core.AcademiaPinares;
using Domain.Core.AcademiaPinares.Adapters;
using Domain.Entities;
using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Services;
using EasyTemplateSolution.Domain.ARepositories;
using EasyTemplateSolution.Domain.Dto;
using EasyTemplateSolution.Infrastructure.DataModels;
using EasyTemplateSolution.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ConsultarSaldo
{
    public class ConsultarSaldoRepository : AEasyDataProcessRepository
    {
        public override bool NeedDataBaseConnection
        {
            get
            {
                return true;
            }
        }

        public override Data DoProcess(ISunitpService _iSunitpService, Data data)
        {
            var dataResponse = new Data();
            dataResponse.Error = "00000000000000000000";
            dataResponse.HasData = true;
            dataResponse.Field = "RESPONSE";
            dataResponse.ExternalError = "PROCESO REALIZADO CORRECTAMENTE";
            dataResponse.InternalError = "";
            dataResponse.DataList = new List<Data>();

            if (!data.Field.Equals("REQUEST_DETAIL"))
            {
                dataResponse.Error = "10000000000000000007";
                dataResponse.ExternalError = "NO SE HAN ENCONTRADO LOS PARAMETROS NECESARIOS PARA EL REQUEST.";
                return dataResponse;
            }
            
            //Academia Pinares
            IAcademiaPinaresAdapter iAcademiaPinaresAdapter = new ConsultarSaldoAdapter();
            var response = iAcademiaPinaresAdapter.DoProcess(_iSunitpService, data);
            dataResponse.DataList.Add(response);

            //Update Data Info to Core
            SaveDataHeaderResponse(_iSunitpService, response);

            return dataResponse;
        }

        private void SaveDataHeaderResponse(ISunitpService _iSunitpService, Data response)
        {
            var guid = "";
            var codigo = "";
            var mensaje = "";
            var nucleo = "";
            var pinaresDetail = new Data();
            foreach(var detail in response.DataList)
            {
                if (detail.Field.Equals("Guid"))
                {
                    guid = detail.Value;
                }
                if (detail.Field.Equals("Error"))
                {
                    foreach(var item in detail.DataList)
                    {
                        if (item.Field.Equals("Codigo"))
                        {
                            codigo = item.Value;
                        }
                        if (item.Field.Equals("Mensaje"))
                        {
                            mensaje = item.Value;
                        }
                    }
                }
                if (detail.Field.Equals("Familia"))
                {
                    foreach (var item in detail.DataList)
                    {
                        if (item.Field.Equals("Nucleo"))
                        {
                            nucleo = item.Value;
                        }
                    }
                }
                if (detail.Field.Equals("Saldos"))
                {
                    pinaresDetail = detail;
                }
            }

            var pws20PinCl = new PWS20PINCL();
            pws20PinCl.SetValue("Guid", guid);
            pws20PinCl.SetValue("Nucleo", nucleo);
            pws20PinCl.SetValue("Codigo", codigo);
            pws20PinCl.SetValue("Mensaje", mensaje); 

            _iSunitpService.AddObjLog("ConsultarSaldoRepository SaveDataHeaderResponse", "00000000000000000000", "OBJETO ENVIADO", pws20PinCl.GetObject());
            //CallModel
            var edm = new EasyDataModels();
            edm.EasyCallInit(_oledbConnection, pws20PinCl);
            var pws20PinClResponse = edm.CallProcedure();
            _iSunitpService.AddObjLog("ConsultarSaldoRepository SaveDataHeaderResponse", "00000000000000000000", "OBJETO RECIBIDO", pws20PinClResponse.GetObject());

            //ValidateResponse
            if (edm.IsSuccessful())
            {
                if (codigo.Equals("00"))
                {
                    SaveDataDetailResponse(_iSunitpService, pinaresDetail, guid);
                }
            }
            else
            {
                _iSunitpService.AddSingleLog(pws20PinClResponse.GetValue("_defaultError"));
            }
            
        }

        private void SaveDataDetailResponse(ISunitpService _iSunitpService, Data pinaresDetail, string Guid)
        {
            
            foreach (var detail in pinaresDetail.DataList)
            {
                var pws21PinCl = new PWS21PINCL();
                pws21PinCl.SetValue("Guid", Guid);

                if (detail.Field.Equals("Saldo"))
                {
                    foreach(var item in detail.DataList)
                    {
                        if (item.Field.Equals("Tipo"))
                        {
                            pws21PinCl.SetValue("Tipo", item.Value);
                        }
                        if (item.Field.Equals("Concepto"))
                        {
                            pws21PinCl.SetValue("Concepto", item.Value);
                        }
                        if (item.Field.Equals("NumFactura"))
                        {
                            pws21PinCl.SetValue("NumFactura", item.Value);
                        }
                        if (item.Field.Equals("Cuota"))
                        {
                            pws21PinCl.SetValue("Cuota", item.Value);
                        }
                        if (item.Field.Equals("Valor"))
                        {
                            pws21PinCl.SetValue("Valor", item.Value);
                        }
                        if (item.Field.Equals("Moneda"))
                        {
                            pws21PinCl.SetValue("Moneda", item.Value);
                        }
                        if (item.Field.Equals("Vence"))
                        {
                            pws21PinCl.SetValue("Vence", item.Value);
                        }
                        if (item.Field.Equals("Periodo"))
                        {
                            pws21PinCl.SetValue("Periodo", item.Value);
                        }
                    }
                }

                _iSunitpService.AddObjLog("ConsultarSaldoRepository SaveDataDetailResponse", "00000000000000000000", "OBJETO ENVIADO", pws21PinCl.GetObject());
                //CallModel
                var edm = new EasyDataModels();
                edm.EasyCallInit(_oledbConnection, pws21PinCl);
                var pws21PinClResponse = edm.CallProcedure();
                _iSunitpService.AddObjLog("ConsultarSaldoRepository SaveDataDetailResponse", "00000000000000000000", "OBJETO RECIBIDO", pws21PinClResponse.GetObject());

                //ValidateResponse
                if (!edm.IsSuccessful())
                {
                    _iSunitpService.AddSingleLog(pws21PinClResponse.GetValue("_defaultError"));
                }

            }
        }
    }
}
