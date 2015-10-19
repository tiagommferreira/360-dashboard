using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_EXAMPLE_WS.Models
{
    public class Venda
    {
        public string TipoDoc
        {
            get;
            set;
        }

        public string Entidade
        {
            get;
            set;
        }

        public int NumDoc
        {
            get;
            set;
        }

        public double TotalIva
        {
            get;
            set;
        }

        public double TotalDesc
        {
            get;
            set;
        }

        public double TotalOutros
        {
            get;
            set;
        }

        public DateTime Data
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public double TotalMerc
        {
            get;
            set;
        }

        public string Serie
        {
            get;
            set;
        }

        public string Nome
        {
            get;
            set;
        }

        public double TotalEcotaxa
        {
            get;
            set;
        }

        public double TotalIEC
        {
            get;
            set;
        }
        /*
        public var Adiantamento
        {
            get;
            set;
        }
        */
        public string TipoEntidade
        {
            get;
            set;
        }

        public bool Anulado
        {
            get;
            set;
        }
    }
}