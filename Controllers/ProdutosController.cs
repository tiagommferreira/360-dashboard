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
            return null;
            //return IntegrationPri.ListaProdutos();
        }
    }
}
