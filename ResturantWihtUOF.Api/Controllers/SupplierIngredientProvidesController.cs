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
    public class SupplierIngredientProvidesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierIngredientProvidesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetBySupplierId")]
        public async Task<IActionResult> GetById(int SupplierId)
        {
            if (SupplierId <= 0)
                return BadRequest("SupplierId is Required");
            return Ok(await _unitOfWork.SupplierIngredientProvides.GetAll(s => s.SupplierId == SupplierId));
        }

        [HttpGet("GetBySupplierIdInclude")]
        public async Task<IActionResult> GetBySupplierIdInclude(int SupplierId)
        {
            if (SupplierId <= 0)
                return BadRequest("SupplierId is Required");
            return Ok(await _unitOfWork.SupplierIngredientProvides.GetAll(s => s.SupplierId == SupplierId , new string[] { "Ingredient", "Supplier" }, s => s.Supplier.Id, OrderBy.Ascending));
        }
        [HttpGet("GetByIngerdientId")]
        public async Task<IActionResult> GetByIngerdientId(int IngredientId)
        {
            if (IngredientId <= 0)
                return BadRequest("SupplierId is Required");
            return Ok(await _unitOfWork.SupplierIngredientProvides.GetAll(s => s.IngredientId == IngredientId));
        }
        [HttpGet("GetByIngerdientInclude")]
        public async Task<IActionResult> GetByIngerdientInclude(int IngredientId)
        {
            if (IngredientId <= 0)
                return BadRequest("SupplierId is Required");
            return Ok(await _unitOfWork.SupplierIngredientProvides.GetAll(s => s.IngredientId == IngredientId, new string[] { "Ingredient", "Supplier" }, s => s.Supplier.Id, OrderBy.Ascending));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.SupplierIngredientProvides.GetAll( s => s.Supplier));    
        [HttpGet("GetAllInclude")]
        public async Task<IActionResult> GetAllInclude() => Ok(await _unitOfWork.SupplierIngredientProvides.GetAll(new string[] { "Ingredient" , "Supplier"}, s => s.Supplier.Id , OrderBy.Ascending));

        private async Task<string> CheckModel(SupplierIngredientProvides model)
        {
            if (!await _unitOfWork.Ingredients.CheckAny(t => t.Id == model.IngredientId))
                return "IngredientId isn't Exist";

            if (!await _unitOfWork.Suppliers.CheckAny(t => t.Id == model.SupplierId))
                return "SupplierId isn't Exist";

            if (await _unitOfWork.SupplierIngredientProvides.CheckAny(t => t.SupplierId == model.SupplierId && t.IngredientId == model.IngredientId))
                return "Supplier have the already ingredient";

            if (model.Kilo <= 0 && model.Litter <= 0)
                return "No Quntity To Add";

            return "Done";
        }
        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(SupplierIngredientProvides model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string checkModeling = await CheckModel(model);

            if (checkModeling != "Done")
                return BadRequest(checkModeling);
            
            SupplierIngredientProvides Model = new SupplierIngredientProvides();

            var checkModel = await _unitOfWork.SupplierIngredientProvides.FindByCriteria(s => s.Id == model.Id);

            if(model.Id <= 0)
            {
                await _unitOfWork.SupplierIngredientProvides.Add(new SupplierIngredientProvides()
                {
                    IngredientId = model.IngredientId,
                    SupplierId = model.SupplierId,
                    Litter = model.Litter,
                    Kilo = model.Kilo,
                });
            }
            else
            {
                if (checkModel is null)
                    return NotFound("Not Found");
                checkModel.SupplierId = model.SupplierId;
                checkModel.IngredientId = model.IngredientId;
                checkModel.Litter = model.Litter;
                checkModel.Kilo = model.Kilo;
            }
            await _unitOfWork.Complete();
            return Ok(checkModeling);

        }

       /*     bool checkWorker = await _unitOfWork.SupplierIngredientProvides.CheckAny(t => t.SupplierId == model.SupplierId);

            if (!checkWorker || await _unitOfWork.SupplierIngredientProvides.CheckAny(s => s.SupplierId == model.SupplierId && s.IngredientId != model.IngredientId))
            {

                Model = await _unitOfWork.SupplierIngredientProvides.Add(model);
                await _unitOfWork.Complete();
            }

            return Ok(checkModeling);*/
   
        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity(IngredientData model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.Id <= 0)
                return NotFound("No Id To Select The Ingredient");

            Ingredient checkModel = await _unitOfWork.Ingredients.FindByCriteria(m => m.Id == model.Id);
            if (checkModel is null)
                return NotFound("Not Found Ingredient To Edit");

            checkModel.Litter = model.Litter;
            checkModel.Kilo = model.Kilo;

            await _unitOfWork.Complete();
            return Ok(checkModel);
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.SupplierIngredientProvides.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }

    }
}
