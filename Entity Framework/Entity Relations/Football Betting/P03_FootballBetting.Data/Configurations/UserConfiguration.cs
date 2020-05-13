namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user
                 .HasKey(u => u.UserId);

            user
                .Property(u => u.Username)
                .HasMaxLength(30)
                .IsRequired()
                .IsUnicode();

            user
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode();

            user
                .Property(u => u.Email)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode(false);

            user
                .Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode();

            user
                .Property(u => u.Balance)
                .IsRequired();
        }
    }
}
