﻿using Interop.StdBE800;
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

                objList = PriEngine.Engine.Consulta("SELECT [ARTIGO].[ARTIGO] AS [artigo],[ARTIGO].[DESCRICAO] AS [descricao],[ARTIGO].[STKACTUAL] AS [stock] ,[ARTIGOMOEDA].[UNIDADE] AS [unidadeMonetaria],[ARTIGOMOEDA].[MOEDA] AS [moeda],[ARTIGOMOEDA].[PVP1] AS [PVP1],[ARTIGOMOEDA].[PVP2] AS [PVP2],[ARTIGOMOEDA].[PVP3] AS [PVP3] FROM [ARTIGO] WITH (NOLOCK) LEFT JOIN [ARTIGOMOEDA] WITH (NOLOCK) ON  [ARTIGO].[ARTIGO] = [ARTIGOMOEDA].[ARTIGO] WHERE ( (([ARTIGO].[TRATAMENTODIM] < 2 OR [ARTIGO].[TRATAMENTODIM] > 2 OR [ARTIGO].[TRATAMENTODIM] IS NULL ) AND ([ARTIGOMOEDA].[MOEDA] = 'EUR')) )");

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
                        PVP3 = objList.Valor("PVP3"),
                        StockAtual = objList.Valor("stock")
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

        #region Transacoes

        public static List<Transacao> ListaSalarios()
        {

            string query = "SELECT Pagamentos.Ano AS Ano, Pagamentos.TipoVenc AS TipoVenc, Pagamentos.NumPeriodoProcessado AS NumPeriodoProcessado, Funcionarios.Codigo AS FuncCodigo, Funcionarios.Nome AS FuncNome, FuncFormasPagamento.Moeda AS PagamentoMoeda, FuncFormasPagamento.ContaEmpresa AS PagamentoContaEmpresa,FuncFormasPagamento.Percentagem AS PagamentoPercentagem,FuncFormasPagamento.ModoPagTesouraria AS PagamentoModoPagamento,Pagamentos.DataEfectiva AS PagamentoData,Pagamentos.ValorLiquido AS PagamentoValorLiquido  FROM Pagamentos, FuncFormasPagamento INNER JOIN Funcionarios ON Funcionarios.Codigo=FuncFormasPagamento.Funcionario WHERE Activo = 1 and Pagamentos.Funcionario=Funcionarios.Codigo";

            StdBELista objList;

            List<Transacao> listTransacoes = new List<Transacao>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {

                    listTransacoes.Add(new Transacao
                    {
                        FuncCodigo = objList.Valor("FuncCodigo"),
                        FuncNome = objList.Valor("FuncNome"),
                        TipoVenc = objList.Valor("TipoVenc"),
                        PagamentoMoeda = objList.Valor("PagamentoMoeda"),
                        PagamentoContaEmpresa = objList.Valor("PagamentoContaEmpresa"),
                        PagamentoPercentagem = objList.Valor("PagamentoPercentagem"),
                        PagamentoModoPagamento = objList.Valor("PagamentoModoPagamento"),
                       // TODO:  PagamentoData = (DateTime) objList.Valor("PagamentoData"),
                        PagamentoValorLiquido = objList.Valor("PagamentoValorLiquido"),
                        Ano = objList.Valor("Ano"),
                        Mes = objList.Valor("NumPeriodoProcessado")
                        
                    });


                    objList.Seguinte();

                }

                return listTransacoes;
            }
            else
                return null;
        }

        public static List<Transacao> GetTransacao(string codigoFuncionario, int ano, int mes)
        {
            StdBELista objList;

            List<Transacao> listTransacoes = new List<Transacao>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT  Pagamentos.Ano AS Ano, Pagamentos.TipoVenc AS TipoVenc, Pagamentos.NumPeriodoProcessado AS NumPeriodoProcessado, Funcionarios.Codigo AS FuncCodigo, Funcionarios.Nome AS FuncNome, FuncFormasPagamento.Moeda AS PagamentoMoeda,	 FuncFormasPagamento.ContaEmpresa AS PagamentoContaEmpresa, FuncFormasPagamento.Percentagem AS PagamentoPercentagem,FuncFormasPagamento.ModoPagTesouraria AS PagamentoModoPagamento,Pagamentos.DataEfectiva AS PagamentoData,Pagamentos.ValorLiquido AS PagamentoValorLiquido FROM Pagamentos, FuncFormasPagamento INNER JOIN Funcionarios ON Funcionarios.Codigo=FuncFormasPagamento.Funcionario WHERE Activo = 1 and Pagamentos.Funcionario='" + codigoFuncionario + "' and Pagamentos.Funcionario=Funcionarios.Codigo and Pagamentos.Ano=" + ano + " and Pagamentos.NumPeriodoProcessado=" + mes + ";");

                while (!objList.NoFim())
                {
                    listTransacoes.Add(new Transacao
                    {
                        FuncCodigo = objList.Valor("FuncCodigo"),
                        FuncNome = objList.Valor("FuncNome"),
                        TipoVenc = objList.Valor("TipoVenc"),
                        PagamentoMoeda = objList.Valor("PagamentoMoeda"),
                        PagamentoContaEmpresa = objList.Valor("PagamentoContaEmpresa"),
                        PagamentoPercentagem = objList.Valor("PagamentoPercentagem"),
                        PagamentoModoPagamento = objList.Valor("PagamentoModoPagamento"),
                        // TODO:  PagamentoData = (DateTime) objList.Valor("PagamentoData"),
                        PagamentoValorLiquido = objList.Valor("PagamentoValorLiquido"),
                        Ano = objList.Valor("Ano"),
                        Mes = objList.Valor("NumPeriodoProcessado")

                    });

                    objList.Seguinte();

                }

                return listTransacoes;
            }
            else
                return null;
        }

        public static List<Transacao> GetSalarios(string codigoFuncionario)
        {
            StdBELista objList;

            List<Transacao> listTransacoes = new List<Transacao>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT  Pagamentos.Ano AS Ano, Pagamentos.TipoVenc AS TipoVenc, Pagamentos.NumPeriodoProcessado AS NumPeriodoProcessado, Funcionarios.Codigo AS FuncCodigo, Funcionarios.Nome AS FuncNome, FuncFormasPagamento.Moeda AS PagamentoMoeda,	 FuncFormasPagamento.ContaEmpresa AS PagamentoContaEmpresa, FuncFormasPagamento.Percentagem AS PagamentoPercentagem,FuncFormasPagamento.ModoPagTesouraria AS PagamentoModoPagamento,Pagamentos.DataEfectiva AS PagamentoData,Pagamentos.ValorLiquido AS PagamentoValorLiquido FROM Pagamentos, FuncFormasPagamento INNER JOIN Funcionarios ON Funcionarios.Codigo=FuncFormasPagamento.Funcionario WHERE Activo = 1 and Pagamentos.Funcionario='" + codigoFuncionario + "' and Pagamentos.Funcionario=Funcionarios.Codigo;");

                while (!objList.NoFim())
                {
                    listTransacoes.Add(new Transacao
                    {
                        FuncCodigo = objList.Valor("FuncCodigo"),
                        FuncNome = objList.Valor("FuncNome"),
                        TipoVenc = objList.Valor("TipoVenc"),
                        PagamentoMoeda = objList.Valor("PagamentoMoeda"),
                        PagamentoContaEmpresa = objList.Valor("PagamentoContaEmpresa"),
                        PagamentoPercentagem = objList.Valor("PagamentoPercentagem"),
                        PagamentoModoPagamento = objList.Valor("PagamentoModoPagamento"),
                        // TODO:  PagamentoData = (DateTime) objList.Valor("PagamentoData"),
                        PagamentoValorLiquido = objList.Valor("PagamentoValorLiquido"),
                        Ano = objList.Valor("Ano"),
                        Mes = objList.Valor("NumPeriodoProcessado")

                    });

                    objList.Seguinte();

                }

                return listTransacoes;
            }
            else
                return null;
        }

        #endregion

        #region faturas

        public static List<LinhaDocumento> GetDocumentoCompra(string serie, string tipoDoc, string numDoc)
        {

            StdBELista objList;

            List<LinhaDocumento> listLinhaFatura = new List<LinhaDocumento>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT \"LinhasCompras\".\"TipoLinha\", \"LinhasCompras\".\"Artigo\", \"LinhasCompras\".\"Quantidade\", \"LinhasCompras\".\"PrecUnit\", \"LinhasCompras\".\"Desconto1\", \"LinhasCompras\".\"TaxaIva\", \"CabecCompras\".\"NumDoc\", \"CabecCompras\".\"DescPag\", \"CabecCompras\".\"Requisicao\", \"CabecCompras\".\"Moeda\", \"CabecCompras\".\"Cambio\", \"CondPag\".\"Descricao\" AS \"DescricaoFatura\", \"CabecCompras\".\"NumDocExterno\", \"LinhasCompras\".\"Descricao\", \"CabecCompras\".\"RegimeIva\", \"LinhasCompras\".\"PrecoLiquido\", \"CabecCompras\".\"Serie\", \"CabecCompras\".\"Nome\", \"CabecCompras\".\"Morada\", \"CabecCompras\".\"Localidade\", \"CabecCompras\".\"CodPostal\", \"CabecCompras\".\"CodPostalLocalidade\", \"CabecCompras\".\"TotalRetencao\", \"CabecCompras\".\"DescEntidade\", \"LinhasCompras\".\"Desconto2\", \"LinhasCompras\".\"Desconto3\", \"Moedas\".\"DecPrecUnit\", \"LinhasCompras\".\"Unidade\", \"CabecCompras\".\"TipoDoc\", \"CabecCompras\".\"Id\", \"LinhasCompras\".\"TotalDA\", \"LinhasCompras\".\"TotalIva\", \"CabecCompras\".\"TotalRetencaoGarantia\", \"LinhasCompras\".\"NumLinha\", \"LinhasCompras\".\"CodIva\", \"Iva\".\"MotivoIsencao\", \"DocumentosCompra\".\"PagarReceber\", \"DocumentosCompra\".\"TipoDocumento\", \"CabecCompras\".\"Morada2\", \"LinhasCompras\".\"TotalEcotaxa\", \"LinhasCompras\".\"TaxaIvaEcotaxa\", \"Iva\".\"IncluirValorDocs\", \"CabecComprasStatus\".\"ATDocCodeID\", \"DocumentosCompra\".\"BensCirculacao\", \"CabecComprasStatus\".\"Estado\", \"CabecComprasStatus\".\"Anulado\", \"Fornecedores\".\"EnderecoWeb\", \"CabecCompras\".\"NumContribuinte\", \"Fornecedores\".\"Matricula\", \"Fornecedores\".\"Conservatoria\", \"Fornecedores\".\"CapitalSocial\", \"CabecCompras\".\"DataDoc\", \"CabecCompras\".\"DataVencimento\", \"CabecCompras\".\"Filial\" FROM   (((((\"CabecCompras\" \"CabecCompras\" LEFT OUTER JOIN \"CondPag\" \"CondPag\" ON \"CabecCompras\".\"CondPag\"=\"CondPag\".\"CondPag\") LEFT OUTER JOIN \"Moedas\" \"Moedas\" ON \"CabecCompras\".\"Moeda\"=\"Moedas\".\"Moeda\") INNER JOIN \"DocumentosCompra\" \"DocumentosCompra\" ON \"CabecCompras\".\"TipoDoc\"=\"DocumentosCompra\".\"Documento\") INNER JOIN (\"Iva\" \"Iva\" FULL OUTER JOIN \"LinhasCompras\" \"LinhasCompras\" ON \"Iva\".\"Iva\"=\"LinhasCompras\".\"CodIva\") ON \"CabecCompras\".\"Id\"=\"LinhasCompras\".\"IdCabecCompras\") LEFT OUTER JOIN \"CabecComprasStatus\" \"CabecComprasStatus\" ON \"CabecCompras\".\"Id\"=\"CabecComprasStatus\".\"IdCabecCompras\") LEFT OUTER JOIN \"Fornecedores\" \"Fornecedores\" ON \"CabecCompras\".\"Entidade\"=\"Fornecedores\".\"Fornecedor\" WHERE \"CabecCompras\".\"Serie\"=N'" + serie + "' AND \"CabecCompras\".\"TipoDoc\"=N'" + tipoDoc + "' AND \"CabecCompras\".\"NumDoc\"=" + numDoc + " ORDER BY \"LinhasCompras\".\"NumLinha\"");

                while (!objList.NoFim())
                {
                    LinhaDocumento linhaFatura = new LinhaDocumento();

                    if (objList.Valor("TipoLinha") != null)
                        linhaFatura.TipoLinha = objList.Valor("TipoLinha");

                    if (objList.Valor("Artigo") != null)
                        linhaFatura.Artigo = objList.Valor("Artigo");

                    if (objList.Valor("Quantidade") != null)
                        linhaFatura.Quantidade = objList.Valor("Quantidade");

                    if (objList.Valor("PrecUnit") != null)
                        linhaFatura.PrecUnit = objList.Valor("PrecUnit");

                    if (objList.Valor("Desconto1") != null)
                        linhaFatura.Desconto1 = objList.Valor("Desconto1");

                    if (objList.Valor("TaxaIva") != null)
                        linhaFatura.TaxaIva = objList.Valor("TaxaIva");

                    if (objList.Valor("NumDoc") != null)
                        linhaFatura.TaxaIva = objList.Valor("NumDoc");

                    if (objList.Valor("DescPag") != null)
                        linhaFatura.DescPag = objList.Valor("DescPag");

                    if (objList.Valor("Requisicao") != null)
                        linhaFatura.Requisicao = objList.Valor("Requisicao");

                    if (objList.Valor("Moeda") != null)
                        linhaFatura.Moeda = objList.Valor("Moeda");

                    if (objList.Valor("DescricaoFatura") != null)
                        linhaFatura.DescricaoFatura = objList.Valor("DescricaoFatura");

                    if (objList.Valor("NumDocExterno") != null)
                        linhaFatura.NumDocExterno = objList.Valor("NumDocExterno");

                    if (objList.Valor("RegimeIva") != null)
                        linhaFatura.RegimeIva = objList.Valor("RegimeIva");

                    if (objList.Valor("PrecoLiquido") != null)
                        linhaFatura.PrecoLiquido = objList.Valor("PrecoLiquido");

                    if (objList.Valor("Serie") != null)
                        linhaFatura.Serie = objList.Valor("Serie");

                    if (objList.Valor("Nome") != null)
                        linhaFatura.Nome = objList.Valor("Nome");

                    if (objList.Valor("Morada") != null)
                        linhaFatura.Morada = objList.Valor("Morada");

                    if (objList.Valor("CodPostal") != null)
                        linhaFatura.CodPostal = objList.Valor("CodPostal");

                    if (objList.Valor("CodPostalLocalidade") != null)
                        linhaFatura.CodPostalLocalidade = objList.Valor("CodPostalLocalidade");

                    if (objList.Valor("Localidade") != null)
                        linhaFatura.Localidade = objList.Valor("Localidade");

                    if (objList.Valor("TotalRetencao") != null)
                        linhaFatura.TotalRetencao = objList.Valor("TotalRetencao");

                    if (objList.Valor("DescEntidade") != null)
                        linhaFatura.DescEntidade = objList.Valor("DescEntidade");

                    if (objList.Valor("Desconto2") != null)
                        linhaFatura.Desconto2 = objList.Valor("Desconto2");

                    if (objList.Valor("Desconto3") != null)
                        linhaFatura.Desconto3 = objList.Valor("Desconto3");

                    if (objList.Valor("DecPrecUnit") != null)
                        linhaFatura.DecPrecUnit = objList.Valor("DecPrecUnit");

                    if (objList.Valor("Unidade") != null)
                        linhaFatura.Unidade = objList.Valor("Unidade");

                    if (objList.Valor("TipoDoc") != null)
                        linhaFatura.TipoDoc = objList.Valor("TipoDoc");

                    if (objList.Valor("Id") != null)
                        linhaFatura.Id = objList.Valor("Id");

                    if (objList.Valor("TotalIva") != null)
                        linhaFatura.TotalIva = objList.Valor("TotalIva");

                    if (objList.Valor("TotalRetencaoGarantia") != null)
                        linhaFatura.TotalRetencaoGarantia = objList.Valor("TotalRetencaoGarantia");

                    if (objList.Valor("NumLinha") != null)
                        linhaFatura.NumLinha = objList.Valor("NumLinha");

                    if (objList.Valor("CodIva") != null)
                        linhaFatura.CodIva = objList.Valor("CodIva");

                    if (objList.Valor("MotivoIsencao") != null)
                        linhaFatura.MotivoIsencao = objList.Valor("MotivoIsencao");

                    if (objList.Valor("PagarReceber") != null)
                        linhaFatura.PagarReceber = objList.Valor("PagarReceber");

                    if (objList.Valor("Morada2") != null)
                        linhaFatura.Morada2 = objList.Valor("Morada2");

                    if (objList.Valor("Descricao") != null)
                        linhaFatura.Descricao = objList.Valor("Descricao");

                    if (objList.Valor("TotalEcotaxa") != null)
                        linhaFatura.TotalEcotaxa = objList.Valor("TotalEcotaxa");

                    if (objList.Valor("TaxaIvaEcotaxa") != null)
                        linhaFatura.TaxaIvaEcotaxa = objList.Valor("TaxaIvaEcotaxa");

                    if (objList.Valor("Estado") != null)
                        linhaFatura.Estado = objList.Valor("Estado");

                    if (objList.Valor("Anulado") != null)
                        linhaFatura.Anulado = objList.Valor("Anulado");

                    if (objList.Valor("EnderecoWeb") != null)
                        linhaFatura.EnderecoWeb = objList.Valor("EnderecoWeb"); 

                    if (objList.Valor("NumContribuinte") != null)
                        linhaFatura.NumContribuinte = objList.Valor("NumContribuinte");

                    if (objList.Valor("DataDoc") != null)
                        linhaFatura.DataDoc = objList.Valor("DataDoc");

                    if (objList.Valor("DataVencimento") != null)
                        linhaFatura.DataVencimento = objList.Valor("DataVencimento");

                    listLinhaFatura.Add(linhaFatura);


                    objList.Seguinte();

                }

                return listLinhaFatura;
            }
            else
                return null;
        }

        public static List<LinhaDocumento> GetDocumentoVenda(string serie, string tipoDoc, string numDoc)
        {
            StdBELista objList;

            List<LinhaDocumento> listLinhaFatura = new List<LinhaDocumento>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {
                
                objList = PriEngine.Engine.Consulta("SELECT \"LinhasDoc\".\"TipoLinha\", \"LinhasDoc\".\"Artigo\", \"LinhasDoc\".\"Quantidade\", \"LinhasDoc\".\"PrecUnit\", \"LinhasDoc\".\"Desconto1\", \"LinhasDoc\".\"TaxaIva\", \"CabecDoc\".\"NumDoc\", \"CabecDoc\".\"TipoDoc\", \"CabecDoc\".\"DescEntidade\", \"CabecDoc\".\"DescPag\", \"CabecDoc\".\"Requisicao\", \"CabecDoc\".\"Moeda\", \"CabecDoc\".\"Cambio\", \"LinhasDoc\".\"Descricao\", \"CabecDoc\".\"RegimeIva\", \"CondPag\".\"Descricao\" AS \"DescricaoFatura\", \"LinhasDoc\".\"PrecoLiquido\", \"CabecDoc\".\"Serie\", \"Moedas\".\"DecPrecUnit\", \"CabecDoc\".\"TotalRetencao\", \"DocumentosVenda\".\"LiquidacaoAutomatica\", \"LinhasDoc\".\"Desconto2\", \"LinhasDoc\".\"Desconto3\", \"LinhasDoc\".\"Unidade\", \"CabecDoc\".\"Id\", \"LinhasDoc\".\"TotalIva\", \"LinhasDoc\".\"NumLinha\", \"LinhasDoc\".\"TotalDA\", \"CabecDoc\".\"TotalRetencaoGarantia\", \"LinhasDoc\".\"CodIva\", \"Iva\".\"MotivoIsencao\", \"DocumentosVenda\".\"PagarReceber\", \"CabecDoc\".\"Nome\", \"CabecDoc\".\"NomeFac\", \"CabecDoc\".\"Morada\", \"CabecDoc\".\"MoradaFac\", \"CabecDoc\".\"Morada2\", \"CabecDoc\".\"Morada2Fac\", \"CabecDoc\".\"Localidade\", \"CabecDoc\".\"LocalidadeFac\", \"CabecDoc\".\"CodPostal\", \"CabecDoc\".\"CodigoPostalFac\", \"CabecDoc\".\"CodPostalLocalidade\", \"CabecDoc\".\"LocalidadeCodigoPostalFac\", \"CabecDoc\".\"NumContribuinte\", \"CabecDoc\".\"NumContribuinteFac\", \"LinhasDoc\".\"TotalEcotaxa\", \"LinhasDoc\".\"TaxaIvaEcotaxa\", \"DocumentosVenda\".\"TipoDocumento\", \"SeriesVendas\".\"DescricaoVia04\", \"SeriesVendas\".\"DescricaoVia05\", \"SeriesVendas\".\"DescricaoVia06\", \"Iva\".\"IncluirValorDocs\", \"CabecDocStatus\".\"ATDocCodeID\", \"DocumentosVenda\".\"BensCirculacao\", \"CabecDocStatus\".\"Estado\", \"CabecDocStatus\".\"Anulado\", \"CabecDoc\".\"DataVencimento\", \"CabecDoc\".\"Data\", \"Moedas\".\"DecArredonda\", \"CabecDoc\".\"CambioMAlt\", \"CabecDoc\".\"CambioMBase\", \"Moedas\".\"DescParteInteira\", \"Moedas\".\"DescParteDecimal\", \"CabecDoc\".\"Filial\" FROM   ((((((\"CabecDoc\" \"CabecDoc\" INNER JOIN \"LinhasDoc\" \"LinhasDoc\" ON \"CabecDoc\".\"Id\"=\"LinhasDoc\".\"IdCabecDoc\") LEFT OUTER JOIN \"CondPag\" \"CondPag\" ON \"CabecDoc\".\"CondPag\"=\"CondPag\".\"CondPag\") INNER JOIN \"Moedas\" \"Moedas\" ON \"CabecDoc\".\"Moeda\"=\"Moedas\".\"Moeda\") INNER JOIN \"DocumentosVenda\" \"DocumentosVenda\" ON \"CabecDoc\".\"TipoDoc\"=\"DocumentosVenda\".\"Documento\") INNER JOIN \"SeriesVendas\" \"SeriesVendas\" ON (\"CabecDoc\".\"TipoDoc\"=\"SeriesVendas\".\"TipoDoc\") AND (\"CabecDoc\".\"Serie\"=\"SeriesVendas\".\"Serie\")) FULL OUTER JOIN \"Iva\" \"Iva\" ON \"LinhasDoc\".\"CodIva\"=\"Iva\".\"Iva\") LEFT OUTER JOIN \"CabecDocStatus\" \"CabecDocStatus\" ON \"CabecDoc\".\"Id\"=\"CabecDocStatus\".\"IdCabecDoc\" WHERE  \"CabecDoc\".\"Filial\"=N'000' AND \"CabecDoc\".\"Serie\"=N'" + serie + "' AND \"CabecDoc\".\"TipoDoc\"=N'" + tipoDoc + "' AND \"CabecDoc\".\"NumDoc\"=" + numDoc + " ORDER BY \"LinhasDoc\".\"NumLinha\"");
                

                while (!objList.NoFim())
                {
                    LinhaDocumento linhaFatura = new LinhaDocumento();

                    if (objList.Valor("TipoLinha") != null)
                        linhaFatura.TipoLinha = objList.Valor("TipoLinha");

                    if (objList.Valor("Artigo") != null)
                        linhaFatura.Artigo = objList.Valor("Artigo");

                    if (objList.Valor("Quantidade") != null)
                        linhaFatura.Quantidade = objList.Valor("Quantidade");

                    if (objList.Valor("PrecUnit") != null)
                        linhaFatura.PrecUnit = objList.Valor("PrecUnit");

                    if (objList.Valor("Desconto1") != null)
                        linhaFatura.Desconto1 = objList.Valor("Desconto1");

                    if (objList.Valor("TaxaIva") != null)
                        linhaFatura.TaxaIva = objList.Valor("TaxaIva");

                    if (objList.Valor("NumDoc") != null)
                        linhaFatura.TaxaIva = objList.Valor("NumDoc");

                    if (objList.Valor("DescPag") != null)
                        linhaFatura.DescPag = objList.Valor("DescPag");

                    if (objList.Valor("Requisicao") != null)
                        linhaFatura.Requisicao = objList.Valor("Requisicao");

                    if (objList.Valor("Moeda") != null)
                        linhaFatura.Moeda = objList.Valor("Moeda");

                    if (objList.Valor("DescricaoFatura") != null)
                        linhaFatura.DescricaoFatura = objList.Valor("DescricaoFatura");

                    if (objList.Valor("RegimeIva") != null)
                        linhaFatura.RegimeIva = objList.Valor("RegimeIva");

                    if (objList.Valor("PrecoLiquido") != null)
                        linhaFatura.PrecoLiquido = objList.Valor("PrecoLiquido");

                    if (objList.Valor("Serie") != null)
                        linhaFatura.Serie = objList.Valor("Serie");

                    if (objList.Valor("Nome") != null)
                        linhaFatura.Nome = objList.Valor("Nome");

                    if (objList.Valor("Morada") != null)
                        linhaFatura.Morada = objList.Valor("Morada");

                    if (objList.Valor("CodPostal") != null)
                        linhaFatura.CodPostal = objList.Valor("CodPostal");

                    if (objList.Valor("CodPostalLocalidade") != null)
                        linhaFatura.CodPostalLocalidade = objList.Valor("CodPostalLocalidade");

                    if (objList.Valor("Localidade") != null)
                        linhaFatura.Localidade = objList.Valor("Localidade");

                    if (objList.Valor("TotalRetencao") != null)
                        linhaFatura.TotalRetencao = objList.Valor("TotalRetencao");

                    if (objList.Valor("DescEntidade") != null)
                        linhaFatura.DescEntidade = objList.Valor("DescEntidade");

                    if (objList.Valor("Desconto2") != null)
                        linhaFatura.Desconto2 = objList.Valor("Desconto2");

                    if (objList.Valor("Desconto3") != null)
                        linhaFatura.Desconto3 = objList.Valor("Desconto3");

                    if (objList.Valor("DecPrecUnit") != null)
                        linhaFatura.DecPrecUnit = objList.Valor("DecPrecUnit");

                    if (objList.Valor("Unidade") != null)
                        linhaFatura.Unidade = objList.Valor("Unidade");

                    if (objList.Valor("TipoDoc") != null)
                        linhaFatura.TipoDoc = objList.Valor("TipoDoc");

                    if (objList.Valor("Id") != null)
                        linhaFatura.Id = objList.Valor("Id");

                    if (objList.Valor("TotalIva") != null)
                        linhaFatura.TotalIva = objList.Valor("TotalIva");

                    if (objList.Valor("TotalRetencaoGarantia") != null)
                        linhaFatura.TotalRetencaoGarantia = objList.Valor("TotalRetencaoGarantia");

                    if (objList.Valor("NumLinha") != null)
                        linhaFatura.NumLinha = objList.Valor("NumLinha");

                    if (objList.Valor("CodIva") != null)
                        linhaFatura.CodIva = objList.Valor("CodIva");

                    if (objList.Valor("MotivoIsencao") != null)
                        linhaFatura.MotivoIsencao = objList.Valor("MotivoIsencao");

                    if (objList.Valor("PagarReceber") != null)
                        linhaFatura.PagarReceber = objList.Valor("PagarReceber");

                    if (objList.Valor("Morada2") != null)
                        linhaFatura.Morada2 = objList.Valor("Morada2");

                    if (objList.Valor("Descricao") != null)
                        linhaFatura.Descricao = objList.Valor("Descricao");

                    if (objList.Valor("TotalEcotaxa") != null)
                        linhaFatura.TotalEcotaxa = objList.Valor("TotalEcotaxa");

                    if (objList.Valor("TaxaIvaEcotaxa") != null)
                        linhaFatura.TaxaIvaEcotaxa = objList.Valor("TaxaIvaEcotaxa");

                    if (objList.Valor("Estado") != null)
                        linhaFatura.Estado = objList.Valor("Estado");

                    if (objList.Valor("Anulado") != null)
                        linhaFatura.Anulado = objList.Valor("Anulado");

                    if (objList.Valor("NumContribuinte") != null)
                        linhaFatura.NumContribuinte = objList.Valor("NumContribuinte");

                    if (objList.Valor("Data") != null)
                        linhaFatura.DataDoc = objList.Valor("Data");

                    if (objList.Valor("DataVencimento") != null)
                        linhaFatura.DataVencimento = objList.Valor("DataVencimento");

                    listLinhaFatura.Add(linhaFatura);


                    objList.Seguinte();

                }

                return listLinhaFatura;
            }
            else
                return null;
        }

        #endregion

        #region Funcionarios

        public static List<Funcionario> ListaFuncionarios()
        {
            StdBELista objList;

            List<Funcionario> listFuncionarios = new List<Funcionario>();

            if (PriEngine.InitializeCompany(SINF_EXAMPLE_WS.Properties.Settings.Default.Company.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.User.Trim(), SINF_EXAMPLE_WS.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT [FUNCIONARIOS].[CODIGO],[FUNCIONARIOS].[NOME] FROM [FUNCIONARIOS] WITH (NOLOCK)");

                while (!objList.NoFim())
                {
                    Console.WriteLine(objList);

                    listFuncionarios.Add(new Funcionario
                    {
                        Codigo = objList.Valor("Codigo"),
                        Nome = objList.Valor("Nome"),
                    });

                    objList.Seguinte();

                }

                return listFuncionarios;
            }
            else
                return null;
        }

        #endregion

    }
}