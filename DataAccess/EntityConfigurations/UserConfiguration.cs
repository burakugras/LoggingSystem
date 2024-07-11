using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(t => t.Id);
            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.Username).HasColumnName("Username");
            builder.Property(b => b.Email).HasColumnName("Email");
            builder.Property(b => b.PasswordHash).HasColumnName("PasswordHash");
            builder.Property(b => b.PasswordSalt).HasColumnName("PasswordSalt");

            builder.HasMany(b => b.Activities)
                .WithOne(usm => usm.User)
                .HasForeignKey(usm => usm.UserId);

            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
        }
    }
}
