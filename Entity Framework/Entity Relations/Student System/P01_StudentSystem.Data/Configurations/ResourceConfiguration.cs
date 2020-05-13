namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> resource)
        {
            resource.HasKey(r => r.ResourceId);

            resource
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode();

            resource
                .Property(r => r.Url)
                .IsRequired()
                .IsUnicode(false);

            resource
                .Property(r => r.ResourceType)
                .IsRequired();

            resource
                .HasOne(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CourseId);
        }
    }
}
