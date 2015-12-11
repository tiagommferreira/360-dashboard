using SINF_EXAMPLE_WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace SINF_EXAMPLE_WS.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
 
            var client = new HttpClient();
            
            // Vendas
            var response = await client.GetAsync("http://localhost:49990/api/Vendas/Total?ano=2014");
            var sales = await response.Content.ReadAsAsync<double>();
            
            // Funcionarios
            response = await client.GetAsync("http://localhost:49990/api/Funcionarios");
            var funcionarios = await response.Content.ReadAsAsync<IEnumerable<Funcionario>>();

            // Compras
            response = await client.GetAsync("http://localhost:49990/api/Compras/Total?ano=2014");
            var purchases = await response.Content.ReadAsAsync<double>();
            purchases = Math.Abs(purchases);

            // Dinheiro em caixa
            response = await client.GetAsync("http://localhost:49990/api/Compras/Cash");
            var cashMoney = await response.Content.ReadAsAsync<double>();

            ViewBag.SalesValue = sales;
            ViewBag.PurchasesValue = purchases;
            ViewBag.Funcionarios = funcionarios;
            ViewBag.Caixa = cashMoney;

            return View(sales);
        }
    }
}
