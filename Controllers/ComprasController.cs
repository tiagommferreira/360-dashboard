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
        
        // GET api/produto/5    
        public Compra Get(string id)
        {
            Compra compra = IntegrationPri.GetCompra(id);
            if (compra == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return compra;
            }
        }

        // GET api/produto/5/fatura    
        public List<LinhaFatura> GetFatura(string id)
        {
            return IntegrationPri.GetFaturaCompra(id);
        }
    }
}
