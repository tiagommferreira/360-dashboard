using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_EXAMPLE_WS.Models;

namespace SINF_EXAMPLE_WS.Controllers
{
    public class FuncionariosController : ApiController
    {
        // GET: /Funcionarios/

        public IEnumerable<Funcionario> Get()
        {
            return IntegrationPri.ListaFuncionarios();
        }

        // GET api/Funcionarios/Salarios  
        public IEnumerable<Transacao> GetSalarios(string id)
        {
            List<Transacao> transacoes = IntegrationPri.GetSalarios(id);
            if (transacoes == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return transacoes;
            }
        }
    }
}
