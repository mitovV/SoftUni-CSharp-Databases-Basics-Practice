namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> student)
        {
            student.HasKey(s => s.StudentId);

            student
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();

            student
                .Property(s => s.PhoneNumber)
                .HasColumnType("CHAR(10)")
                .IsRequired(false);

            student
                .Property(s => s.RegisteredOn)
                .HasColumnType("DATETIME2")
                .IsRequired();

            student
                .Property(s => s.Birthday)
                .IsRequired(false);
        }
    }
}
