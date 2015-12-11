using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_EXAMPLE_WS.Models;
using System.Web;

namespace SINF_EXAMPLE_WS.Controllers
{
    public class ComprasController : ApiController
    {

        //
        // GET: /Compras/

        public IEnumerable<Compra> Get()
        {
            return IntegrationPri.ListaCompras();
        }

        // GET api/Compras/Documento?serie=A&tipoDoc=FA&numDoc=1
        public List<LinhaDocumento> GetDocumento(string serie, string tipoDoc, string numDoc)
        {
            return IntegrationPri.GetDocumentoCompra(serie, tipoDoc, numDoc);
        }

        // GET api/Compras/Total?ano=2014
        public double GetTotal(string ano)
        {
            return IntegrationPri.GetTotalCompras(ano);
        }

        public IEnumerable<TransacaoInfo> GetInfo()
        {
            string year = HttpContext.Current.Request.QueryString["year"];
            return IntegrationPri.GetCompraInfo(year);
        }

        public double GetCash()
        {
            return IntegrationPri.GetCash();
        }
        
    }
}
