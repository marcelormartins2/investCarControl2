using InvestCarControl.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvestCarControl.Data
{
    public class IdentyDbContext : IdentityDbContext<Parceiro>
    {
        public IdentyDbContext(DbContextOptions<IdentyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Despesa> Despesa { get; set; }
        public virtual DbSet<Fabricante> Fabricante { get; set; }
        public virtual DbSet<Modelocar> Modelocar { get; set; }
        public virtual DbSet<Parceiro> Parceiro { get; set; }
        public virtual DbSet<Participacao> Participacao { get; set; }
        public virtual DbSet<Responsavel> Responsavel { get; set; }
        public virtual DbSet<Veiculo> Veiculo { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Participacao>()
                .HasKey(pf => new { pf.ParceiroId, pf.VeiculoId });
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Responsavel>()
                .HasKey(pf => new { pf.ParceiroId, pf.DespesaId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
