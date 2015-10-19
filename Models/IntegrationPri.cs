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
    }
}