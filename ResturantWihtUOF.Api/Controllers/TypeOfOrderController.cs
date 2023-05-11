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
    public class TypeOfOrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TypeOfOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.TypeOfOrders.FindByCriteria(m => m.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.TypeOfOrders.GetAll(o => o.TypeOrder, OrderBy.Ascending));

        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(TypeOfOrder model)
        {
            if (!await _unitOfWork.TypeOfOrders.CheckAny(t => t.TypeOrder == model.TypeOrder))
                return NotFound("TypeOrder Exist");

            var checkModel = await _unitOfWork.TypeOfOrders.FindByCriteria(m => m.Id == model.Id);
            if (model.Id <= 0)
            {
                //Add 
                checkModel = await _unitOfWork.TypeOfOrders.Add(model);
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");
                checkModel.TypeOrder = model.TypeOrder;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.TypeOfOrders.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }
    }
}
