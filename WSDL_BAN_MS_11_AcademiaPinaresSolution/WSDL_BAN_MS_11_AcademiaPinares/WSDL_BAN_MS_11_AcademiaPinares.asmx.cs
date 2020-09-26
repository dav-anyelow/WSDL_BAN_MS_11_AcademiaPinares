using Application.Services;
using EasyTemplateSolution.Presentation.Decrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WSDL_BAN_MS_11_AcademiaPinares
{
    /// <summary>
    /// Descripción breve de WSDL_BAN_MS_11_AcademiaPinares
    /// </summary>
    [WebService(Namespace = "http://WSDL_BAN_MS_11_AcademiaPinares/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSDL_BAN_MS_11_AcademiaPinares : System.Web.Services.WebService
    {

        [WebMethod]
        public string DoProcess(string DataTransferObject)
        {
            var dda = new DataDecryptAdapter();
            var dto = dda.GetDataTransferObject(DataTransferObject);

            if (dto.Error.Equals("00000000000000000000"))
            {
                //Do Process
                var academiaPinaresService = new AcademiaPinaresService();
                var response = academiaPinaresService.DoProcess(dto);
                var dtoResponse = dda.GetDataTransferObjectString(response);
                return dtoResponse;
            }
            else
            {
                var dtoResponse = dda.GetDataTransferObjectString(dto);
                return dtoResponse;
            }
        }
    }
}
}
