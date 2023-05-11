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
    public class DeliveringController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public DeliveringController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Delivering.FindByCriteria(m => m.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        [HttpGet("GetByIdInclude")]
        public async Task<IActionResult> GetByIdInclude(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Delivering.FindByCriteriaInclude(m => m.Id == Id, new[] {"CutomerOrder", "Delivery","Order"});
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        [HttpGet("GetIsDelivered")]
        public async Task<IActionResult> GetDelivered()
        {
            var model = await _unitOfWork.Delivering.GetAll(m => m.isDelivered == true);
            return model is null ? NotFound("Not Found") : Ok(model);
        }
        [HttpGet("GetIsDeliveredInclude")]
        public async Task<IActionResult> GetDeliveredInclude()
        {
            var model = await _unitOfWork.Delivering.GetAll(m => m.isDelivered == true, new[] { "CutomerOrder", "Delivery" ,"Order"});
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        /////// Get Customer //////
        [HttpGet("GetByCustomer")]
        public async Task<IActionResult> GetByCustomer() =>
          Ok(await _unitOfWork.Delivering.GetAll(new[] { "CutomerOrder" }, o => o.CutomerOrderId, OrderBy.Ascending));

        [HttpGet("GetByCustomerId")]
        public async Task<IActionResult> GetByCustomerId(int customerId) =>
        Ok(await _unitOfWork.Delivering.GetAll(c => c.CutomerOrderId == customerId, new[] { "CutomerOrder" }, d => d.Id, OrderBy.Ascending));

        [HttpGet("GetByCustomerIdIncludes")]
        public async Task<IActionResult> GetByCustomerIdIncludes(int customerId) =>
      Ok(await _unitOfWork.Delivering.GetAll(c => c.CutomerOrderId == customerId, new[] { "CutomerOrder", "Delivery", "Order" }, d => d.Id, OrderBy.Ascending));

        [HttpGet("GetByDelivery")]
        public async Task<IActionResult> GetByDelivery() =>
           Ok(await _unitOfWork.Delivering.GetAll(new[] { "Delivery" }, d => d.DeliveryId, OrderBy.Ascending));

        [HttpGet("GetByDeliveryId")]
        public async Task<IActionResult> GetByDeliveryId(string deliveryId) =>
             Ok(await _unitOfWork.Delivering.GetAll(c => c.DeliveryId == deliveryId, new[] { "Delivery" }, d => d.Id, OrderBy.Ascending));


        [HttpGet("GetByDeliveryIdIncludes")]
        public async Task<IActionResult> GetByDeliveryIdIncludes(string deliveryId) =>
           Ok(await _unitOfWork.Delivering.GetAll(c => c.DeliveryId == deliveryId, new[] { "CutomerOrder", "Delivery", "Order" }, d => d.Id, OrderBy.Ascending));


        [HttpGet("GetByOrderId")]
        public async Task<IActionResult> GetByOrderId(int orderId) =>
             Ok(await _unitOfWork.Delivering.GetAll(c => c.OrderId == orderId , null , d => d.Id, OrderBy.Ascending));

        [HttpGet("GetByOrderIdInclude")]
        public async Task<IActionResult> GetByOrderIdInclude(int orderId) =>
           Ok(await _unitOfWork.Delivering.GetAll(c => c.OrderId == orderId, new[] { "Order" }, d => d.Order.Time, OrderBy.Ascending));

        [HttpGet("GetByOrderIdIncludes")]
        public async Task<IActionResult> GetByOrderIdIncludes(int orderId) =>
          Ok(await _unitOfWork.Delivering.GetAll(c => c.OrderId == orderId, new[] { "CutomerOrder", "Delivery", "Order" }, d => d.Order.Time, OrderBy.Ascending));


        /////// Get All //////

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.Delivering.GetAll(o => o.Id, OrderBy.Ascending));


        [HttpGet("GetAllInclude")]
        public async Task<IActionResult> GetAllInclude() => Ok(await _unitOfWork.Delivering.GetAll(new[] { "CutomerOrder", "Delivery","Order" }, o => o.Id, OrderBy.Ascending));

        /////// Add_Update //////
        /*
                 private async Task<string> checkModel(DeliveringData model)
                {
                    if (!await _unitOfWork.Customers.CheckAny(t => t.Id == model.CutomerOrderId))
                        return "Cutomer Not Found";

                    if (!await _unitOfWork.User.CheckAny(t => t.Id == model.DeliveryId))
                        return "Delivery Not Found";

                    if (!await _unitOfWork.Orders.CheckAny(t => t.Id == model.OrderId))
                        return "Order Not Found";

                    return "ok";
                }*/
        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(DeliveringData model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _unitOfWork.Customers.CheckAny(t => t.Id == model.CutomerOrderId))
                return NotFound("Cutomer Not Found");

            if (!await _unitOfWork.User.CheckAny(t => t.Id == model.DeliveryId))
                return NotFound("Delivery Not Found");

            if (!await _unitOfWork.Orders.CheckAny(t => t.Id == model.OrderId))
                return NotFound("Order Not Found");


            Delivering checkModel = await _unitOfWork.Delivering.FindByCriteria(m => m.Id == model.Id);

            if (model.Id <= 0)
            { //Add 
              checkModel = await _unitOfWork.Delivering.Add(
                   new Delivering
                    {
                        DeliveryId = model.DeliveryId,isDelivered = model.isDelivered, Site = model.Site,
                        OrderId = model.OrderId, CutomerOrderId = model.CutomerOrderId,
                    });
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");

                checkModel.DeliveryId = model.DeliveryId;
                checkModel.isDelivered = model.isDelivered;
                checkModel.Site = model.Site;
                checkModel.OrderId = model.OrderId;
                checkModel.CutomerOrderId = model.CutomerOrderId;
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }
        [HttpPut("EditIsDelivered")]
        public async Task<IActionResult> EditIsDelivered(int id)
        {
            if (id <= 0)
                return BadRequest("Id Is Required");
            Delivering checkModel = await _unitOfWork.Delivering.FindByCriteria(m => m.Id == id);
            if (checkModel is null)
                return NotFound("DeliveryOrder Not Found !");

            checkModel.isDelivered = !checkModel.isDelivered;
            return Ok("Edited");
        }

        /////// Delete //////

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.Delivering.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }

    }
}
