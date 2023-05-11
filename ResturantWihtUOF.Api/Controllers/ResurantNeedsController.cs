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
    public class ResurantNeedsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ResurantNeedsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.ResturantNeeds.FindByCriteria(m => m.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.ResturantNeeds.GetAll(o => o.Name, OrderBy.Ascending));


        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(ResturantNeeds model)
        {
            if (model.Name is null)
                return BadRequest("Name Is Required");
            if (model.Kilo <= 0 && model.Quantity <= 0)
                return BadRequest("You Have to insert value in at leat one of Quantiy Or Kilo");

            if (!await _unitOfWork.ResturantNeeds.CheckAny(t => t.Name == model.Name))
                return NotFound("Name Exist");

            var checkModel = await _unitOfWork.ResturantNeeds.FindByCriteria(m => m.Id == model.Id);
            if (model.Id <= 0)
            {
                //Add 
                checkModel = await _unitOfWork.ResturantNeeds.Add(model);
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");
                checkModel.Name = model.Name;
                checkModel.Quantity = model.Quantity;
                checkModel.Kilo = model.Kilo;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.ResturantNeeds.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }
    }
}
