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
    public class SalesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:49990/api/Vendas");
            var vendas = await response.Content.ReadAsAsync<IEnumerable<Venda>>();

            ViewBag.Vendas = vendas;
            return View(vendas);
        }

        public async Task<ActionResult> View(string serie, string tipoDoc, string numDoc)
        {
            System.Diagnostics.Debug.WriteLine(serie + " " + tipoDoc + " " + numDoc);
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:49990/api/Vendas/Documento?" + "serie=" + serie + "&tipoDoc=" + tipoDoc + "&numDoc=" +numDoc);
            
            var linhaDoc = await response.Content.ReadAsAsync<IEnumerable<LinhaDocumento>>();

            ViewBag.linhaDoc = linhaDoc;
            return View(linhaDoc);
        }
    }
}
