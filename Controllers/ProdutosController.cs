using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_EXAMPLE_WS.Models;

namespace SINF_EXAMPLE_WS.Controllers
{
    public class ProdutosController : ApiController
    {
        //
        // GET: /Produtos/
        public IEnumerable<Produto> Get()
        {
            return IntegrationPri.ListaProdutos();
        }

        // GET api/produto/5    
        public Produto Get(string id)
        {
            Produto artigo = IntegrationPri.GetProduto(id);
            if (artigo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return artigo;
            }
        }

    }
}
