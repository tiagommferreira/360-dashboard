﻿using SINF_EXAMPLE_WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
            ViewBag.Sales = sales;
            ViewBag.Funcionarios = funcionarios;
            return View(sales);
        }
    }
}
