using Interop.StdBE800;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_EXAMPLE_WS.Models
{
    public class IntegrationPri
    {
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
    }
}