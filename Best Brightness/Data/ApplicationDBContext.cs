using BestBrightness.Data;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }
     // Add this line

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure sequences
        modelBuilder.HasSequence<long>("BarcodeSequence", schema: "dbo")
            .StartsAt(6005134000)
            .IncrementsBy(1);

        modelBuilder.HasSequence<int>("EmployeeIdSequence", schema: "dbo")
            .StartsAt(444000)
            .IncrementsBy(1);

        modelBuilder.HasSequence<int>("AdminIdSequence", schema: "dbo")
            .StartsAt(333000)
            .IncrementsBy(1);

        // Use sequences in models
        modelBuilder.Entity<Product>()
            .Property(p => p.Barcode)
            .HasDefaultValueSql("NEXT VALUE FOR dbo.BarcodeSequence");

        modelBuilder.Entity<Employee>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEXT VALUE FOR dbo.EmployeeIdSequence");

        modelBuilder.Entity<Admin>()
            .Property(a => a.Id)
            .HasDefaultValueSql("NEXT VALUE FOR dbo.AdminIdSequence");

        // Configure relationships
        modelBuilder.Entity<SaleItem>()
            .HasOne(si => si.Product)
            .WithMany(p => p.SaleItems)
            .HasForeignKey(si => si.ProductId)
            .HasPrincipalKey(p => p.Barcode);

        modelBuilder.Entity<SaleItem>()
            .HasOne(si => si.Sale)
            .WithMany(s => s.SaleItems)
            .HasForeignKey(si => si.SaleId);

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Employee)
            .WithMany(e => e.Sales)
            .HasForeignKey(s => s.EmployeeId);

       
    }
}
