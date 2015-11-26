using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SINF_EXAMPLE_WS.Models;


namespace SINF_EXAMPLE_WS.Controllers
{
    public class OrdersController : Controller
    {
        //
        // GET: /Orders/
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:49990/api/Encomendas");
            var encomendas = await response.Content.ReadAsAsync<IEnumerable<Encomenda>>();

            ViewBag.Encomendas = encomendas;
            return View();
        }

    }
}
