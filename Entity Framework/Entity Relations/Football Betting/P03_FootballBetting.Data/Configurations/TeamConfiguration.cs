namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> team)
        {
            team
                 .HasKey(t => t.TeamId);

            team
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode();

            team
                .Property(t => t.LogoUrl)
                .HasMaxLength(300)
                .IsRequired(false)
                .IsUnicode(false);

            team
                .Property(t => t.Initials)
                .HasMaxLength(3)
                .IsRequired(true)
                .IsUnicode(true);

            team
                .Property(t => t.Budget)
                .IsRequired();

            team
                .HasOne(t => t.Town)
                .WithMany(town => town.Teams)
                .HasForeignKey(t => t.TownId);

            team
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(pkc => pkc.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            team
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(skc => skc.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
