using System;
using System.Collections.Generic;

namespace InvestCarControl.Models
{
    public partial class Veiculo
    {
        public Veiculo()
        {
            Participacao = new HashSet<Participacao>();
        }

        public int Id { get; set; }
        public string Placa { get; set; }
        public string Chassis { get; set; }
        public string Cor { get; set; }
        public string Dut { get; set; }
        public int? Hodometro { get; set; }
        public int AnoFab { get; set; }
        public int AnoModelo { get; set; }
        public string Origem { get; set; }
        public int? Renavam { get; set; }
        public double? ValorFipe { get; set; }
        public double? ValorPago { get; set; }
        public double? ValorVenda { get; set; }
        public int? DespesaId { get; set; }
        public int ModeloCarId { get; set; }

        public Despesa Despesa { get; set; }
        public Modelocar ModeloCar { get; set; }
        public ICollection<Participacao> Participacao { get; set; }
    }
}
