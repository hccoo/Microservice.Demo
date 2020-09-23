using Microservice.Demo.Service.Domain.Aggregates;

namespace Microservice.Demo.Service.Domain.Repositories
{
    public interface IVerificationRepository : IRepository<Verification, int>
    {
    }
}
