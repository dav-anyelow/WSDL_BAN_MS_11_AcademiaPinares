using EasyTemplateSolution.Application.Service;
using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Concrete;
using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Services;
using EasyTemplateSolution.Domain.Dto;
using Infrastructure.Repositories.ConsultarSaldo;
using Infrastructure.Repositories.PagarCuota;
using Infrastructure.Repositories.ReversarPago;
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
            _iSunitpService.SetInt("LAYER_3", "BANCO_MS_11", "ACADEMIA PINARES", "MICROSERVICIO DE ACADEMIA PINARES");
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
            edpf.RegisterRepository(_iSunitpService, "BAN_MS_11_M_0_TEST_CONNECTION", new TestConnectionRepository());
            edpf.RegisterRepository(_iSunitpService, "BAN_MS_11_M_1_ACADEMIA_PINARES_CONSULTAR_SALDO", new ConsultarSaldoRepository());
            edpf.RegisterRepository(_iSunitpService, "BAN_MS_11_M_2_ACADEMIA_PINARES_PAGAR_CUOTA", new PagarCuotaRepository());
            edpf.RegisterRepository(_iSunitpService, "BAN_MS_11_M_1_ACADEMIA_PINARES_REVERSAR_PAGO", new ReversarPagoRepository());

            dataTransferObjectResponse = edpf.DoProcess(_iSunitpService, dataTransferObject);

            _iSunitpService.AddObjLog("AcademiaPinaresService DoProcess", dataTransferObjectResponse.Data.Error, dataTransferObjectResponse.Data.ExternalError, dataTransferObjectResponse);
            _iSunitpService.AddSingleLog("FIN DEL PROCESAMIENTO");
            _iSunitpService.SaveLog();

            return dataTransferObjectResponse;
        }
    }
}
