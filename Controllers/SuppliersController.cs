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
    public class SuppliersController : Controller
    {
        //
        // GET: /Suppliers/
        public async Task<ActionResult> Index()
        {
            var clients = new HttpClient();
            var response = await clients.GetAsync("http://localhost:49990/api/Fornecedores");
            var fornecedores = await response.Content.ReadAsAsync<IEnumerable<Fornecedor>>();

            ViewBag.Fornecedores = fornecedores;
            return View();
        }

    }
}
