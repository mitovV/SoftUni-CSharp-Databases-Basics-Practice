namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=CODINGMANIA\SQLEXPRESS;Database=HospitalDatabase;Integrated Security=true;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Diagnose>(entity =>
            {
                entity.HasKey(d => d.DiagnoseId);

                entity
                .Property(d => d.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

                entity
                .Property(d => d.Comments)
                .HasMaxLength(250)
                .IsUnicode();

                entity
                .HasOne(d => d.Patient)
                .WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientId);
            });

            modelBuilder
                .Entity<Medicament>(entity =>
            {
                entity.HasKey(m => m.MedicamentId);

                entity
                .Property(m => m.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();
            });

            modelBuilder
                .Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.PatientId);

                entity
                .Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

                entity
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

                entity
                .Property(p => p.Address)
                .HasMaxLength(250)
                .IsRequired()
                .IsUnicode();

                entity
                .Property(p => p.Email)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired();

                entity
                .Property(p => p.HasInsurance)
                .IsRequired();
            });

            modelBuilder
                .Entity<PatientMedicament>(entity =>
                {
                    entity.HasKey(pm => new { pm.PatientId, pm.MedicamentId });

                    entity.HasOne(pm => pm.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(pm => pm.PatientId);

                    entity.HasOne(pm => pm.Medicament)
                    .WithMany(pm => pm.Prescriptions)
                    .HasForeignKey(pm => pm.MedicamentId);
                });

            modelBuilder
                .Entity<Visitation>(entity =>
                {
                    entity
                    .HasKey(v => v.VisitationId);

                    entity
                    .Property(v => v.Date)
                    .IsRequired();

                    entity
                    .Property(v => v.Comments)
                    .HasMaxLength(250)
                    .IsUnicode();

                    entity
                    .HasOne(v => v.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(v => v.PatientId);

                    entity
                    .HasOne(v => v.Doctor)
                    .WithMany(d => d.Visitations)
                    .HasForeignKey(v => v.DoctorId);
                });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity
                .HasKey(d => d.DoctorId);

                entity
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();

                entity
                .Property(d => d.Specialty)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();
            });
        }
    }
}