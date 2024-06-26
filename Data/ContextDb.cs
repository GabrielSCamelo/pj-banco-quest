﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pj_banco_quest.Models;

namespace pj_banco_quest.Data
{
    public class ContextDb : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Questao> Questoes { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Simulado> Simulados { get; set; }
        public DbSet<Simulado_Aluno> Simulados_Alunos { get; set; }
        public DbSet<Aluno_Turma> Alunos_Turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aluno_Turma>()
                .HasKey(at => new { at.AlunoId, at.TurmaId });

            modelBuilder.Entity<Simulado_Aluno>()
                .HasKey(sa => new { sa.SimuladoId, sa.AlunoId });

            modelBuilder.Entity<Questao>()
                .Property(q => q.OpcaoCorretaIndex)
                .HasMaxLength(1);

            modelBuilder.Entity<Simulado>()
                .Property(s => s.DataCriacao)
                .HasColumnType("datetime");

            modelBuilder.Entity<Aluno_Turma>()
                 .HasIndex(at => new { at.AlunoId, at.TurmaId })
                 .IsUnique()
                 .HasName("IX_AlunoTurma");

            modelBuilder.Entity<Simulado_Aluno>()
                .HasIndex(sa => new { sa.SimuladoId, sa.AlunoId })
                .IsUnique()
                .HasName("IX_SimuladoAluno");
        }
    }
}