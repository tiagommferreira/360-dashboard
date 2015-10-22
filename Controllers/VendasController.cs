using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_EXAMPLE_WS.Models;


namespace SINF_EXAMPLE_WS.Controllers
{
    public class VendasController : ApiController
    {
        // GET: /Vendas/

        public IEnumerable<Venda> Get()
        {
            return IntegrationPri.ListaVendas();
        }

        // GET api/Vendas/Documento?serie=A&tipoDoc=FA&numDoc=1
        public List<LinhaDocumento> GetDocumento(string serie, string tipoDoc, string numDoc)
        {
            return IntegrationPri.GetDocumentoVenda(serie, tipoDoc, numDoc);   
        }
    }
}