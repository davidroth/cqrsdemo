using CqrsDemo.Core.Commands;
using CqrsDemo.Core.Queries;
using CqrsDemoWeb.Models;
using Fusonic.Extensions.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CqrsDemoWeb.Controllers
{
    public class OrdersController(IMediator mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var orders = await mediator.Send(new GetOrderList());
            return View(orders);
        }

        public async Task<IActionResult> CancelOrder(CancelOrder command)
        {
            await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Recalculate(RecalculateOrder command)
        {
            await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int orderId)
        {
            var details = await mediator.Send(new GetOrderDetails(orderId));
            return View(new OrderEditViewModel
            {
                Id = details.Id,
                Name = details.Name,
                Positions =  details.Positions.Select(x => new OrderEditViewModel.Position() 
                {  
                    Id = x.Id, 
                    Name = x.Name, 
                    Quantity = x.Quantity 
                }).ToList(),
                Product = details.Product,
                Status  = details.Status
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateOrderDetails command)
        {
            await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}