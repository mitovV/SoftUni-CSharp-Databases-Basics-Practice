namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> position)
        {
            position
                .HasKey(p => p.PositionId);

            position
                .Property(p => p.Name)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode(false);
        }
    }
}
