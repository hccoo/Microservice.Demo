using Microservice.Demo.Service.Domain.Aggregates;
using System.Collections.Generic;
using System.Linq;

namespace Microservice.Demo.Service.Domain.Services
{
    public interface IVerificationService
    {
        void SetVerificationsSuspend(IEnumerable<Verification> verifications);
    }

    public class VerificationService : IVerificationService
    {
        public void SetVerificationsSuspend(IEnumerable<Verification> verifications) => verifications.ToList().ForEach(item => { item.SetSuspend(); });
    }
}
