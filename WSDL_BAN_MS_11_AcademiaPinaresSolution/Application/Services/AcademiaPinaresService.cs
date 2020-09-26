using EasyTemplateSolution.Application.Service;
using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Concrete;
using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Services;
using EasyTemplateSolution.Domain.Dto;
using Infrastructure.Repositories.TestConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AcademiaPinaresService
    {
        private ISunitpService _iSunitpService;
        public AcademiaPinaresService()
        {
            _iSunitpService = new SunitpService();
            _iSunitpService.SetInt("LAYER_3", "BANCO_MS_8", "PROCESOS DIGITALES VIVI", "MICROSERVICIO DE PROCESOS DIGITALES VIVI");
            _iSunitpService.AddSingleLog("INICIO DEL PROCESAMIENTO");
        }

        public DataTransferObject DoProcess(DataTransferObject dataTransferObject)
        {
            var dataTransferObjectResponse = new DataTransferObject();
            dataTransferObjectResponse.Bank = "HN";
            dataTransferObjectResponse.ExecuteMethod = dataTransferObject.ExecuteMethod;
            dataTransferObjectResponse.Error = "00000000000000000000";
            dataTransferObjectResponse.ExternalError = "PROCESO REALIZADO CORRECTAMENTE";

            var edpf = new EasyDataProcessFactory();
            edpf.RegisterRepository(_iSunitpService, "BAN_MS_8_M_0_TEST_CONNECTION", new TestConnectionRepository());

            dataTransferObjectResponse = edpf.DoProcess(_iSunitpService, dataTransferObject);

            _iSunitpService.AddObjLog("ProcesosDigitalesViviService DoProcess", dataTransferObjectResponse.Data.Error, dataTransferObjectResponse.Data.ExternalError, dataTransferObjectResponse);
            _iSunitpService.AddSingleLog("FIN DEL PROCESAMIENTO");
            _iSunitpService.SaveLog();

            return dataTransferObjectResponse;
        }
    }
}
