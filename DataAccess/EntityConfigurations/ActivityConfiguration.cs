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
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities").HasKey(t => t.Id);
            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.UserId).HasColumnName("UserId");
            builder.Property(b => b.ActivityType).HasColumnName("ActivityType");
            builder.Property(b => b.Date).HasColumnName("Date");
            builder.Property(b => b.Description).HasColumnName("Description");

            builder.HasOne(b => b.User)
                .WithMany(u => u.Activities)
                .HasForeignKey(b => b.UserId);

            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
        }
    }
}
