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
            var response = await client.GetAsync("http://localhost:49990/api/Vendas");
            var sales = await response.Content.ReadAsAsync<IEnumerable<Venda>>();
            response = await client.GetAsync("http://localhost:49990/api/Funcionarios");
            var funcionarios = await response.Content.ReadAsAsync<IEnumerable<Funcionario>>();


            var topProductsResponse = await client.GetAsync("http://localhost:49990/api/Produtos/Top");
            var topProducts = await topProductsResponse.Content.ReadAsAsync<IEnumerable<object>>();

            var vendasInfoResponse = await client.GetAsync("http://localhost:49990/api/Vendas/Info");
            var vendasInfo = await vendasInfoResponse.Content.ReadAsAsync < IEnumerable<TransacaoInfo>>();
            
            ViewBag.Sales = sales;
            ViewBag.Funcionarios = funcionarios;
            ViewBag.TopProdutos = topProducts;

            return View(sales);
        }
    }
}
