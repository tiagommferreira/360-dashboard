using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_EXAMPLE_WS.Models;

namespace SINF_EXAMPLE_WS.Controllers
{
    public class TransacoesController : ApiController
    {
        
        //
        // GET: /Compras/

        public IEnumerable<Transacao> Get()
        {
            return IntegrationPri.ListaTransacoes();
        }

        // GET api/produto/5    
        public Transacao Get(string id)
        {
            Transacao transacao = IntegrationPri.GetTransacao(id);
            if (transacao == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return transacao;
            }
        }

    }
}
