using Interop.StdBE800;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_EXAMPLE_WS.Models
{
    public class IntegrationPri
    {

        #region Clientes
        //List clientes com Primavera API
        public static List<Cliente> ListaClientes()
        {

            StdBELista objList;

            List<Cliente> listClientes = new List<Cliente>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, Fac_Mor AS campo_exemplo FROM  CLIENTES");

                System.Diagnostics.Debug.WriteLine("LISTA");

                while (!objList.NoFim())
                {
                    Console.WriteLine(objList);
                    
                    listClientes.Add(new Cliente
                    {
                        CodCliente = objList.Valor("Cliente"),
                        NomeCliente = objList.Valor("Nome"),
                        Moeda = objList.Valor("Moeda"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        //Morada = objList.Valor("campo_exemplo")
                    });
                    
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }
        #endregion

        #region Produtos
        public static List<Produto> ListaProdutos()
        {

            StdBELista objList;

            List<Produto> listProdutos = new List<Produto>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT [ARTIGO].[ARTIGO] AS [artigo],[ARTIGO].[DESCRICAO] AS [descricao],[ARTIGOMOEDA].[UNIDADE] AS [unidadeMonetaria],[ARTIGOMOEDA].[MOEDA] AS [moeda],[ARTIGOMOEDA].[PVP1] AS [PVP1],[ARTIGOMOEDA].[PVP2] AS [PVP2],[ARTIGOMOEDA].[PVP3] AS [PVP3] FROM [ARTIGO] WITH (NOLOCK) LEFT JOIN [ARTIGOMOEDA] WITH (NOLOCK) ON  [ARTIGO].[ARTIGO] = [ARTIGOMOEDA].[ARTIGO] WHERE ( (([ARTIGO].[TRATAMENTODIM] < 2 OR [ARTIGO].[TRATAMENTODIM] > 2 OR [ARTIGO].[TRATAMENTODIM] IS NULL ) AND ([ARTIGOMOEDA].[MOEDA] = 'EUR')) )");

                while (!objList.NoFim())
                {
                    
                    listProdutos.Add(new Produto
                    {
                        CodProduto = objList.Valor("artigo"),
                        Descricao = objList.Valor("descricao"),
                        UnidadeBase = objList.Valor("unidadeMonetaria"),
                        Moeda = objList.Valor("moeda"),
                        PVP1 = objList.Valor("PVP1"),
                        PVP2 = objList.Valor("PVP2"),
                        PVP3 = objList.Valor("PVP3")
                    });
                    

                    objList.Seguinte();

                }

                return listProdutos;
            }
            else
                return null;
        }

        public static Produto GetProduto(string id)
        {
            StdBELista objList;

            Produto produto = new Produto();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT [ARTIGO].[ARTIGO] AS [artigo],[ARTIGO].[DESCRICAO] AS [descricao],[ARTIGOMOEDA].[UNIDADE] AS [unidadeMonetaria],[ARTIGOMOEDA].[MOEDA] AS [moeda],[ARTIGOMOEDA].[PVP1] AS [PVP1],[ARTIGOMOEDA].[PVP2] AS [PVP2],[ARTIGOMOEDA].[PVP3] AS [PVP3] FROM [ARTIGO] WITH (NOLOCK) LEFT JOIN [ARTIGOMOEDA] WITH (NOLOCK) ON  [ARTIGO].[ARTIGO] = [ARTIGOMOEDA].[ARTIGO] WHERE ( (([ARTIGO].[TRATAMENTODIM] < 2 OR [ARTIGO].[TRATAMENTODIM] > 2 OR [ARTIGO].[TRATAMENTODIM] IS NULL ) AND ([ARTIGOMOEDA].[MOEDA] = 'EUR') AND ([ARTIGO].[ARTIGO] = '" + id + "') ) )");

                if (!objList.Vazia())
                {
                    produto.CodProduto = objList.Valor("artigo");
                    produto.Descricao = objList.Valor("descricao");
                    produto.UnidadeBase = objList.Valor("unidadeMonetaria");
                    produto.Moeda = objList.Valor("moeda");
                    produto.PVP1 = objList.Valor("PVP1");
                    produto.PVP2 = objList.Valor("PVP2");
                    produto.PVP3 = objList.Valor("PVP3");
                    return produto;
                }

                
            }

            return null;
        }

        #endregion  
    
        #region Compras
            
        public static List<Compra> ListaCompras()
        {
            StdBELista objList;

            List<Compra> listCompras = new List<Compra>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta(" SELECT \"CabecCompras\".\"TipoDoc\", \"CabecCompras\".\"TipoEntidade\", \"CabecCompras\".\"TotalMerc\", \"CabecCompras\".\"TotalIva\", \"CabecCompras\".\"TotalDesc\", \"CabecCompras\".\"TotalOutros\", \"CabecCompras\".\"TotalDespesasAdicionais\", \"CabecCompras\".\"NumDoc\", \"CabecCompras\".\"Entidade\", \"CabecCompras\".\"DataIntroducao\", \"DocumentosCompra\".\"Descricao\", \"CabecCompras\".\"NumDocExterno\", \"CabecCompras\".\"Serie\", \"CabecCompras\".\"Cambio\", \"CabecCompras\".\"CambioMBase\", \"CabecCompras\".\"CambioMAlt\", \"CabecCompras\".\"TotalEcotaxa\", \"CabecCompras\".\"TotalIEC\", \"CabecCompras\".\"DataDoc\", \"CabecCompras\".\"TipoEntidade\", \"CabecComprasStatus\".\"Anulado\" FROM   (\"CabecComprasStatus\" \"CabecComprasStatus\" INNER JOIN \"CabecCompras\" \"CabecCompras\" ON \"CabecComprasStatus\".\"IdCabecCompras\"=\"CabecCompras\".\"Id\") INNER JOIN \"DocumentosCompra\" \"DocumentosCompra\" ON \"CabecCompras\".\"TipoDoc\"=\"DocumentosCompra\".\"Documento\" WHERE  (\"CabecCompras\".\"TipoDoc\"=N'VFA' OR \"CabecCompras\".\"TipoDoc\"=N'VFP' OR \"CabecCompras\".\"TipoDoc\"=N'VFR' OR \"CabecCompras\".\"TipoDoc\"=N'VGR' OR \"CabecCompras\".\"TipoDoc\"=N'VNC' OR \"CabecCompras\".\"TipoDoc\"=N'VVD') AND (\"CabecCompras\".\"DataDoc\">='2014-01-01 00:00:00' AND \"CabecCompras\".\"DataDoc\"<'2015-10-20 00:00:00') AND \"CabecComprasStatus\".\"Anulado\"=0 AND \"CabecCompras\".\"Serie\"=N'A' ORDER BY \"CabecCompras\".\"TipoDoc\", \"CabecCompras\".\"NumDoc\"");

                while (!objList.NoFim())
                {

                    listCompras.Add(new Compra
                    {
                        TipoDoc = objList.Valor("TipoDoc"),
                        TipoEntidade = objList.Valor("TipoEntidade"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                        TotalDesc = objList.Valor("TotalDesc"),
                        TotalOutros = objList.Valor("TotalOutros"),
                        TotalDespesasAdicionais = objList.Valor("TotalDespesasAdicionais"),
                        NumDoc = objList.Valor("NumDoc"),
                        Entidade = objList.Valor("Entidade"),
                        DataIntroducao = objList.Valor("DataIntroducao"),
                        Descricao = objList.Valor("Descricao"),
                        NumDocExterno = objList.Valor("NumDocExterno"),
                        Serie = objList.Valor("Serie"),
                        TotalEcoTaxa = objList.Valor("TotalEcoTaxa"),
                        DataDoc = objList.Valor("DataDoc"),
                        Anulado = objList.Valor("Anulado"),
                    });


                    objList.Seguinte();

                }

                return listCompras;
            }
            else
                return null;
        }

        public static Compra GetCompra(string id)
        {
            StdBELista objList;

            Compra compra = new Compra();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta(" SELECT \"CabecCompras\".\"TipoDoc\", \"CabecCompras\".\"TipoEntidade\", \"CabecCompras\".\"TotalMerc\", \"CabecCompras\".\"TotalIva\", \"CabecCompras\".\"TotalDesc\", \"CabecCompras\".\"TotalOutros\", \"CabecCompras\".\"TotalDespesasAdicionais\", \"CabecCompras\".\"NumDoc\", \"CabecCompras\".\"Entidade\", \"CabecCompras\".\"DataIntroducao\", \"DocumentosCompra\".\"Descricao\", \"CabecCompras\".\"NumDocExterno\", \"CabecCompras\".\"Serie\", \"CabecCompras\".\"Cambio\", \"CabecCompras\".\"CambioMBase\", \"CabecCompras\".\"CambioMAlt\", \"CabecCompras\".\"TotalEcotaxa\", \"CabecCompras\".\"TotalIEC\", \"CabecCompras\".\"DataDoc\", \"CabecCompras\".\"TipoEntidade\", \"CabecComprasStatus\".\"Anulado\" FROM   (\"CabecComprasStatus\" \"CabecComprasStatus\" INNER JOIN \"CabecCompras\" \"CabecCompras\" ON \"CabecComprasStatus\".\"IdCabecCompras\"=\"CabecCompras\".\"Id\") INNER JOIN \"DocumentosCompra\" \"DocumentosCompra\" ON \"CabecCompras\".\"TipoDoc\"=\"DocumentosCompra\".\"Documento\" WHERE  (\"CabecCompras\".\"TipoDoc\"=N'VFA' OR \"CabecCompras\".\"TipoDoc\"=N'VFP' OR \"CabecCompras\".\"TipoDoc\"=N'VFR' OR \"CabecCompras\".\"TipoDoc\"=N'VGR' OR \"CabecCompras\".\"TipoDoc\"=N'VNC' OR \"CabecCompras\".\"TipoDoc\"=N'VVD') AND (\"CabecCompras\".\"DataDoc\">='2014-01-01 00:00:00' AND \"CabecCompras\".\"DataDoc\"<'2015-10-20 00:00:00') AND \"CabecComprasStatus\".\"Anulado\"=0 AND \"CabecCompras\".\"Serie\"=N'A' AND \"CabecCompras\".\"NumDoc\"=" + id + " ORDER BY \"CabecCompras\".\"TipoDoc\", \"CabecCompras\".\"NumDoc\"");

                if (!objList.Vazia())
                {
                    compra.TipoDoc = objList.Valor("TipoDoc");
                    compra.TipoEntidade = objList.Valor("TipoEntidade");
                    compra.TotalMerc = objList.Valor("TotalMerc");
                    compra.TotalIva = objList.Valor("TotalIva");
                    compra.TotalDesc = objList.Valor("TotalDesc");
                    compra.TotalOutros = objList.Valor("TotalOutros");
                    compra.TotalDespesasAdicionais = objList.Valor("TotalDespesasAdicionais");
                    compra.NumDoc = objList.Valor("NumDoc");
                    compra.Entidade = objList.Valor("Entidade");
                    compra.DataIntroducao = objList.Valor("DataIntroducao");
                    compra.Descricao = objList.Valor("Descricao");
                    compra.NumDocExterno = objList.Valor("NumDocExterno");
                    compra.Serie = objList.Valor("Serie");
                    compra.TotalEcoTaxa = objList.Valor("TotalEcoTaxa");
                    compra.DataDoc = objList.Valor("DataDoc");
                    compra.Anulado = objList.Valor("Anulado");
                    return compra;
                }


            }

            return null;
        }

        #endregion


        #region Vendas

        public static List<Venda> ListaVendas()
        {
            StdBELista objList;

            List<Venda> listVendas = new List<Venda>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT \"CabecDoc\".\"TipoDoc\", \"CabecDoc\".\"Entidade\", \"CabecDoc\".\"NumDoc\", \"CabecDoc\".\"TotalIva\", \"CabecDoc\".\"TotalDesc\", \"CabecDoc\".\"TotalOutros\", \"CabecDoc\".\"Data\", \"DocumentosVenda\".\"Descricao\", \"CabecDoc\".\"TotalMerc\", \"CabecDoc\".\"Serie\", \"CabecDoc\".\"Nome\", \"CabecDoc\".\"Cambio\", \"CabecDoc\".\"CambioMBase\", \"CabecDoc\".\"CambioMAlt\", \"CabecDoc\".\"TotalEcotaxa\", \"CabecDoc\".\"TotalIEC\", \"EstadosConta\".\"Adiantamento\", \"CabecDoc\".\"TipoEntidade\", \"CabecDocStatus\".\"Anulado\" FROM   ((\"CabecDoc\" \"CabecDoc\" INNER JOIN \"DocumentosVenda\" \"DocumentosVenda\" ON \"CabecDoc\".\"TipoDoc\"=\"DocumentosVenda\".\"Documento\") INNER JOIN \"CabecDocStatus\" \"CabecDocStatus\" ON \"CabecDoc\".\"Id\"=\"CabecDocStatus\".\"IdCabecDoc\") LEFT OUTER JOIN \"EstadosConta\" \"EstadosConta\" ON (\"DocumentosVenda\".\"TipoConta\"=\"EstadosConta\".\"TipoConta\") AND (\"DocumentosVenda\".\"Estado\"=\"EstadosConta\".\"Estado\") WHERE  (\"CabecDoc\".\"TipoDoc\"=N'AVE' OR \"CabecDoc\".\"TipoDoc\"=N'FA' OR \"CabecDoc\".\"TipoDoc\"=N'GR' OR \"CabecDoc\".\"TipoDoc\"=N'NC' OR \"CabecDoc\".\"TipoDoc\"=N'VD') AND (\"CabecDoc\".\"Data\">= '2014-01-01 00:00:00' AND \"CabecDoc\".\"Data\"< '2015-10-20 00:00:00') AND \"CabecDocStatus\".\"Anulado\"=0 AND (\"CabecDoc\".\"Serie\"=N'A' OR \"CabecDoc\".\"Serie\"=N'C') ORDER BY \"CabecDoc\".\"TipoDoc\", \"CabecDoc\".\"NumDoc\"");

                while (!objList.NoFim())
                {

                    listVendas.Add(new Venda
                    {
                        TipoDoc = objList.Valor("TipoDoc"),
                        Entidade = objList.Valor("Entidade"),
                        NumDoc = objList.Valor("NumDoc"),
                        TotalIva = objList.Valor("TotalIva"),
                        TotalDesc = objList.Valor("TotalDesc"),
                        TotalOutros = objList.Valor("TotalOutros"),
                        Data = objList.Valor("Data"),
                        Descricao = objList.Valor("Descricao"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        Serie = objList.Valor("Serie"),
                        Nome = objList.Valor("Nome"),
                        TotalEcotaxa = objList.Valor("TotalEcotaxa"),
                        TotalIEC = objList.Valor("TotalIEC"),
                        //Adiantamento = objList.Valor("Adiantamento"),
                        TipoEntidade = objList.Valor("TipoEntidade"),
                        Anulado = objList.Valor("Anulado")
                    });


                    objList.Seguinte();

                }

                return listVendas;
            }
            else
                return null;
        }

        public static Venda GetVenda(string id)
        {
            StdBELista objList;

            Venda venda = new Venda();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT \"CabecDoc\".\"TipoDoc\", \"CabecDoc\".\"Entidade\", \"CabecDoc\".\"NumDoc\", \"CabecDoc\".\"TotalIva\", \"CabecDoc\".\"TotalDesc\", \"CabecDoc\".\"TotalOutros\", \"CabecDoc\".\"Data\", \"DocumentosVenda\".\"Descricao\", \"CabecDoc\".\"TotalMerc\", \"CabecDoc\".\"Serie\", \"CabecDoc\".\"Nome\", \"CabecDoc\".\"Cambio\", \"CabecDoc\".\"CambioMBase\", \"CabecDoc\".\"CambioMAlt\", \"CabecDoc\".\"TotalEcotaxa\", \"CabecDoc\".\"TotalIEC\", \"EstadosConta\".\"Adiantamento\", \"CabecDoc\".\"TipoEntidade\", \"CabecDocStatus\".\"Anulado\" FROM   ((\"CabecDoc\" \"CabecDoc\" INNER JOIN \"DocumentosVenda\" \"DocumentosVenda\" ON \"CabecDoc\".\"TipoDoc\"=\"DocumentosVenda\".\"Documento\") INNER JOIN \"CabecDocStatus\" \"CabecDocStatus\" ON \"CabecDoc\".\"Id\"=\"CabecDocStatus\".\"IdCabecDoc\") LEFT OUTER JOIN \"EstadosConta\" \"EstadosConta\" ON (\"DocumentosVenda\".\"TipoConta\"=\"EstadosConta\".\"TipoConta\") AND (\"DocumentosVenda\".\"Estado\"=\"EstadosConta\".\"Estado\") WHERE (\"CabecDoc\".\"NumDoc\" = '" + id + "') AND (\"CabecDoc\".\"TipoDoc\"=N'AVE' OR \"CabecDoc\".\"TipoDoc\"=N'FA' OR \"CabecDoc\".\"TipoDoc\"=N'GR' OR \"CabecDoc\".\"TipoDoc\"=N'NC' OR \"CabecDoc\".\"TipoDoc\"=N'VD') AND (\"CabecDoc\".\"Data\">= '2014-01-01 00:00:00' AND \"CabecDoc\".\"Data\"< '2015-10-20 00:00:00') AND \"CabecDocStatus\".\"Anulado\"=0 AND (\"CabecDoc\".\"Serie\"=N'A' OR \"CabecDoc\".\"Serie\"=N'C') ORDER BY \"CabecDoc\".\"TipoDoc\", \"CabecDoc\".\"NumDoc\"");

                if (!objList.Vazia())
                {
                    venda.TipoDoc = objList.Valor("TipoDoc");
                    venda.Entidade = objList.Valor("Entidade");
                    venda.NumDoc = objList.Valor("NumDoc");
                    venda.TotalIva = objList.Valor("TotalIva");
                    venda.TotalDesc = objList.Valor("TotalDesc");
                    venda.TotalOutros = objList.Valor("TotalOutros");
                    venda.Data = objList.Valor("Data");
                    venda.Descricao = objList.Valor("Descricao");
                    venda.TotalMerc = objList.Valor("TotalMerc");
                    venda.Serie = objList.Valor("Serie");
                    venda.Nome = objList.Valor("Nome");
                    venda.TotalEcotaxa = objList.Valor("TotalEcotaxa");
                    venda.TotalIEC = objList.Valor("TotalIEC");
                    //Adiantamento = objList.Valor("Adiantamento"),
                    venda.TipoEntidade = objList.Valor("TipoEntidade");
                    venda.Anulado = objList.Valor("Anulado");

                    return venda;
                }


            }

            return null;
        }

        #endregion
    }
}