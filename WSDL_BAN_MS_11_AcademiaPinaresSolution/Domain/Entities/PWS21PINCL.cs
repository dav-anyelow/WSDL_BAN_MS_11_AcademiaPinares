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
    public class PWS21PINCL : EasyMappingTool
    {
        public PWS21PINCL()
        {
            this.SetClProgram("BCAH96", "PWS21PINCL");
            var Guid = new EasyParameter();
            Guid.field = "Guid";
            Guid.type = OleDbType.VarChar;
            Guid.size = 100;
            Guid.value = "";
            Guid.scale = 0;
            Guid.parameterDirection = ParameterDirection.Input;
            AddParameter(Guid);

            var Tipo = new EasyParameter();
            Tipo.field = "Tipo";
            Tipo.type = OleDbType.VarChar;
            Tipo.size = 100;
            Tipo.value = "";
            Tipo.scale = 0;
            Tipo.parameterDirection = ParameterDirection.Input;
            AddParameter(Tipo);

            var Concepto = new EasyParameter();
            Concepto.field = "Concepto";
            Concepto.type = OleDbType.VarChar;
            Concepto.size = 100;
            Concepto.value = "";
            Concepto.scale = 0;
            Concepto.parameterDirection = ParameterDirection.Input;
            AddParameter(Concepto);

            var NumFactura = new EasyParameter();
            NumFactura.field = "NumFactura";
            NumFactura.type = OleDbType.VarChar;
            NumFactura.size = 100;
            NumFactura.value = "";
            NumFactura.scale = 0;
            NumFactura.parameterDirection = ParameterDirection.Input;
            AddParameter(NumFactura);

            var Cuota = new EasyParameter();
            Cuota.field = "Cuota";
            Cuota.type = OleDbType.VarChar;
            Cuota.size = 100;
            Cuota.value = "";
            Cuota.scale = 0;
            Cuota.parameterDirection = ParameterDirection.Input;
            AddParameter(Cuota);

            var Valor = new EasyParameter();
            Valor.field = "Valor";
            Valor.type = OleDbType.VarChar;
            Valor.size = 100;
            Valor.value = "";
            Valor.scale = 0;
            Valor.parameterDirection = ParameterDirection.Input;
            AddParameter(Valor);

            var Moneda = new EasyParameter();
            Moneda.field = "Moneda";
            Moneda.type = OleDbType.VarChar;
            Moneda.size = 100;
            Moneda.value = "";
            Moneda.scale = 0;
            Moneda.parameterDirection = ParameterDirection.Input;
            AddParameter(Moneda);

            var Vence = new EasyParameter();
            Vence.field = "Vence";
            Vence.type = OleDbType.VarChar;
            Vence.size = 100;
            Vence.value = "";
            Vence.scale = 0;
            Vence.parameterDirection = ParameterDirection.Input;
            AddParameter(Vence);

            var Periodo = new EasyParameter();
            Periodo.field = "Periodo";
            Periodo.type = OleDbType.VarChar;
            Periodo.size = 100;
            Periodo.value = "";
            Periodo.scale = 0;
            Periodo.parameterDirection = ParameterDirection.Input;
            AddParameter(Periodo);


        }
    }
}
