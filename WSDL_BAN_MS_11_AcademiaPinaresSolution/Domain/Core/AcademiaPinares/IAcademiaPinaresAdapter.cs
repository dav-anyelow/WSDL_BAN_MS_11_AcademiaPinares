using EasyTemplateSolution.DistributedServices.WsdlSunitpClient.Services;
using EasyTemplateSolution.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.AcademiaPinares
{
    public interface IAcademiaPinaresAdapter
    {
        Data DoProcess(ISunitpService _iSunitpService, Data data);
    }
}
