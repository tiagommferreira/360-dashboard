using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_EXAMPLE_WS.Models;

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

        public IEnumerable<TransacaoInfo> GetInfo()
        {
            return IntegrationPri.GetCompraInfo();
        }
        
    }
}
