﻿using Domain.Core.AcademiaPinares;
using Domain.Core.AcademiaPinares.Adapters;
using Domain.Entities;
using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Services;
using EasyTemplateSolution.Domain.ARepositories;
using EasyTemplateSolution.Domain.Dto;
using EasyTemplateSolution.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ReversarPago
{
    public class ReversarPagoRepository : AEasyDataProcessRepository
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
            IAcademiaPinaresAdapter iAcademiaPinaresAdapter = new ReversarCuotaAdapter();
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
            foreach (var detail in response.DataList)
            {
                if (detail.Field.Equals("Guid"))
                {
                    guid = detail.Value;
                }
                if (detail.Field.Equals("Error"))
                {
                    foreach (var item in detail.DataList)
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
            if (!edm.IsSuccessful())
            {
                _iSunitpService.AddSingleLog(pws20PinClResponse.GetValue("_defaultError"));
            }

        }
    }
}
