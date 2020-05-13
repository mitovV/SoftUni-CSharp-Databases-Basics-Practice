namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> player)
        {
            player
                .HasKey(p => p.PlayerId);

            player
                .Property(p => p.Name)
                .HasMaxLength(30)
                .IsRequired()
                .IsUnicode();

            player
                .Property(p => p.SquadNumber)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false);

            player
                .Property(p => p.IsInjured)
                .IsRequired();

            player
                .HasOne(p => p.Position)
                .WithMany(po => po.Players)
                .HasForeignKey(p => p.PositionId);

            player
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);
        }
    }
}
