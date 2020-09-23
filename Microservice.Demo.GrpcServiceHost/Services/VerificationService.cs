using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microservice.Demo.GrpcServiceHost.Protos;
using Microservice.Demo.Service.Application.Dtos;
using Microservice.Demo.Service.Applications.Services;
using Microservice.Demo.Service.Domain.Aggregates;
using Microsoft.Extensions.Logging;

namespace Microservice.Demo.GrpcServiceHost
{
    public class VerificationService : Protos.Verification.VerificationBase
    {
        private readonly ILogger<VerificationService> _logger;
        private readonly IVerificationAppService _verificationService;
        public VerificationService(ILogger<VerificationService> logger, IVerificationAppService appService)
        {
            _logger = logger;
            _verificationService = appService;
        }

        public override async Task<VerificationReply> SendMessage(VerificationRequest request, ServerCallContext context)
        {
            var task = Task.Run(() => {
                _verificationService.AddVerification(new VerificationDTO() { Code = request.Code, BizCode = BizCode.Register, To = request.To });
            });
            await task;

            return new VerificationReply(){ Ok = true };
        }
    }
}
