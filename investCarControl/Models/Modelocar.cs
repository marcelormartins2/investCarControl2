using System;
using System.Collections.Generic;

namespace InvestCarControl.Models
{
    public partial class Modelocar
    {
        public Modelocar()
        {
            Veiculo = new HashSet<Veiculo>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int FabricanteId { get; set; }

        public Fabricante Fabricante { get; set; }
        public ICollection<Veiculo> Veiculo { get; set; }
    }
}
