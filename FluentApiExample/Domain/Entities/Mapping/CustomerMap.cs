using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApiExample.Domain.Entities.Mapping
{
    public class CustomerMap:EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(true);

            this.Property(c => c.City)
                .HasMaxLength(20);

            this.Property(c => c.ContactName)
                .HasMaxLength(40);

            this.ToTable("Customers");

            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Country).HasColumnName("Country");

            this.Property(c => c.CompanyName).HasColumnName("CompanyName");

            this.Property(c => c.ContactName)
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_ContactName", 2) { IsUnique = true }));

            this.HasMany(c=>c.Orders)
                .WithOptional()
                .HasForeignKey(o=>o.CustomerId)
                .WillCascadeOnDelete(true);
        }
    }
}
