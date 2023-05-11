using ResturantWihtUOF.Core.Models;
using System;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Interfaces
{
       public interface IUnitOfWork : IDisposable
        {
         IAuthServirces User { get; }
         IBaseRepository<Resturant> Resturant { get; }
         IBaseRepository<Customers> Customers { get; }
         IBaseRepository<Delivering> Delivering { get; }
         IBaseRepository<Reservation> Reservation { get; }
         IBaseRepository<Kitchen> Kitchens { get; }
         IBaseRepository<Ingredient> Ingredients { get; }
         IBaseRepository<Meal> Meals { get; }
         IBaseRepository<Suppliers> Suppliers { get; }
         IBaseRepository<TypeOfOrder> TypeOfOrders { get; }
         IBaseRepository<TypeOfReservation> TypeOfReservation { get; }
         IBaseRepository<Tables> Tables { get; }
         IBaseRepository<Order> Orders { get; }
         IBaseRepository<Fatora> Fatoras { get; }
         IBaseRepository<ResturantNeeds> ResturantNeeds { get; }
         IBaseRepository<SupplierIngredientProvides> SupplierIngredientProvides { get; }
         IBaseRepository<Sections> Sections { get; }
         IBaseRepository<RolesResturant> RolesResturants { get; }
         IBaseRepository<WorkingPeroid> WorkingPeroids { get; }
         IBaseRepository<Address> Address { get; }
         IBaseRepository<WorkingPeriodUsers> WorkingPeriodUsers { get; }
        Task<int> Complete();
    }
}
