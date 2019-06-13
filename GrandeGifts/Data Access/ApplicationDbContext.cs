//Added namespaces:
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GrandeGifts.Models;

namespace GrandeGifts.Data_Access
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Address> TblAddresses { get; set; }
        public DbSet<Hamper> TblHampers { get; set; }
        public DbSet<Category> TblCategories { get; set; }

        /*
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        */

        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            //Laptop:
            //option.UseSqlServer(@"Server=DESKTOP-SVPTAKF;Database=GrandeGifts;Trusted_Connection=True;MultipleActiveResultSets=true");
            //TAFE:
            option.UseSqlServer(@"Server=PC10-12;Database=GrandeGifts;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder B)
        {
            base.OnModelCreating(B);

            /*
            B.Entity<HamperProduct>()
                .HasKey(x => new { x.HamperId, x.ProductId });

            B.Entity<HamperProduct>()
                .HasOne(x => x.Hamper)
                .WithMany(y => y.Products)
                .HasForeignKey(z => z.HamperId);

            B.Entity<HamperProduct>()
                    .HasOne(x => x.Product)
                    .WithMany(y => y.Hampers)
                    .HasForeignKey(z => z.ProductId);
            */
        }
    }
}
