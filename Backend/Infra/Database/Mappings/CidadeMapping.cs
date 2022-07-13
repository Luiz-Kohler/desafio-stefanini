using Domain.Entities;
using Infra.Database.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Mappings
{
    public class CidadeMapping : BaseMapping<Cidade>
    {
        public override string TableName => "Cidade";

        protected override void MapearEntidade(EntityTypeBuilder<Cidade> builder)
        {
            builder.Property(c => c.Nome).HasColumnName("Nome").HasColumnType("VARCHAR(200)").IsRequired();
            builder.Property(c => c.UF).HasColumnName("UF").HasColumnType("CHAR(2)").IsRequired();

            builder.HasMany(c => c.Pessoas)
                .WithOne(p => p.Cidade)
                .HasForeignKey(p => p.CidadeId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
        protected override void MapearIndices(EntityTypeBuilder<Cidade> builder)
        {
            builder.HasIndex(c => new { c.Id, c.EhAtivo }, "ix_id_ativo");
            builder.HasIndex(c => new { c.Nome, c.UF, c.EhAtivo }, "ix_nome_uf_ativo");
        }
    }
}
