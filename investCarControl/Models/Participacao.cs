using System;
using System.Collections.Generic;

namespace InvestCarControl.Models
{
    public partial class Participacao
    {
        public int ParceiroId { get; set; }
        public int VeiculoId { get; set; }
        public double PorcentagemCompra { get; set; }
        public double PorcentagemLucro { get; set; }

        public Parceiro Parceiro { get; set; }
        public Veiculo Veiculo { get; set; }
    }
}
