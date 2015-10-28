using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_EXAMPLE_WS.Models;

namespace SINF_EXAMPLE_WS.Controllers
{
    public class SalariosController : ApiController
    {
        
        //
        // GET: /Compras/

        public IEnumerable<Transacao> Get()
        {
            return IntegrationPri.ListaSalarios();
        }

        // GET api/produto/5    
        public List<Transacao> Get(string codigoFuncionario, int ano, int mes)
        {
            List<Transacao> transacao = IntegrationPri.GetTransacao(codigoFuncionario, ano, mes);
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
