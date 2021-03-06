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
    public class SalesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:49990/api/Vendas");
            var vendas = await response.Content.ReadAsAsync<IEnumerable<Venda>>();

            response = await client.GetAsync("http://localhost:49990/api/Produtos");
            var produtos = await response.Content.ReadAsAsync<IEnumerable<Produto>>();

            ViewBag.Produtos = produtos;
            ViewBag.Vendas = vendas;
            return View(vendas);
        }

        public async Task<ActionResult> View(string serie, string tipoDoc, string numDoc)
        {
            System.Diagnostics.Debug.WriteLine(serie + " " + tipoDoc + " " + numDoc);
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:49990/api/Vendas/Documento?" + "serie=" + serie + "&tipoDoc=" + tipoDoc + "&numDoc=" +numDoc);

            var linhaDoc = await response.Content.ReadAsAsync<IEnumerable<LinhaDocumento>>();

            response = await client.GetAsync("http://localhost:49990/api/Vendas");
            var vendas = await response.Content.ReadAsAsync<IEnumerable<Venda>>();

            var venda = new Venda();
            foreach(var v in vendas)
            {
                if(v.NumDoc == Int32.Parse(numDoc) && v.Serie == serie && v.TipoDoc == tipoDoc)
                {
                    venda = v;
                }
            }
            ViewBag.linhaDoc = linhaDoc;
            ViewBag.venda = venda;
            return View(linhaDoc);
        }
    }
}
