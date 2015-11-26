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
    public class EncomendasController : ApiController
    {
        //
        // GET: /Encomendas/
        public IEnumerable<Encomenda> Get()
        {

            string product = HttpContext.Current.Request.QueryString["product"];
            return IntegrationPri.ListaEncomendas(product);
        }

        // GET api/Produtos/Top?serie=A&tipoDoc=FA&numDoc=1
        


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
