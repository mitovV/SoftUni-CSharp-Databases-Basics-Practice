namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> course)
        {
            course
                .HasKey(c => c.CourseId);

            course
                .Property(c => c.Name)
                .HasMaxLength(80)
                .IsRequired()
                .IsUnicode();

            course
                .Property(c => c.Description)
                .HasMaxLength(300)
                .IsRequired(false)
                .IsUnicode();

            course
                .Property(c => c.StartDate)
                .HasColumnType("DATETIME2")
                .IsRequired();

            course
               .Property(c => c.EndDate)
               .HasColumnType("DATETIME2")
               .IsRequired();

            course
                .Property(c => c.Price)
                .IsRequired();                
        }
    }
}
