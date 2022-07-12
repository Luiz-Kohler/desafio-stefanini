using Domain.Entities;
using Infra.Database.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Mappings
{
    public class PessoaMapping : BaseMapping<Pessoa>
    {
        public override string TableName => "Pessoa";

        protected override void MapearEntidade(EntityTypeBuilder<Pessoa> builder)
        {
            builder.Property(p => p.Nome).HasColumnName("Nome").HasColumnType("VARCHAR(300)").IsRequired();
            builder.Property(p => p.Cpf).HasColumnName("CPF").HasColumnType("CHAR(11)").IsRequired();
            builder.Property(p => p.DataNascimento).HasColumnName("Data_Nascimento").HasColumnType("DATETIME").IsRequired();

            builder.HasOne(p => p.Cidade)
                .WithMany(c => c.Pessoas)
                .HasForeignKey(p => p.CidadeId)
                .HasPrincipalKey(c => c.Id);
        }

        protected override void MapearIndices(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasIndex(p => new { p.Id, p.EhAtivo }, "ix_id_ativo");
            builder.HasIndex(p => new { p.Cpf, p.EhAtivo }, "ix_cpf_ativo");
        }
    }
}
