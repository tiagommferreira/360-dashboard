using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_EXAMPLE_WS.Models
{
    public class Transacao
    {
        public string FuncCodigo
        {
            get;
            set;
        }
        public string FuncNome
        {
            get;
            set;
        }
        public string PagamentoMoeda
        {
            get;
            set;
        }
        public string PagamentoContaEmpresa
        {
            get;
            set;
        }
        public double PagamentoPercentagem
        {
            get;
            set;
        }
        public string PagamentoModoPagamento
        {
            get;
            set;
        }
        public DateTime PagamentoData
        {
            get;
            set;
        }
        public double PagamentoValorLiquido
        {
            get;
            set;
        }

    }
}