using Core.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityConfigurations
{
    public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.ToTable("OperationClaims").HasKey(t => t.Id);
            builder.Property(b=>b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b=>b.Name).HasColumnName("Name").IsRequired();

            builder.HasMany(u => u.UserOperationClaims);

            builder.HasQueryFilter(b=>!b.DeletedDate.HasValue);
        }
    }
}
