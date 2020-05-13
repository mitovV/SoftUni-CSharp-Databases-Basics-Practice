namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> playerStatistic)
        {
            playerStatistic
                 .HasKey(ps => new { ps.GameId, ps.PlayerId });

            playerStatistic
                .Property(ps => ps.ScoredGoals)
                .IsRequired();

            playerStatistic
                .Property(ps => ps.Assists)
                .IsRequired();

            playerStatistic
                .Property(ps => ps.MinutesPlayed)
                .IsRequired();

            playerStatistic
                .HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(ps => ps.PlayerId);

            playerStatistic
                .HasOne(ps => ps.Game)
                .WithMany(g => g.PlayerStatistics)
                .HasForeignKey(ps => ps.GameId);
        }
    }
}
