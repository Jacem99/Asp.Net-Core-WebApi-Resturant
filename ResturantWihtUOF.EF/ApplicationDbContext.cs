using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResturantWihtUOF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.EF
{
   public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
      

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ///////////// ApplicationUser /////////////////
            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Suppliers)
                .WithOne(s => s.Supplier)
                .HasForeignKey(s => s.SupplierId);

            builder.Entity<ApplicationUser>()
             .HasMany(a => a.Cheifs)
             .WithOne(s => s.Chief)
             .HasForeignKey(s => s.ChiefId);

            ///////////// SupplierIngredientProvides /////////////////

            builder.Entity<SupplierIngredientProvides>().HasKey(s => s.Id);

            builder.Entity<SupplierIngredientProvides>()
            .HasOne(a => a.Supplier)
            .WithMany(s => s.SupplierIngredientProvides)
            .HasForeignKey(s => s.SupplierId);

            builder.Entity<SupplierIngredientProvides>()
                  .HasOne(a => a.Ingredient)
                  .WithMany(s => s.SupplierIngredientProvides)
                  .HasForeignKey(s => s.IngredientId);


            ///////////// SupplierIngredientProvides /////////////////
            builder.Entity<WorkingPeriodUsers>().HasKey( w => new { w.WorkerId , w.PeroidId });

            builder.Entity<WorkingPeriodUsers>()
            .HasOne(a => a.Worker)
            .WithMany(s => s.WorkingPeriodUsers)
            .HasForeignKey(s => s.WorkerId);

            builder.Entity<WorkingPeriodUsers>()
                  .HasOne(a => a.Peroid)
                  .WithMany(s => s.WorkingPeriodUsers)
                  .HasForeignKey(s => s.PeroidId);






        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Delivering> Delivering { get; set; }
        public DbSet<Fatora> Fatora { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Kitchen> Kitchen { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Resturant> Resturant { get; set; }
        public DbSet<ResturantNeeds> ResturantNeeds { get; set; }
        public DbSet<RolesResturant> RolesResturant { get; set; }
        public DbSet<Sections> Sections { get; set; }
        public DbSet<SupplierIngredientProvides> SupplierIngredientProvides { get; set; }
        public DbSet<Tables> Tables { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<TypeOfOrder> TypeOfOrder { get; set; }
        public DbSet<TypeOfReservation> TypeOfReservation { get; set; }
        public DbSet<WorkingPeriodUsers> WorkingPeriodUsers { get; set; }
        public DbSet<WorkingPeroid> WorkingPeroid { get; set; }
    }
}
