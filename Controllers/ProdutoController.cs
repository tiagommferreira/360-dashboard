using SINF_EXAMPLE_WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SINF_EXAMPLE_WS.Controllers
{
    public class ProdutoController : Controller
    {
        //
        // GET: /Produto/

        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:49990/api/Produtos");
            var produtos = await response.Content.ReadAsAsync<IEnumerable<Produto>>();
            
            ViewBag.Produtos = produtos;
            return View(produtos);
        }

    }
}
