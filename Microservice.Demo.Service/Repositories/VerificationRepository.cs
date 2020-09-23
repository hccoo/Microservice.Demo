using System;
using System.Collections.Generic;
using System.Text;
using Microservice.Demo.Service.Domain.Aggregates;
using Microservice.Demo.Service.Domain.Repositories;

namespace Microservice.Demo.Service.Repositories
{
    public class VerificationRepository : Repository<Verification, int>, IVerificationRepository
    {
        public VerificationRepository(IDbContextProvider provider) : base(provider)
        {

        }
    }
}
