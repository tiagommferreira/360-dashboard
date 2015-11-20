using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_EXAMPLE_WS.Models
{
    public class Produto
    {
        public string CodProduto 
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public string UnidadeBase
        {
            get;
            set;
        }

        public string Moeda
        {
            get;
            set;
        }

        public double PVP1
        {
            get;
            set;
        }

        public double PVP2
        {
            get;
            set;
        }

        public double PVP3
        {
            get;
            set;
        }

        public double StockAtual
        {
            get;
            set;
        }
    }
}