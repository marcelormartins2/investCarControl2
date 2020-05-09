using System;
using System.Collections.Generic;

namespace InvestCarControl.Models
{
    public partial class Parceiro
    {
        public Parceiro()
        {
            Participacao = new HashSet<Participacao>();
            Responsavel = new HashSet<Responsavel>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereço { get; set; }

        public ICollection<Participacao> Participacao { get; set; }
        public ICollection<Responsavel> Responsavel { get; set; }
    }
}
