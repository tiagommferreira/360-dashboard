using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SINF_EXAMPLE_WS.Models;


namespace SINF_EXAMPLE_WS.Controllers
{
    public class PurchasesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:49990/api/Compras");
            var compras = await response.Content.ReadAsAsync<IEnumerable<Object>>();

            ViewBag.Compras = compras;

            return View(compras);
        }

        public async Task<ActionResult> View(string serie, string tipoDoc, string numDoc)
        {
            System.Diagnostics.Debug.WriteLine(serie + " " + tipoDoc + " " + numDoc);
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:49990/api/Compras/Documento?" + "serie=" + serie + "&tipoDoc=" + tipoDoc + "&numDoc=" + numDoc);

            var compras = await response.Content.ReadAsAsync<IEnumerable<Object>>();

            ViewBag.compras = compras;
            return View(compras);
        }
    }
}
