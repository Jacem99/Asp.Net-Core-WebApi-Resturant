using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantWihtUOF.Core.Consts;
using ResturantWihtUOF.Core.Data;
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
    public class TypeOfReservationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TypeOfReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.TypeOfReservation.FindByCriteria(m => m.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.TypeOfReservation.GetAll(o => o.TypeReservation, OrderBy.Ascending));

        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(TypeOfReservationData model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool checkType = await _unitOfWork.TypeOfReservation.CheckAny(t => t.TypeReservation == model.TypeReservation);
              
            var checkModel = await _unitOfWork.TypeOfReservation.FindByCriteria(m => m.Id == model.Id);
            if (model.Id <= 0)
            { 
                //Add 
                if(checkType)
                    return BadRequest("TypeReservation Exist");

                checkModel = await _unitOfWork.TypeOfReservation.Add(
                    new TypeOfReservation()
                    {
                        TypeReservation = model.TypeReservation
                    }) ;
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");

                if (checkType && checkModel.TypeReservation != model.TypeReservation)
                    return BadRequest("TypeReservation Exist");

                checkModel.TypeReservation = model.TypeReservation;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.TypeOfReservation.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }
    }
}

