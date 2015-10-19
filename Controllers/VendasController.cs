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

        // GET api/Vendas/5   
        public Venda Get(string id)
        {
            Venda venda = IntegrationPri.GetVenda(id);
            if (venda == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return venda;
            }
        }
    }
}
