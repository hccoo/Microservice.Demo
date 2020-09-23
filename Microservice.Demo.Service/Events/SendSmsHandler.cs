using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microservice.Demo.Infrastructure.Events;
using Microservice.Demo.Service.Domain;
using Microservice.Demo.Service.Domain.Aggregates;
using Microservice.Demo.Service.Domain.Events;

namespace Microservice.Demo.Service.Events
{
    [HandlesAsynchronously]
    public class SendSmsHandler : INotificationHandler<VerificationCreatedEvent> //IEventHandler<VerificationCreatedEvent>
    {
        public SendSmsHandler()
        {
        }

        public Task Handle(VerificationCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
