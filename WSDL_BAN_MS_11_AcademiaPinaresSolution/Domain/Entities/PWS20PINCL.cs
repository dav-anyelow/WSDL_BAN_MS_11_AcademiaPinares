using SUNITP.LIB.ManagerProcedures;
using SUNITP.LIB.ManagerProcedures.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PWS20PINCL : EasyMappingTool
    {
        public PWS20PINCL()
        {
            this.SetClProgram("BCAH96", "PWS20PINCL");
            var Guid = new EasyParameter();
            Guid.field = "Guid";
            Guid.type = OleDbType.VarChar;
            Guid.size = 100;
            Guid.value = "";
            Guid.scale = 0;
            Guid.parameterDirection = ParameterDirection.Input;
            AddParameter(Guid);

            var Nucleo = new EasyParameter();
            Nucleo.field = "Nucleo";
            Nucleo.type = OleDbType.VarChar;
            Nucleo.size = 100;
            Nucleo.value = "";
            Nucleo.scale = 0;
            Nucleo.parameterDirection = ParameterDirection.Input;
            AddParameter(Nucleo);

            var Codigo = new EasyParameter();
            Codigo.field = "Codigo";
            Codigo.type = OleDbType.VarChar;
            Codigo.size = 100;
            Codigo.value = "";
            Codigo.scale = 0;
            Codigo.parameterDirection = ParameterDirection.Input;
            AddParameter(Codigo);

            var Mensaje = new EasyParameter();
            Mensaje.field = "Mensaje";
            Mensaje.type = OleDbType.VarChar;
            Mensaje.size = 100;
            Mensaje.value = "";
            Mensaje.scale = 0;
            Mensaje.parameterDirection = ParameterDirection.Input;
            AddParameter(Mensaje);
            
        }
    }
}
