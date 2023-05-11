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
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /////// Get By Id //////

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Orders.FindByCriteria(m => m.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }
        [HttpGet("GetByIdInclude")]
        public async Task<IActionResult> GetByIdInclude(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Orders.FindByCriteriaInclude(m => m.Id == Id,new[] {"Meal", "Ingredient", "TypeOfOrder"  });
            return model is null ? NotFound("Not Found") : Ok(model);
        }


        /////// Get Order //////

        [HttpGet("GetByTypeOrder")]
        public async Task<IActionResult> GetByTypeOrderName() => Ok(
                await _unitOfWork.Orders.GetAll(
                new[] { "TypeOfOrder" },
                o => o.Time, OrderBy.Ascending
                ));

        [HttpGet("GetByTypeOrderId")]
        public async Task<IActionResult> GetByTypeOrderId(int typeOrderId)
        {
            if (typeOrderId <= 0)
                return BadRequest("TypeOrderId is required");

            return Ok( await _unitOfWork.Orders.GetAll( o => o.TypeOfOrderId == typeOrderId, null,  o => o.Time, OrderBy.Ascending));
        }

        [HttpGet("GetByTypeOrderIdInclude")]
        public async Task<IActionResult> GetByTypeOrderIdInclude(int typeOrderId)
        {
            if (typeOrderId <= 0)
                return BadRequest("TypeOrderId is required");

            return Ok(
                await _unitOfWork.Orders.GetAll(
                o => o.TypeOfOrderId == typeOrderId,
                new[] { "TypeOfOrder" },
                o => o.Time, OrderBy.Ascending
                ));
        }

        [HttpGet("GetByTypeOrderIdIncludes")]
        public async Task<IActionResult> GetByTypeOrderIdIncludes(int typeOrderId)
        {
            if (typeOrderId <= 0)
                return BadRequest("TypeOrderId is required");

            return Ok(
                await _unitOfWork.Orders.GetAll(
                o => o.TypeOfOrderId == typeOrderId,
                new[] { "Meal", "Ingredient", "TypeOfOrder"  },
                o => o.Time, OrderBy.Ascending
                ));
        }

        [HttpGet("GetByTypeOrderName")]
        public async Task<IActionResult> GetByTypeOrderName(string TypeOfOreder)
        {
            if (TypeOfOreder is null)
                return BadRequest("TypeOfOreder is required");

            return Ok(
                await _unitOfWork.Orders.GetAll(
                o => o.TypeOfOrder.TypeOrder == TypeOfOreder,
                new[] { "Meal", "Ingredient", "TypeOfOrder"  },
                o => o.Time, OrderBy.Ascending
                ));
        }

        /////// Get Ingredient //////
        [HttpGet("GetByIngredient")]
        public async Task<IActionResult> GetByIngredient() => Ok(
              await _unitOfWork.Orders.GetAll(
              new[] { "Ingredient" },
              o => o.Time, OrderBy.Ascending
              ));

        [HttpGet("GetByIngredientId")]
        public async Task<IActionResult> GetByIngredientId(int ingredientId)
        {
            if (ingredientId <= 0)
                return BadRequest("IngredientId is required");

            return Ok( await _unitOfWork.Orders.GetAll( o => o.IngredientId == ingredientId, null, o => o.Time, OrderBy.Ascending ));
        }

        [HttpGet("GetByIngredientIdInclude")]
        public async Task<IActionResult> GetByIngredientIdInclude(int ingredientId)
        {
            if (ingredientId <= 0)
                return BadRequest("IngredientId is required");

            return Ok( await _unitOfWork.Orders.GetAll(
                    o => o.IngredientId == ingredientId,  new[] { "Ingredient" }, o => o.Time, OrderBy.Ascending ));
        }
        [HttpGet("GetByIngredientIdIncludes")]
        public async Task<IActionResult> GetByIngredientIdIncludes(int ingredientId)
        {
            if (ingredientId <= 0)
                return BadRequest("IngredientId is required");

            return Ok(
                await _unitOfWork.Orders.GetAll(
                o => o.IngredientId == ingredientId,
                new[] { "Meal", "Ingredient", "TypeOfOrder" },
                o => o.Time, OrderBy.Ascending
                ));
        }

        /////// Get Meal //////

        [HttpGet("GetByMeal")]
        public async Task<IActionResult> GetByMeal() => Ok(
                await _unitOfWork.Orders.GetAll(
                new[] { "Meal" },
                o => o.Time, OrderBy.Ascending
                ));

        [HttpGet("GetByMealId")]
        public async Task<IActionResult> GetByMealId(int MealId)
        {
            if (MealId <= 0)
                return BadRequest("MealId is required");

            return Ok(
                await _unitOfWork.Orders.GetAll(
                o => o.MealId == MealId,
                new[] { "Meal", "Ingredient", "TypeOfOrder"  },
                o => o.Time, OrderBy.Ascending
                ));
        }

        [HttpGet("GetByMealIdInclude")]
        public async Task<IActionResult> GetByMealIdInclude(int MealId)
        {
            if (MealId <= 0)
                return BadRequest("MealId is required");

            return Ok(
                await _unitOfWork.Orders.GetAll(
                o => o.MealId == MealId,
                new[] { "Meal" },
                o => o.Time, OrderBy.Ascending
                ));
        }
        [HttpGet("GetByMealIdIncludes")]
        public async Task<IActionResult> GetByMealIdIncludes(int MealId)
        {
            if (MealId <= 0)
                return BadRequest("MealId is required");

            return Ok(
                await _unitOfWork.Orders.GetAll(
                o => o.MealId == MealId,
                new[] { "Meal", "Ingredient", "TypeOfOrder" },
                o => o.Time, OrderBy.Ascending
                ));
        }

        [HttpGet("GetByMealName")]
        public async Task<IActionResult> GetByMealName(string MealName)
        {
            if (MealName is null)
                return BadRequest("MealName is required");

            return Ok(
                await _unitOfWork.Orders.GetAll(
                o => o.Meal.Name == MealName,
                new[] { "Meal" },
                o => o.Time, OrderBy.Ascending
                ));
        }


        [HttpGet("GetIsDone")]
        public async Task<IActionResult> GetIsDone(bool isDone)
        {
            var model = await _unitOfWork.Orders.GetAll(m => m.Done == isDone);
            return Ok(model);
        }
        [HttpGet("GetIsDoneIncludes")]
        public async Task<IActionResult> GetIsDoneInclude(bool isDone)
        {
            var model = await _unitOfWork.Orders.GetAll(m => m.Done == isDone
            , new[] { "Meal", "Ingredient", "TypeOfOrder" }, o => o.Time, OrderBy.Ascending);
            return Ok(model);
        }

        /////// Get All //////

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.Orders.GetAll(o => o.Time, OrderBy.Ascending));


        [HttpGet("GetAllInclude")]
        public async Task<IActionResult> GetAllInclude() => Ok(await _unitOfWork.Orders.GetAll(new[] { "Meal", "Ingredient", "TypeOfOrder" }, o => o.Time, OrderBy.Ascending));

        /////// Add_Update //////

        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(OrderData model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _unitOfWork.Ingredients.CheckAny(t => t.Id == model.IngredientId))
                return NotFound("Ingredients Not Found");

            if (!await _unitOfWork.TypeOfOrders.CheckAny(t => t.Id == model.TypeOfOrderId))
                return NotFound("TypeOfOrder Not Found");

            if (!await _unitOfWork.Meals.CheckAny(t => t.Id == model.MealId))
                return NotFound("Meal Not Found");

            var checkModel = await _unitOfWork.Orders.FindByCriteria(m => m.Id == model.Id);
            if (model.Id <= 0)
            {
                //Add 
                checkModel = await _unitOfWork.Orders.Add(
                    new Order
                    {
                        Quantity = model.Quantity,
                        Time = new DateTime().ToLocalTime(),
                        TypeOfOrderId = model.TypeOfOrderId,
                        IngredientId = model.IngredientId,
                        MealId = model.MealId,
                    }) ;
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");

                checkModel.Quantity = model.Quantity;
                checkModel.Done = model.Done;
                checkModel.TypeOfOrderId = model.TypeOfOrderId;
                checkModel.IngredientId = model.IngredientId;
                checkModel.MealId = model.MealId;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpPost("Add_UpdateRange")]
        public async Task<IActionResult> Add_UpdateRange(Order[] models)
        {
            var checkModel = await _unitOfWork.Orders.AddRange(models);
            return Ok();
        }
            [HttpGet("EditIsDone")]
        public async Task<IActionResult> EditIsDelivered(int id)
        {
            if (id <= 0)
                return BadRequest("Id Is Required");

            Order checkModel = await _unitOfWork.Orders.FindByCriteria(m => m.Id == id);
            if (checkModel is null)
                return NotFound("Order Not Found !");

            checkModel.Done = !checkModel.Done;
            await _unitOfWork.Complete();
            return Ok("Edited");
        }

        /////// Delete //////

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.Orders.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }
    
}
}
