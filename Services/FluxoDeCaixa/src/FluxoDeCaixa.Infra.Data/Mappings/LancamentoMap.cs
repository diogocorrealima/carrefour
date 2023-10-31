using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Infra.Data.Mappings
{    
    public class LancamentoMap : IEntityTypeConfiguration<Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.IdUsuario)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Valor)
                .HasColumnType("decimal")
                .HasMaxLength(100)
                .IsRequired();   
        }
    }
}
