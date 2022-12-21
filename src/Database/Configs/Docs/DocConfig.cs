using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Users;
using Models.Utils.I18N;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Docs
{
    public class DocConfig : IEntityTypeConfiguration<Doc>
    {
        public void Configure(EntityTypeBuilder<Doc> builder)
        {
            builder.ToTable("Docs");
            
            builder
                .HasIndex(x => x.Number)
                .IsUnique(true);

            builder
                .HasOne(x => x.DocType)
                .WithMany();


            builder
                .HasOne(x => x.CreatedUser)
                .WithMany(x => x.CreatedDocs)
                .HasForeignKey(x => x.CreatedUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.ReceiverUser)
                .WithMany(x => x.ReceivedDocs)
                .HasForeignKey(x => x.ReceiverUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
