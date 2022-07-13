using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Common
{
    public abstract class BaseMapping<TEntity> : IBaseMapping
            where TEntity : BaseEntity
    {
        public abstract string TableName { get; }

        public void MapearEntidade(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<TEntity>();
            MapearBase(entityBuilder);
            MapearEntidade(entityBuilder);
            MapearIndices(entityBuilder);
        }

        private void MapearBase(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.CriadoEm).HasColumnName("CRIADO_EM").HasColumnType("DATETIME").IsRequired();
            builder.Property(e => e.UltimaAtualizacaoEm).HasColumnName("ULTIMA_ATUALZIACAO_EM").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(e => e.EhAtivo).HasColumnName("ATIVO").IsRequired();
        }

        protected abstract void MapearEntidade(EntityTypeBuilder<TEntity> builder);
        protected virtual void MapearIndices(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasIndex(e => e.Id);
        }
    }
}
