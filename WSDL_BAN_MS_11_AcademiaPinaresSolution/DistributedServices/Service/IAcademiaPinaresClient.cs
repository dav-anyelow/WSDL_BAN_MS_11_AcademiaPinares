using DistributedServices.Cobranzas_Bancarias_API_Davivienda_ServiceReference;
using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedServices.Service
{
    public interface IAcademiaPinaresClient
    {
        ConsultaSaldoResponse ConsultaSaldo(ISunitpService _iSunitpService, ConsultaSaldoRequest request);

        PagoCuotaResponse PagoCuota(ISunitpService _iSunitpService, PagoCuotaRequest request);


        ReversarPagoResponse ReversarPago(ISunitpService _iSunitpService, ReversarPagoRequest request);
    }
}
