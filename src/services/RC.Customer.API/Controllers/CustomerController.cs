using Microsoft.AspNetCore.Mvc;
using RC.Customer.API.Application.Commands;
using RC.Customer.API.Application.Queries;
using RC.MessageBus.Mediator;
using RC.WebAPI.Core;
using System.Net;

namespace RC.Customer.API.Controllers
{
    public class CustomerController : MainController
    {
        private readonly ICustomerQueries _customerQueries;
        private readonly IMediatRHandler _mediator;

        public CustomerController(ICustomerQueries customerQueries, IMediatRHandler mediator)
        {
            _customerQueries = customerQueries;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("customers")]
        public ActionResult ListCustomers()
        {
            return CustomResponse(_customerQueries.GetAll());
        }

        [HttpPost]
        [Route("customer")]
        public async Task<IActionResult> AddCustomerAsync()
        {
            var result = await _mediator.SendCommandAsync(new AddCustomerCommand(0, "Igor Moura", "igor.moura254@hotmail.com", "43972511850"));

            return CustomResponse(result, HttpStatusCode.Created);
        }
    }
}
