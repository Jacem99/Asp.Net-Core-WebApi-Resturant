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
    public class MealsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MealsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <=0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Meals.FindByCriteria(m => m.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.Meals.GetAll(o => o.Name, OrderBy.Ascending));

        [HttpGet("GetByIncludes")]
        public async Task<IActionResult> GetByIncludes() => Ok(await _unitOfWork.Meals.GetAll(new[] { "Ingredient" }, o => o.Name, OrderBy.Ascending));

        [HttpGet("GetByIdIngredient")]
        public async Task<IActionResult> GetByIdIngredient(int IngredientId)
        {
            if (!await _unitOfWork.Meals.CheckAny(m => m.IngredientId == IngredientId))
                return NotFound("Not Found");
            return Ok(await _unitOfWork.Meals.GetAll(m => m.IngredientId == IngredientId, null, o => o.Name, OrderBy.Ascending));
        }

        [HttpGet("GetByIdIngredientIncludes")]
        public async Task<IActionResult> GetByIdIngredientIncludes(int IngredientId)  {
            if (!await _unitOfWork.Meals.CheckAny(m => m.IngredientId == IngredientId))
                return NotFound("Not Found");
            return Ok(await _unitOfWork.Meals.GetAll(m => m.IngredientId == IngredientId, new[] { "Ingredient" }, o => o.Name, OrderBy.Ascending));
         }

        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(MealData model)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _unitOfWork.Ingredients.CheckAny(m => m.Id == model.IngredientId))
                return NotFound("Not Found Id RelationShip");

            var checkModel = await _unitOfWork.Meals.FindByCriteria(m => m.Id == model.Id);
           
            //Add 
            if(model.Id <= 0)
            {
                checkModel = await _unitOfWork.Meals.Add(
                    new Meal() { Name = model.Name , IngredientId = model.IngredientId , Price = model.Price });
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");

                checkModel.Name = model.Name;
                checkModel.Price = model.Price;
                checkModel.IngredientId = model.IngredientId;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpDelete("DeleteByName")]
        public async Task<IActionResult> DeleteByName(string Name)
        {
            if (Name == null)
                return BadRequest("Name Is Required");

            return await _unitOfWork.Meals.Remove(m => m.Name == Name) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.Meals.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }
    }
}
