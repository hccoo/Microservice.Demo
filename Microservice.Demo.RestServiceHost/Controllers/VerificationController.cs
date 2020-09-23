using System.Threading.Tasks;
using Microservice.Demo.Service.Application.Dtos;
using Microservice.Demo.Service.Applications.Services;
using Microservice.Demo.Service.Domain.Repositories;
using Microservice.Demo.Service.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Demo.RestServiceHost.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly IVerificationAppService _verificationService;
        private readonly IMessageAppService _messageService;

        public VerificationController(IVerificationAppService verificationService,IMessageAppService messageAppService)
        {
            _verificationService = verificationService;
            _messageService = messageAppService;
        }

        [HttpPost]
        public void Add(VerificationDTO model)
        {
            _messageService.AddVerification(model);
        }
    }
}