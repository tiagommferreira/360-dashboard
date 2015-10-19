using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_EXAMPLE_WS.Models
{
    public class Compra
    {

        public string TipoDoc
        {
            get;
            set;
        }

        public string TipoEntidade
        {
            get;
            set;
        }

        public double TotalMerc
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

        public double TotalDespesasAdicionais
        {
            get;
            set;
        }
        public int NumDoc
        {
            get;
            set;
        }
        public string Entidade
        {
            get;
            set;
        }
        public DateTime DataIntroducao
        {
            get;
            set;
        }
        public string Descricao
        {
            get;
            set;
        }
        public string NumDocExterno
        {
            get;
            set;
        }
        public string Serie
        {
            get;
            set;
        }
        public double TotalEcoTaxa
        {
            get;
            set;
        }
        public DateTime DataDoc
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