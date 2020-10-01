using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Services;
using EasyTemplateSolution.Domain.ARepositories;
using EasyTemplateSolution.Domain.Dto;
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

            var idServicio = new Data()
            {
                HasData = true,
                Field = "idServicio",
                Value = "0"
            };
            dataResponse.DataList.Add(idServicio);

            var status = new Data()
            {
                HasData = true,
                Field = "status",
                Value = "0"
            };
            dataResponse.DataList.Add(status);

            return dataResponse;
        }
    }
}
