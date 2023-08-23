using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectCommentConfiguration : IEntityTypeConfiguration<ProjectComment>
    {
        public void Configure(EntityTypeBuilder<ProjectComment> builder)
        {
            builder
                .HasKey(projectComment => projectComment.Id);


            builder
                .HasOne(pc => pc.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(pc => pc.IdProject)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(p => p.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
