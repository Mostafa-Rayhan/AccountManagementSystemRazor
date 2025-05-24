using AccountManagementSystem.Models.Account;
using AccountManagementSystem.Models.Voucher;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AccountManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSets if needed for any direct table access
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherDetail> VoucherDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<ChartOfAccount>()
                .HasOne(c => c.ParentAccount)
                .WithMany()
                .HasForeignKey(c => c.ParentAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; } = true;
    }
}
