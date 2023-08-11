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
            .HasKey(pm => pm.Id);

            builder
                .HasOne(pm => pm.User)
                .WithMany(pm => pm.Comments)
                .HasForeignKey(pm => pm.IdUser);

            builder
                .HasOne(pm => pm.Project)
                .WithMany(pm => pm.Comments)
                .HasForeignKey(pm => pm.IdProject);
        }
    }
}
