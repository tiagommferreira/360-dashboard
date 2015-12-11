using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_EXAMPLE_WS.Models
{
    public class Encomenda
    {
        public string CodProduto
        {
            get;
            set;
        }

        public string ProdutoDescricao
        {
            get;
            set;
        }

        public DateTime DataEncomenda
        {
            get;
            set;
        }

        public double Quantidade
        {
            get;
            set;
        }

        public string Cliente
        {
            get;
            set;
        }

        public double PrecoUnitario
        {
            get;
            set;
        }

        public DateTime DataEntrega
        {
            get;
            set;
        }

    }
}