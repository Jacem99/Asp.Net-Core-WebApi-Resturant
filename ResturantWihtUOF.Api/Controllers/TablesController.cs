using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantWihtUOF.Core.Consts;
using ResturantWihtUOF.Core.Interfaces;
using ResturantWihtUOF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantWihtUOF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TablesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /* [HttpGet]
         [Authorize(Roles ="الزبون")]*/
        /* public string test()
         {
             var claims = User.Claims.First(c=>c.Type=="uid").Value;
             var test = User.Claims.First(c=>c.Type=="roleUser").Value;

             return test;
         }*/



        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Tables.FindByCriteria(m => m.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }
            

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.Tables.GetAll(o => o.Number, OrderBy.Ascending));


        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(Tables model)
        {

            if (!await _unitOfWork.Tables.CheckAny(t => t.Number == model.Number))
                return NotFound("Number Exist");

            var checkModel = await _unitOfWork.Tables.FindByCriteria(m => m.Id == model.Id);
            if (model.Id <= 0)
            {
                //Add 
                checkModel = await _unitOfWork.Tables.Add(model);
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");
                checkModel.Number = model.Number;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.Tables.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }
    }
}
