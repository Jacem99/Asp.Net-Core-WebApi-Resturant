using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantWihtUOF.Core.Consts;
using ResturantWihtUOF.Core.Data;
using ResturantWihtUOF.Core.Interfaces;
using ResturantWihtUOF.Core.Models;
using System;

using System.Threading.Tasks;

namespace ResturantWihtUOF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetbyId")]
        public async Task<IActionResult> GetbyId(int CustomerId)
        {
            if (CustomerId <= 0)
                return BadRequest("CustomerId Is Required");

            var result = await _unitOfWork.Customers.FindByCriteria(c => c.Id == CustomerId);

            return result is null ? NotFound("Not Found") : Ok(result);
        }

        [HttpGet("CheckAny")]
        public async Task<IActionResult> CheckAny(int CustomerId = 0)
        {
            if (CustomerId <= 0)
                return Ok(await _unitOfWork.Customers.CheckAny());

            return Ok(await _unitOfWork.Customers.CheckAny(c => c.Id == CustomerId));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.Customers.GetAll());

        [HttpGet("GetAllByRelationShip")]
        public async Task<IActionResult> GetAllByRelationShip() => Ok(await _unitOfWork.Customers.GetAll(new[] { "Address" }, o => o.Name, OrderBy.Ascending));

        [HttpGet("GetByCriteria")]
        public async Task<IActionResult> GetByCriteria(string phone = null)
        {
            if (phone is null)
                return BadRequest("phone Is Required");

            return Ok(await _unitOfWork.Customers.GetAll(c => c.Phone == phone));
        }
        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(CustomerData model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var checkModel = await _unitOfWork.Customers.FindByCriteria(c => c.Id == model.CustomerId);
            bool checkPhone = await _unitOfWork.Customers.CheckAny(c => c.Phone == model.Phone);

            if (model.CustomerId <= 0)
            { //Add 
                if (checkPhone)
                    return BadRequest("CustomerPhone Is Existed");

                checkModel = await _unitOfWork.Customers.Add(
                    new Customers()
                    {
                        Id = model.CustomerId,
                        Name = model.Name,
                        Phone = model.Phone,
                        AddressId = model.AddressId
                    });
            }
            else
            {
                //Update
                if (checkModel is null)
                    return BadRequest("Customer isn't Exist");

                if (checkPhone && checkModel.Phone != model.Phone)
                    return BadRequest("CustomerPhone Is Existed");

                checkModel.Name = model.Name;
                checkModel.Phone = model.Phone;
                checkModel.AddressId = model.AddressId;

            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }
        [HttpGet("Count")]
        public async Task<IActionResult> Count() => Ok(await _unitOfWork.Customers.Count());
        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int customerId)
        {
            if (customerId <= 0)
                return BadRequest("customerId is Required");

            int result = await _unitOfWork.Customers.Remove(c => c.Id == customerId);
            if (result == 0)
                return NotFound("Not Found");

            await _unitOfWork.Complete();
            return Ok("Deleted");
        }

    }

}
