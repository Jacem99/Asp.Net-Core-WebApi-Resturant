using ResturantWihtUOF.Core.Data;
using ResturantWihtUOF.Core.Interfaces;
using ResturantWihtUOF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Core.Interfaces
{
   public interface IAuthServirces : IBaseRepository<ApplicationUser>
    {
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<string> AddRole(RoleModel model);
        Task<string> DeleteRoleUser(RoleModel model);
    }
}
