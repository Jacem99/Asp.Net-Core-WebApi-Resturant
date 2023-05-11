
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ResturantWihtUOF.Core.Data;
using ResturantWihtUOF.Core.Interfaces;
using ResturantWihtUOF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.EF.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<RolesResturant> _roleManager;
        private readonly JWT _jwt;
        public IAuthServirces User { get; private set; }
        public IBaseRepository<Resturant> Resturant { get; private set; }
        public IBaseRepository<Customers> Customers { get; private set; }
        public IBaseRepository<Delivering> Delivering { get; private set; }
        public IBaseRepository<Reservation> Reservation { get; private set; }
        public IBaseRepository<Kitchen> Kitchens { get; private set; }
        public IBaseRepository<Ingredient> Ingredients { get; private set; }
        public IBaseRepository<Meal> Meals { get; private set; }
        public IBaseRepository<Suppliers> Suppliers { get; private set; }
        public IBaseRepository<TypeOfOrder> TypeOfOrders { get; private set; }
        public IBaseRepository<TypeOfReservation> TypeOfReservation { get; private set; }
        public IBaseRepository<Tables> Tables { get; private set; }
        public IBaseRepository<Order> Orders { get; private set; }
        public IBaseRepository<Fatora> Fatoras { get; private set; }
        public IBaseRepository<ResturantNeeds> ResturantNeeds { get; private set; }
        public IBaseRepository<SupplierIngredientProvides> SupplierIngredientProvides { get; private set; }
        public IBaseRepository<Sections> Sections { get; private set; }
        public IBaseRepository<RolesResturant> RolesResturants { get; private set; }
        public IBaseRepository<WorkingPeroid> WorkingPeroids { get; private set; }
        public IBaseRepository<Address> Address { get; private set; }
        public IBaseRepository<WorkingPeriodUsers> WorkingPeriodUsers { get; private set; }
        public UnitOfWork(UserManager<ApplicationUser> userManager, RoleManager<RolesResturant> roleManager, ApplicationDbContext dbContext, IOptions<JWT> jwt )
        {
           _dbContext = dbContext;
           _userManager = userManager;
           _roleManager = roleManager;
           _jwt = jwt.Value;

            User = new AuthServices(_userManager , jwt , _dbContext , _roleManager);
            Resturant = new BaseRepository<Resturant>(_dbContext);
            Customers = new BaseRepository<Customers>(_dbContext);
            Ingredients = new BaseRepository<Ingredient>(_dbContext);
            Kitchens = new BaseRepository<Kitchen>(_dbContext);
            Meals = new BaseRepository<Meal>(_dbContext);
            Suppliers = new BaseRepository<Suppliers>(_dbContext);
            Tables = new BaseRepository<Tables>(_dbContext);
            Orders = new BaseRepository<Order>(_dbContext);
            Fatoras = new BaseRepository<Fatora>(_dbContext);
            TypeOfReservation = new BaseRepository<TypeOfReservation>(_dbContext);
            TypeOfOrders = new BaseRepository<TypeOfOrder>(_dbContext);
            ResturantNeeds = new BaseRepository<ResturantNeeds>(_dbContext);
            SupplierIngredientProvides = new BaseRepository<SupplierIngredientProvides>(_dbContext);
            Sections = new BaseRepository<Sections>(_dbContext);
            RolesResturants = new BaseRepository<RolesResturant>(_dbContext);
            WorkingPeroids = new BaseRepository<WorkingPeroid>(_dbContext);
            WorkingPeriodUsers = new BaseRepository<WorkingPeriodUsers>(_dbContext);
            Delivering = new BaseRepository<Delivering>(_dbContext);
            Reservation = new BaseRepository<Reservation>(_dbContext);
            Address = new BaseRepository<Address>(_dbContext);
           
        }

        public async Task<int> Complete()
        {
           return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
