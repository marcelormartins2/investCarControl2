using System;
using System.Collections.Generic;

namespace InvestCarControl.Models
{
    public partial class Responsavel
    {
        public int DespesaId { get; set; }
        public int ParceiroId { get; set; }
        public double Valor { get; set; }

        public Despesa Despesa { get; set; }
        public Parceiro Parceiro { get; set; }
    }
}
