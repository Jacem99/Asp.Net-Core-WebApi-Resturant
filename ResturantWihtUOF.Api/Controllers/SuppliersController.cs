using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class SuppliersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public SuppliersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetBySupplierId(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Suppliers.FindByCriteria(m => m.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        [HttpGet("GetBySupplierId")]
        public async Task<IActionResult> GetBySupplierId(string SupplierId)
        {
            if (SupplierId is null)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Suppliers.FindByCriteria(m => m.SupplierId == SupplierId);
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        [HttpGet("GetByCheifId")]
        public async Task<IActionResult> GetByCheifId(string CheifId)
        {
            if (CheifId is null)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Suppliers.FindByCriteria(m => m.ChiefId == CheifId);
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.Suppliers.GetAll());

        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(Suppliers model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _unitOfWork.User.CheckAny(t => t.Id == model.ChiefId))
                return NotFound("ChiefId isn't Exist");

            if (!await _unitOfWork.User.CheckAny(t => t.Id == model.SupplierId))
                return NotFound("SupplierId isn't Exist");

            var checkModel = await _unitOfWork.Suppliers.FindByCriteria(m => m.Id == model.Id);

            if (model.Id <= 0)
            {
                //Add 
                checkModel = await _unitOfWork.Suppliers.Add(model);
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");

                checkModel.SupplierId = model.SupplierId;
                checkModel.ChiefId = model.ChiefId;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.Suppliers.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }

        [HttpDelete("DeleteBySupplierId")]
        public async Task<IActionResult> DeleteBySupplierId(string SupplierId)
        {
            if (SupplierId is null)
                return BadRequest("Id Is Required");

            return await _unitOfWork.Suppliers.Remove(m => m.SupplierId == SupplierId) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }
    }
}
