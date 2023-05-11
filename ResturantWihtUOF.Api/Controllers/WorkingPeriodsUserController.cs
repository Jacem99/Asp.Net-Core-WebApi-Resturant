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
    public class WorkingPeriodsUserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkingPeriodsUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetByWorkerId")]
        public async Task<IActionResult> GetById(string WorkerId)
        {
            if (WorkerId is null)
                return BadRequest("SupplierId is Required");
            return Ok(await _unitOfWork.WorkingPeriodUsers.GetAll(s => s.WorkerId == WorkerId));
        }

        [HttpGet("GetByWorkerIdInclude")]
        public async Task<IActionResult> GetByWorkerIdInclude(string WorkerId)
        {
            if (WorkerId is null)
                return BadRequest("SupplierId is Required");
            return Ok(await _unitOfWork.WorkingPeriodUsers.GetAll(s => s.WorkerId == WorkerId, new string[] { "Peroid", "Worker" }, s => s.WorkerId, OrderBy.Ascending));
        }

        [HttpGet("GetByPeriodNameInclude")]
        public async Task<IActionResult> GetByPeriodNameInclude(string PeriodName)
        {
            if (PeriodName is null)
                return BadRequest("SupplierId is Required");
            return Ok(await _unitOfWork.WorkingPeriodUsers.GetAll(s => s.Peroid.Period == PeriodName.Trim(), new string[] { "Peroid", "Worker" }, s => s.WorkerId, OrderBy.Ascending));
        }
        [HttpGet("GetByPeroidId")]
        public async Task<IActionResult> GetByIngerdientId(int PeroidId)
        {
            if (PeroidId <= 0)
                return BadRequest("SupplierId is Required");
            return Ok(await _unitOfWork.WorkingPeriodUsers.GetAll(s => s.PeroidId == PeroidId));
        }
        [HttpGet("GetByPeroidIdInclude")]
        public async Task<IActionResult> GetByIngerdientInclude(int PeroidId)
        {
            if (PeroidId <= 0)
                return BadRequest("SupplierId is Required");
            return Ok(await _unitOfWork.WorkingPeriodUsers.GetAll(s => s.PeroidId == PeroidId, new string[] { "Peroid", "Worker" }, s => s.WorkerId, OrderBy.Ascending));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.WorkingPeriodUsers.GetAll(s => s.WorkerId));
        [HttpGet("GetAllInclude")]
        public async Task<IActionResult> GetAllInclude() => Ok(await _unitOfWork.WorkingPeriodUsers.GetAll(new string[] { "Period", "Worker" }, s => s.WorkerId, OrderBy.Ascending));

        private async Task<string> CheckModel(WorkingPeriodUsers model)
        {
            if (!await _unitOfWork.User.CheckAny(t => t.Id == model.WorkerId))
                return "UserId isn't Exist";

            if (!await _unitOfWork.WorkingPeroids.CheckAny(t => t.Id == model.PeroidId))
                return "PeroidId isn't Exist";

            if (await _unitOfWork.WorkingPeriodUsers.CheckAny(w => w.WorkerId == model.WorkerId && w.PeroidId == model.PeroidId))
               return "Worker already in this period";

            return "Done";
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add_Update(WorkingPeriodUsers model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var checkModel = await CheckModel(model);

            if(checkModel != "Done")
                return BadRequest(checkModel);

            var Model = new WorkingPeriodUsers();

            bool checkWorker = await _unitOfWork.WorkingPeriodUsers.CheckAny(t => t.WorkerId == model.WorkerId);

            if( !checkWorker || await _unitOfWork.WorkingPeriodUsers.CheckAny(w=> w.WorkerId == model.WorkerId && w.PeroidId != model.PeroidId))
            {
                
                Model = await _unitOfWork.WorkingPeriodUsers.Add(model);
                await _unitOfWork.Complete();
            }

            return Ok(checkModel);
        } 
        
        [HttpPut("Update")]
        public async Task<IActionResult> Update(WorkingPeriodUsers model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var checkModel = await CheckModel(model);

            if(checkModel != "Done")
                return BadRequest(checkModel);

            if (!await _unitOfWork.WorkingPeriodUsers.CheckAny(t => t.WorkerId == model.WorkerId))
                return NotFound("WorkerId isn't Found"); 

            var checkCriterai = await _unitOfWork.WorkingPeriodUsers.FindByCriteria(w => w.WorkerId == model.WorkerId && w.PeroidId != model.PeroidId);

            if (checkCriterai is not null )
            {
                checkCriterai.PeroidId = model.PeroidId;
                checkCriterai.WorkerId = model.WorkerId;
                await _unitOfWork.Complete();
            }
            return Ok(checkCriterai);
        } 
       
        [HttpDelete("DeleteAllRangeByWorker")]
        public async Task<IActionResult> DeleteRangeByWorker(string WorkerId)
        {
            if (WorkerId is null)
                return BadRequest("Id Is Required");
            return await _unitOfWork.WorkingPeriodUsers.RemoveRange(m => m.WorkerId == WorkerId) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }


        [HttpDelete("DeleteByPeriod")]
        public async Task<IActionResult> DeleteById(WorkingPeriodUsers model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _unitOfWork.WorkingPeriodUsers.Remove(m => m.WorkerId == model.WorkerId && m.PeroidId == model.PeroidId) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }
    }
}
