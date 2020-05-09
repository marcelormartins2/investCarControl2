using System;
using System.Collections.Generic;

namespace InvestCarControl.Models
{
    public partial class Fabricante
    {
        public Fabricante()
        {
            Modelocar = new HashSet<Modelocar>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Site { get; set; }
        public int? Prioridade { get; set; } = 0;

        public ICollection<Modelocar> Modelocar { get; set; }
    }
}
