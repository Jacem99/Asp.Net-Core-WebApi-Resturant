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
    public class ReservationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Reservation.FindByCriteria(m => m.Id == Id);
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        [HttpGet("GetByIdInclude")]
        public async Task<IActionResult> GetByIdInclude(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            var model = await _unitOfWork.Reservation.FindByCriteriaInclude(m => m.Id == Id, 
                new[] { "Customers", "Waiter", "Order" ,"TypeOfReservation" ,"Tables"});
            return model is null ? NotFound("Not Found") : Ok(model);
        }

        [HttpGet("GetIsTaken")]
        public async Task<IActionResult> GetIsTaken(bool isTaken)
        {
            var model = await _unitOfWork.Reservation.GetAll(m => m.Taken == isTaken);
            return Ok(model);
        }
        [HttpGet("GetIsTakenInclude")]
        public async Task<IActionResult> GetIsTakenInclude(bool isTaken)
        {
            var model = await _unitOfWork.Reservation.GetAll(m => m.Taken ==isTaken
            , new[] { "Customers", "Waiter", "Order", "TypeOfReservation", "Tables" } , o=> o.Order.Time , OrderBy.Ascending);
            return Ok(model);
        }

        /////// Get Customer //////
        [HttpGet("GetByCustomer")]
        public async Task<IActionResult> GetByCustomer() =>
          Ok(await _unitOfWork.Reservation.GetAll(new[] { "Customers" }, o => o.CustomersId, OrderBy.Ascending));

        [HttpGet("GetByCustomerId")]
        public async Task<IActionResult> GetByCustomerId(int customerId) =>
        Ok(await _unitOfWork.Reservation.GetAll(c => c.CustomersId == customerId, new[] { "Customers" }, d => d.Id, OrderBy.Ascending));

        [HttpGet("GetByCustomerIdIncludes")]
        public async Task<IActionResult> GetByCustomerIdIncludes(int customerId) =>
            Ok(await _unitOfWork.Reservation.GetAll(c => c.CustomersId == customerId,
            new[] { "Customers", "Waiter", "Order", "TypeOfReservation", "Tables" }, d => d.Order.Time, OrderBy.Ascending));

        [HttpGet("GetByWaiter")]
        public async Task<IActionResult> GetByWaiter() =>
           Ok(await _unitOfWork.Reservation.GetAll(new[] { "Waiter" }, d => d.Id, OrderBy.Ascending));

        [HttpGet("GetByWaiterId")]
        public async Task<IActionResult> GetByWaiterId(string waiterId) =>
             Ok(await _unitOfWork.Reservation.GetAll(c => c.WaiterId == waiterId, new[] { "Waiter" }, d => d.Id, OrderBy.Ascending));


        [HttpGet("GetByWaiterIdIncludes")]
        public async Task<IActionResult> GetByDeliveryIdIncludes(string WaiterId) =>
           Ok(await _unitOfWork.Reservation.GetAll(c => c.WaiterId == WaiterId
           , new[] { "Customers", "Waiter", "Order", "TypeOfReservation", "Tables" }, d => d.Order.Time, OrderBy.Ascending));


        [HttpGet("GetByOrderId")]
        public async Task<IActionResult> GetByOrderId(int orderId) =>
             Ok(await _unitOfWork.Reservation.GetAll(c => c.OrderId == orderId, null, d => d.Id, OrderBy.Ascending));

        [HttpGet("GetByOrderIdInclude")]
        public async Task<IActionResult> GetByOrderIdInclude(int orderId) =>
           Ok(await _unitOfWork.Reservation.GetAll(c => c.OrderId == orderId, new[] { "Order" }, d => d.Order.Time, OrderBy.Ascending));

        [HttpGet("GetByOrderIdIncludes")]
        public async Task<IActionResult> GetByOrderIdIncludes(int orderId) =>
          Ok(await _unitOfWork.Reservation.GetAll(c => c.OrderId == orderId,
             new[] { "Customers", "Waiter", "Order", "TypeOfReservation", "Tables" }, d => d.Order.Time, OrderBy.Ascending));

        [HttpGet("GetByTableId")]
        public async Task<IActionResult> GetByTableId(int tableId) =>
           Ok(await _unitOfWork.Reservation.GetAll(c => c.TableId == tableId, null, d => d.Id, OrderBy.Ascending));

        [HttpGet("GetByTableIdInclude")]
        public async Task<IActionResult> GetByTableIdInclude(int tableId) =>
           Ok(await _unitOfWork.Reservation.GetAll(c => c.TableId == tableId, new[] { "Tables" }, d =>d.Id, OrderBy.Ascending));

        [HttpGet("GetByTableIdIncludes")]
        public async Task<IActionResult> GetByTableIdIncludes(int tableId) =>
          Ok(await _unitOfWork.Reservation.GetAll(c => c.TableId == tableId,
             new[] { "Customers", "Waiter", "Order", "TypeOfReservation", "Tables" }, d => d.Order.Time, OrderBy.Ascending));


        /////// Get All //////

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _unitOfWork.Reservation.GetAll(o => o.Id, OrderBy.Ascending));


        [HttpGet("GetAllInclude")]
        public async Task<IActionResult> GetAllInclude() => Ok(await _unitOfWork.Reservation.GetAll(
            new[] { "Customers", "Waiter", "Order", "TypeOfReservation", "Tables" }, o => o.Order.Time, OrderBy.Ascending));

        
        [HttpPost("Add_Update")]
        public async Task<IActionResult> Add_Update(ReservationData model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _unitOfWork.Customers.CheckAny(t => t.Id == model.CustomersId))
                return NotFound("Cutomer Not Found");

            if (!await _unitOfWork.User.CheckAny(t => t.Id == model.WaiterId))
                return NotFound("Waiter Not Found");

            if (!await _unitOfWork.Orders.CheckAny(t => t.Id == model.OrderId))
                return NotFound("Order Not Found");

            if (!await _unitOfWork.Tables.CheckAny(t => t.Id == model.TableId))
                return NotFound("Table Not Found");


            Reservation checkModel = await _unitOfWork.Reservation.FindByCriteria(m => m.Id == model.Id);

            if (model.Id <= 0)
            { //Add 
                checkModel = await _unitOfWork.Reservation.Add(
                     new Reservation
                     {
                         Taken = model.Taken,
                         TypeOfReservationId = model.TypeOfReservationId,
                         CustomersId = model.CustomersId,
                         WaiterId = model.WaiterId,
                         OrderId = model.OrderId,
                         TableId = model.TableId,
                     });
            }
            else
            {
                //Update
                if (checkModel is null)
                    return NotFound("Not Found");

                checkModel.TypeOfReservationId = model.TypeOfReservationId;
                checkModel.CustomersId = model.CustomersId;
                checkModel.WaiterId = model.WaiterId;
                checkModel.OrderId = model.OrderId;
                checkModel.TableId = model.TableId;
                checkModel.Taken = model.Taken;
              
            }
            await _unitOfWork.Complete();
            return Ok(checkModel);
        }
        [HttpPut("EditIsTaken")]
        public async Task<IActionResult> EditIsDelivered(int id)
        {
            if (id <= 0)
                return BadRequest("Id Is Required");
            Reservation checkModel = await _unitOfWork.Reservation.FindByCriteria(m => m.Id == id);
            if (checkModel is null)
                return NotFound("Reservation Not Found !");

            checkModel.Taken = !checkModel.Taken;
            return Ok("Edited");
        }

        /////// Delete //////

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id Is Required");

            return await _unitOfWork.Reservation.Remove(m => m.Id == Id) == 0 ? NotFound("Not Found") : Ok("Deleted");
        }

    }
}
