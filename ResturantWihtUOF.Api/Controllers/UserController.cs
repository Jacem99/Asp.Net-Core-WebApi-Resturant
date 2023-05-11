using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResturantWihtUOF.Core.Consts;
using ResturantWihtUOF.Core.Data;
using ResturantWihtUOF.Core.Interfaces;

using System;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    { 
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IAuthServirces authServirces , IUnitOfWork unitOfWork)
        {
          
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string Id)
        {
            if (Id is null)
                return BadRequest("Id Is Required");

            var user = await _unitOfWork.User.FindByCriteria(u => u.Id == Id.Trim());
            if (user == null)
                 return NotFound("Not Found");
            return Ok(user);
        }

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetByEmail(string Email)
        {
            if (Email is null)
                return BadRequest("Email Is Required");

            var user = await _unitOfWork.User.FindByCriteria(u => u.Email == Email.Trim());
            if (user == null)
                return NotFound("Not Found");
            return Ok(user);
        }

        [HttpGet("GetByUserName")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            if (userName is null)
                return BadRequest("UserName Is Required");


            var user = await _unitOfWork.User.FindByCriteria(u => u.UserName == userName.ToLower().Trim());
            if (user == null)
                return NotFound("Not Found");
            return Ok(user);
        }

        [HttpGet("GetByPhone")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            if (phone is null)
                return BadRequest("phone Is Required");

            var user = await _unitOfWork.User.FindByCriteria(u => u.Id == phone.Trim());
            if (user == null)
                return NotFound("Not Found");
            return Ok(user);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _unitOfWork.User.GetAll(null, c => c.UserName, OrderBy.Ascending));

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(string id)
        {
            if (id is null)
                return BadRequest("No Parameter");

            if (await _unitOfWork.User.Remove(u => u.Id == id) == 0)
                return NotFound("Not Found");

            await _unitOfWork.Complete();
            return Ok("Deleted");
        }

        [HttpGet("Count")]
        public async Task<IActionResult> Count() => Ok(await _unitOfWork.User.Count());

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.User.RegisterAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _unitOfWork.User.GetTokenAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("addRole")]
        [Authorize(Roles = RolesUser.Admin)]
        public async Task<IActionResult> AddRole([FromBody] RoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string role = await _unitOfWork.User.AddRole(model);

            if (!String.IsNullOrEmpty(role))
                return BadRequest(role);
            return Ok(model);
        }

        [HttpPost("DeleteRoleUser")]
        [Authorize(Roles="الزبون,الأدمن,الكاشير")]
        public async Task<IActionResult> DeleteRoleUser([FromBody] RoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string role = await _unitOfWork.User.DeleteRoleUser(model);
            if (!String.IsNullOrEmpty(role))
                return BadRequest(role);
            return Ok(model);
        }
    }
}
