using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microservice.Demo.Service.Domain.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microservice.Demo.RestServiceHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public string Get()
        {
            _mediator.Publish(new VerificationCreatedEvent());
            return "Hello world.";
        }
    }


    public class Ping : DomainEvent, INotification { }


    public class Pong : INotificationHandler<Ping>
    {
        public Pong() { }
        public Task Handle(Ping notification, CancellationToken cancellationToken)
        {
            string s = "Hello world2";
            return Task.CompletedTask;
        }
    }
}
