namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=CODINGMANIA\SQLEXPRESS;Database=SalesDB;Integrated Security=true;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity
                .HasKey(p => p.ProductId);

                entity
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

                entity
                .Property(p => p.Description)
                .HasMaxLength(250)
                .IsUnicode(true)
                .HasDefaultValue("No description");

                entity
                .Property(p => p.Quantity)
                .IsRequired();

                entity
                .Property(p => p.Price)
                .IsRequired();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity
                .HasKey(c => c.CustomerId);

                entity
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode();

                entity
                .Property(c => c.Email)
                .HasMaxLength(80)
                .IsRequired()
                .IsUnicode(false);

                entity
                .Property(c => c.CreditCardNumber)
                .IsRequired()
                .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(s => s.StoreId);

                entity
                .Property(s => s.Name)
                .HasMaxLength(80)
                .IsRequired()
                .IsUnicode();
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(s => s.SaleId);

                entity
                .Property(s => s.Date)
                .IsRequired()
                .HasColumnType("DATETIME2")
                .HasDefaultValueSql("GETDATE()");

                entity
                .HasOne(s => s.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductId);

                entity
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

                entity
                .HasOne(s => s.Store)
                .WithMany(st => st.Sales)
                .HasForeignKey(s => s.StoreId);
            });
        }
    }
}
