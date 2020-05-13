namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> game)
        {
            game
                 .HasKey(g => g.GameId);

            game
                .Property(g => g.HomeTeamGoals)
                .IsRequired();

            game.Property(g => g.AwayTeamGoals)
                .IsRequired();

            game
                .Property(g => g.DateTime)
                .IsRequired();

            game
                .Property(g => g.HomeTeamBetRate)
                .IsRequired();

            game
                .Property(g => g.AwayTeamBetRate)
                .IsRequired();

            game
                .Property(g => g.DrawBetRate)
                .IsRequired();

            game
                .Property(g => g.Result)
                .HasMaxLength(7)
                .IsRequired()
                .IsUnicode(false);

            game
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            game
                .HasOne(g => g.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
