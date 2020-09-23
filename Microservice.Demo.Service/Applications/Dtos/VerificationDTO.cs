using Microservice.Demo.Service.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Demo.Service.Application.Dtos
{
    public class VerificationDTO
    {
        public string Code { get; set; }

        public string To { get; set; }

        public BizCode BizCode { get; set; }
    }
}
