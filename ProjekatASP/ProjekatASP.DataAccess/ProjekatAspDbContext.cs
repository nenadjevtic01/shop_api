using Microsoft.EntityFrameworkCore;
using ProjekatASP.DataAccess.Configurations;
using ProjekatASP.Domain;

namespace ProjekatASP.DataAccess
{
    public class ProjekatAspDbContext : DbContext
    {
        public ProjekatAspDbContext(DbContextOptions options = null) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-I2GGQMV\\MSSQL22;Initial Catalog=AspProjekat;Integrated Security=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<ProductSize>().HasKey(x => new { x.ProductId, x.SizeId });
            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UseCaseId, x.UserId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set;}
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptItem> ReceiptsItems { get; set;}
        public DbSet<Size> Sizes { get; set; }
        public DbSet<UseCase> UseCases { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
    }
}