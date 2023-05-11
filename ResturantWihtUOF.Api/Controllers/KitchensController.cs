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
    public class KitchensController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public KitchensController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Parameter Id is Required");
            var model = await _unitOfWork.Kitchens.FindByCriteria(k => k.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        [HttpGet("GetByIngredientName")]
        public async Task<IActionResult> GetByIngredientName(string IngredientName)
        {
            if (IngredientName is null)
                return BadRequest("Parameter Name is Required");

            var model = await _unitOfWork.Kitchens.GetAll(k => k.Ingredient.Name.Contains(IngredientName), new string[] { "Ingredient", "Meal", "TypeOfOrder" } ,k => k.Time,OrderBy.Ascending  );
            return model is null ? NotFound("Not Found") : Ok(model);
        }
        [HttpGet("GetByTypeOfOrderName")]
        public async Task<IActionResult> GetByTypeOfOrderName(string TypeOfOrderName)
        {
            if (TypeOfOrderName is null)
                return BadRequest("Parameter Name is Required");
            var model = await _unitOfWork.Kitchens.GetAll(k => k.TypeOfOrder.TypeOrder.Contains(TypeOfOrderName), new string[] { "Ingredient", "Meal", "TypeOfOrder" }, k => k.Time, OrderBy.Ascending);
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        ///////////////////

        [HttpGet("GetByIngredientId")]
        public async Task<IActionResult> GetByIngredientId(int IngredientId)
        {
            if (IngredientId <= 0)
                return BadRequest("IngredientId is Required");

            var model = await _unitOfWork.Kitchens.GetAll(k => k.Ingredient.Id == IngredientId, new string[] { "Ingredient", "Meal", "TypeOfOrder" }, k => k.Time, OrderBy.Ascending);
            return model is null ? NotFound("Not Found") : Ok(model);
        }
        [HttpGet("GetByTypeOfOrderId")]
        public async Task<IActionResult> GetByTypeOfOrderId(int TypeOfOrderId)
        {
            if (TypeOfOrderId <= 0)
                return BadRequest("TypeOfOrderId is Required");

            var model = await _unitOfWork.Kitchens.GetAll(k => k.TypeOfOrder.Id == TypeOfOrderId, new string[] { "Ingredient", "Meal", "TypeOfOrder" }, k => k.Time, OrderBy.Ascending);
            return model is null ? NotFound("Not Found") : Ok(model);
        }
      
        //////////////

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>  Ok(await _unitOfWork.Kitchens.GetAll(k => k.Time , OrderBy.Ascending));

        [HttpGet("GetAllByInclude")]
        public async Task<IActionResult> GetAllByInclude() =>
            Ok(await _unitOfWork.Kitchens.GetAll(new string[] { "Ingredient", "Meal" , "TypeOfOrder" } , k=> k.Time , OrderBy.Ascending));


        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(Kitchen model)
        {

            if (!await _unitOfWork.Ingredients.CheckAny(t => t.Id == model.IngredientId))
                return NotFound("IngredientId isn't Exist");

            if (!await _unitOfWork.TypeOfOrders.CheckAny(t => t.Id == model.TypeOfOrderId))
                return NotFound("TypeOfOrderId isn't Exist");

            if (!await _unitOfWork.Meals.CheckAny(t => t.Id == model.MealId))
                return NotFound("MealId isn't Exist");

            var checkModel = await _unitOfWork.Kitchens.FindByCriteria(m => m.Id == model.Id);

            if (model.Id <= 0)
            {
                //Add 
                checkModel = await _unitOfWork.Kitchens.Add(model);
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");

                checkModel.IngredientId = model.IngredientId;
                checkModel.MealId = model.MealId;
                checkModel.TypeOfOrderId = model.TypeOfOrderId;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.Kitchens.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }

    }
}
