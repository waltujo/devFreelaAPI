using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
             .ToTable("Projects")
            .HasKey(project => project.Id);

            builder
             .Property(p => p.TotalCost)
             .HasColumnType("decimal(18,4)");

            builder
                .HasOne(p => p.Freelancer)
                .WithMany(f => f.FreelanceProjects)
                .HasForeignKey(p => p.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                 .HasOne(p => p.Client)
                .WithMany(c => c.OwnedProjects)
                .HasForeignKey(p => p.IdClient)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
