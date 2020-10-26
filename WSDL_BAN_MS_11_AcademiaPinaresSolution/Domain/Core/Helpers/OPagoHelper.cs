using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Helpers
{
    public class OPagoHelper
    {
        private string __oPago = "";
        private string __oPagoTemplate = "<clsPago xmlns='Caja Empresarial'>"
                                        + "<Recibo>{0}</Recibo>"
                                        + "<Cajero>{1}</Cajero>"
                                        + "<CodigoFamilia>{2}</CodigoFamilia>"
                                        + "<FechaPago>{3}</FechaPago>"
                                        + "<Valor>{4}</Valor>"
                                        + "<Periodo>{5}</Periodo>"
                                        + "<NumFactura>{6}</NumFactura>"
                                        + "<Cuota>{7}</Cuota>"
                                        + "<CodigoTransaccion>{8}</CodigoTransaccion>"
                                        + "</clsPago>";
        public OPagoHelper(__oPago oPago)
        {
            __oPago = string.Format(__oPagoTemplate,
                oPago.Recibo,
                oPago.Cajero,
                oPago.CodigoFamilia,
                oPago.FechaPago,
                oPago.Valor,
                oPago.Periodo,
                oPago.NumFactura,
                oPago.Cuota,
                oPago.CodigoTransaccion
                );
        }
        public string GetOPago()
        {
            return __oPago;
        }
    }

    public class __oPago
    {
        public string Recibo { set; get; }
        public string Cajero { set; get; }
        public string CodigoFamilia { set; get; }
        public string FechaPago { set; get; }
        public string Valor { set; get; }
        public string Periodo { set; get; }
        public string NumFactura { set; get; }
        public string Cuota { set; get; }
        public string CodigoTransaccion { set; get; }

    }
}
