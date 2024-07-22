using CqrsDemo.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using CqrsDemo.Core.Commands.Dispos;
using Fusonic.Extensions.Mediator;

namespace CqrsDemoWeb.Controllers
{
    public class DisposController(IMediator mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var orders = await mediator.Send(new GetDispoDeviceList());
            return View(orders);
        }

        public async Task<IActionResult> ChangeAvailabilityDate(ChangeAvailabilityDate command)
        {
            await mediator.Send(command, default);
            return RedirectToAction(nameof(Index));
        }
    }
}