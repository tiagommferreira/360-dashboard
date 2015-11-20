using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_EXAMPLE_WS.Models;
using System.Web;
using System.Xml.Linq;

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

        // GET api/Produtos/Top?serie=A&tipoDoc=FA&numDoc=1
        public IEnumerable<object> GetTop()
        {
            return IntegrationPri.GetTopProdutos();
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
