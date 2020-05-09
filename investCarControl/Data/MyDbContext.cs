using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using InvestCarControl.Models;

namespace InvestCarControl.Data
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
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
            modelBuilder.Entity<Despesa>(entity =>
            {
                entity.ToTable("despesa");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("datetime");

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Valor).HasColumnName("valor");
            });

            modelBuilder.Entity<Fabricante>(entity =>
            {
                entity.ToTable("fabricante");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Site)
                   .HasColumnName("site")
                   .HasColumnType("varchar(45)");
                entity.Property(e => e.Prioridade).HasColumnName("prioridade");
            });

            modelBuilder.Entity<Modelocar>(entity =>
            {
                entity.ToTable("modelocar");

                entity.HasIndex(e => e.FabricanteId)
                    .HasName("fk_modeloCar_fabricante1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FabricanteId).HasColumnName("fabricante_id");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.Fabricante)
                    .WithMany(p => p.Modelocar)
                    .HasForeignKey(d => d.FabricanteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_modeloCar_fabricante1");
            });

            modelBuilder.Entity<Parceiro>(entity =>
            {
                entity.ToTable("parceiro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Endereço)
                    .HasColumnName("endereço")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Telefone)
                    .HasColumnName("telefone")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Participacao>(entity =>
            {
                entity.HasKey(e => new { e.ParceiroId, e.VeiculoId });

                entity.ToTable("participacao");

                entity.HasIndex(e => e.ParceiroId)
                    .HasName("fk_parceiro_has_veiculo_parceiro_idx");

                entity.HasIndex(e => e.VeiculoId)
                    .HasName("fk_parceiro_has_veiculo_veiculo1_idx");

                entity.Property(e => e.ParceiroId).HasColumnName("parceiro_id");

                entity.Property(e => e.VeiculoId).HasColumnName("veiculo_id");

                entity.Property(e => e.PorcentagemCompra).HasColumnName("porcentagemCompra");

                entity.Property(e => e.PorcentagemLucro).HasColumnName("porcentagemLucro");

                entity.HasOne(d => d.Parceiro)
                    .WithMany(p => p.Participacao)
                    .HasForeignKey(d => d.ParceiroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_parceiro_has_veiculo_parceiro");

                entity.HasOne(d => d.Veiculo)
                    .WithMany(p => p.Participacao)
                    .HasForeignKey(d => d.VeiculoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_parceiro_has_veiculo_veiculo1");
            });

            modelBuilder.Entity<Responsavel>(entity =>
            {
                entity.HasKey(e => new { e.DespesaId, e.ParceiroId });

                entity.ToTable("responsavel");

                entity.HasIndex(e => e.DespesaId)
                    .HasName("fk_despesa_has_parceiro_despesa1_idx");

                entity.HasIndex(e => e.ParceiroId)
                    .HasName("fk_despesa_has_parceiro_parceiro1_idx");

                entity.Property(e => e.DespesaId).HasColumnName("despesa_id");

                entity.Property(e => e.ParceiroId).HasColumnName("parceiro_id");

                entity.Property(e => e.Valor).HasColumnName("valor");

                entity.HasOne(d => d.Despesa)
                    .WithMany(p => p.Responsavel)
                    .HasForeignKey(d => d.DespesaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_despesa_has_parceiro_despesa1");

                entity.HasOne(d => d.Parceiro)
                    .WithMany(p => p.Responsavel)
                    .HasForeignKey(d => d.ParceiroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_despesa_has_parceiro_parceiro1");
            });

            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.ToTable("veiculo");

                entity.HasIndex(e => e.DespesaId)
                    .HasName("fk_veiculo_despesa1_idx");

                entity.HasIndex(e => e.ModeloCarId)
                    .HasName("fk_veiculo_modeloCar1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnoFab).HasColumnName("anofab");

                entity.Property(e => e.AnoModelo).HasColumnName("anoModelo");

                entity.Property(e => e.Hodometro).HasColumnName("hodometro");

            entity.Property(e => e.Origem)
                        .HasColumnName("origem")
                        .HasColumnType("varchar(20)");

            entity.Property(e => e.Renavam).HasColumnName("renavam");

            entity.Property(e => e.ValorFipe).HasColumnName("valorfipe");

            entity.Property(e => e.ValorPago).HasColumnName("valorpago");

            entity.Property(e => e.ValorVenda).HasColumnName("valorvenda");

        entity.Property(e => e.Chassis)
                    .HasColumnName("chassis")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Cor)
                    .HasColumnName("cor")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.DespesaId).HasColumnName("despesa_id");

                entity.Property(e => e.Dut)
                    .HasColumnName("dut")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ModeloCarId).HasColumnName("modeloCar_id");

                entity.Property(e => e.Placa)
                    .HasColumnName("placa")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.Despesa)
                    .WithMany(p => p.Veiculo)
                    .HasForeignKey(d => d.DespesaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_veiculo_despesa1");

                entity.HasOne(d => d.ModeloCar)
                    .WithMany(p => p.Veiculo)
                    .HasForeignKey(d => d.ModeloCarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_veiculo_modeloCar1");
            });
        }
    }
}
