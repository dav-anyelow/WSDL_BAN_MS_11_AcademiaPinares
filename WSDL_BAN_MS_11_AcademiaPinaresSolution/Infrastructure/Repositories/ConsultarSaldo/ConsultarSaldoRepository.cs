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
                return false;
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
            
            return dataResponse;
        }

        private void SaveDataHeaderResponse(ISunitpService _iSunitpService, Data response)
        {
            var pws20PinCl = new PWS20PINCL();

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

        private void SaveDataDetailResponse(ISunitpService _iSunitpService, Data response)
        {
            var pws21PinCl = new PWS21PINCL();

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
