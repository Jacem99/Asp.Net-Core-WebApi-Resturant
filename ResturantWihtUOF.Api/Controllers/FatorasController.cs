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
    public class FatorasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FatorasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Fatoras.GetAll(m => m.FatoraId == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        [HttpGet("GetByCacheerId")]
        public async Task<IActionResult> GetByCacheerId(string CacheerId)
        {
            if (CacheerId is null)
                return BadRequest("Id Is Required");
            
            return Ok(await _unitOfWork.Fatoras.GetAll(m => m.CasheerId == CacheerId));
        }

        [HttpGet("GetByCacheerInclude")]
        public async Task<IActionResult> GetByCacheerInclude(string CacheerId)
        {
            if (CacheerId is null)
                return BadRequest("Id Is Required");

            return Ok(await _unitOfWork.Fatoras.GetAll(m => m.CasheerId == CacheerId, new string[] { "Casheer", "Order" }, f => f.Casheer.Id, OrderBy.Ascending));
        }

        [HttpGet("GetByOrderId")]
        public async Task<IActionResult> GetByOrderId(int OrderId)
        {
            if (OrderId <= 0)
                return BadRequest("OrderId Is Required");

            return Ok(await _unitOfWork.Fatoras.GetAll(m => m.OrderId == OrderId));
        }

        [HttpGet("GetByOrderInclude")]
        public async Task<IActionResult> GetByOrderInclude(int OrderId)
        {
            if (OrderId <= 0)
                return BadRequest("Id Is Required");

            return Ok(await _unitOfWork.Fatoras.GetAll(m => m.OrderId == OrderId, new string[] { "Casheer", "Order" }, f => f.CasheerId, OrderBy.Ascending));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.Fatoras.GetAll());
        [HttpGet("GetAllInclude")]
        public async Task<IActionResult> GetAllInclude() => 
            Ok(await _unitOfWork.Fatoras.GetAll(new string[] { "Casheer", "Order" }, f => f.CasheerId, OrderBy.Ascending));

        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(Fatora model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _unitOfWork.User.CheckAny(t => t.Id == model.CasheerId))
                return NotFound("CasheerId isn't Exist");

            if (!await _unitOfWork.Orders.CheckAny(t => t.Id == model.OrderId))
                return NotFound("OrderId isn't Exist");

            var checkModel = await _unitOfWork.Fatoras.FindByCriteria(m => m.FatoraId == model.FatoraId);

            if (model.FatoraId <= 0)
            {
                //Add 
                checkModel = await _unitOfWork.Fatoras.Add(model);
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");

                checkModel.CasheerId = model.CasheerId;
                checkModel.OrderId = model.OrderId;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int FatoraId)
        {
            if (FatoraId <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.Fatoras.Remove(m => m.FatoraId == FatoraId) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }

    }
}
