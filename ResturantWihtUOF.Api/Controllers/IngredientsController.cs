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
    public class IngredientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public IngredientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Ingredients.FindByCriteria(m => m.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }
            

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.Ingredients.GetAll());

        [HttpGet("GetByRelationShip")]
        public async Task<IActionResult> GetByRelationShip() => Ok(await _unitOfWork.Ingredients.GetAll(new[] { "Cheif" }, o => o.Name, OrderBy.Ascending));

        [HttpGet("GetByIdRelationShip")]
        public async Task<IActionResult> GetByIdRelationShip(string CheifId)
        {
            if (!await _unitOfWork.Ingredients.CheckAny(m => m.CheifId == CheifId))
                return NotFound("Not Found");
            return Ok(await _unitOfWork.Ingredients.GetAll(m => m.CheifId == CheifId, new[] { "Cheif" }, o => o.Name, OrderBy.Ascending));
        }

        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(IngredientData model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(model.CheifId is not null)
                if (!await _unitOfWork.User.CheckAny(m => m.Id == model.CheifId))
                    return NotFound("Not Found Id for CheifId RelationShip");

            if (model.Kilo <= 0 && model.Litter <= 0)
                return BadRequest("No Quntity To Add");

            var checkModel = await _unitOfWork.Ingredients.FindByCriteria(m => m.Id == model.Id);

            Ingredient testModel = new Ingredient() { };

            if (checkModel.Kilo is not null)
                checkModel.Kilo = model.Kilo;

            if (checkModel.Litter is not null)
                checkModel.Litter = model.Litter;

            if (model.Id <= 0)
            {
                //Add 
                checkModel = await _unitOfWork.Ingredients.Add(
                    new Ingredient() { Name = model.Name, Description = model.Description,
                        Kilo = checkModel.Kilo , Litter = checkModel.Litter ,
                        CheifId = model.CheifId });
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");

                checkModel.Name = model.Name;
                checkModel.Description = model.Description;
                checkModel.Litter = model.Litter;
                checkModel.Kilo = model.Kilo;
                checkModel.CheifId = model.CheifId;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity(IngredientData model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.Id <= 0)
                return NotFound("No Id to select the Ingredient");

            Ingredient checkModel = await _unitOfWork.Ingredients.FindByCriteria(m => m.Id == model.Id);

            if(checkModel is null)
                return NotFound("Not Found Ingredient To Edit");

            checkModel.Litter = model.Litter;
            checkModel.Kilo = model.Kilo;

            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpDelete("DeleteByName")]
        public async Task<IActionResult> DeleteByName(string Name)
        {
            if (Name == null)
                return BadRequest("Name Is Required");

            return await _unitOfWork.Ingredients.Remove(m => m.Name == Name) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.Ingredients.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }
    }
}
