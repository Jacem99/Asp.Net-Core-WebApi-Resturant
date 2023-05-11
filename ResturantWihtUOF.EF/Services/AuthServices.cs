using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ResturantWihtUOF.Core.Data;
using ResturantWihtUOF.Core.Interfaces;
using ResturantWihtUOF.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.EF.Services
{
    public class AuthServices : BaseRepository<ApplicationUser> , IAuthServirces
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<RolesResturant> _roleManager;
        private new readonly ApplicationDbContext _dbContext;
        private readonly JWT _jwt;

        public AuthServices(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt , ApplicationDbContext dbContext
            , RoleManager<RolesResturant> roleManager):base(dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _jwt = jwt.Value;
    }
        private async Task<JwtSecurityToken> createJwtToken(ApplicationUser user )
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            IdentityOptions _option = new IdentityOptions();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new Claim[]
            {
                new Claim ("uid" ,user.Id),
                new Claim(JwtRegisteredClaimNames.Sub , user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
               /* new Claim ("roleUser" ,roles.FirstOrDefault()),*/
               /* new Claim ("roles" ,roles.FirstOrDefault()),*/
            }
           .Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials :signingCredentials
                );
            return jwtSecurityToken;
        }
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already Registered!" };
            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is already registerd!" };

            if (await _dbContext.User.AnyAsync(p => p.PhoneNumber == model.Phone))
                return new AuthModel { Message = "Phone is already Exist!" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.Phone,

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description} , ";
                }
                return new AuthModel { Message = errors };
            }

            if(await _dbContext.User.CountAsync() == 1)
                await _userManager.AddToRoleAsync(user, "الأدمن");

            await _userManager.AddToRoleAsync(user, "الزبون");

            var jwtSecurityToken = await createJwtToken(user);
            return new AuthModel
            {
                UserName = user.UserName,
                Email = user.Email,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Expireson = jwtSecurityToken.ValidTo,
                Roles = await _userManager.GetRolesAsync(user),
            };
        }
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }
            var jwtSecurityToken = await createJwtToken(user);
            var roleList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Roles = roleList.ToList();
            authModel.Email = user.Email;
            authModel.Expireson = jwtSecurityToken.ValidTo;

            return authModel;
        }
        public async Task<string> AddRole(RoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.RoleExistsAsync(model.RoleName);

            if (user is null || !role)
                return "Invalid UserId or Role";

            if (await _userManager.IsInRoleAsync(user, model.RoleName))
                return "User already in role";

            if (await _userManager.IsInRoleAsync(user, "الأدمن"))
                return "The Admin have all role in site";
            
            if (model.RoleName=="الأدمن")
            {
                var rolesUser = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user , rolesUser);
            }
          return (await _userManager.AddToRoleAsync(user, model.RoleName)).Succeeded ? "" : "Some thing is Wrong";
        }
        public async Task<string> DeleteRoleUser(RoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            bool checkUserinRole = await _userManager.IsInRoleAsync(user, model.RoleName.Trim());
            RolesResturant role = await _roleManager.FindByNameAsync(model.RoleName.Trim());   

            if (user is null || role is null)
                return "Invalid UserId or Role";

            if (!checkUserinRole)
                return "User haven't this role";

         /*   if (model.RoleName == RolesUser.Admin && !await _dbContext.AnyAsync(r => r.RoleId == role.Id && r.UserId != user.Id))
                return "Can't Delete this role for user because he is the only in role Admin";*/

            await _userManager.RemoveFromRoleAsync(user, model.RoleName.Trim());
            return "";
        }
    }
}
