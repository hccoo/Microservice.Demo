using Microservice.Demo.Infrastructure;
using Microservice.Demo.Infrastructure.Events;
using Microservice.Demo.Infrastructure.Transactions;
using Microservice.Demo.Service.Application.Dtos;
using Microservice.Demo.Service.Domain.Aggregates;
using Microservice.Demo.Service.Domain.Repositories;
using Microservice.Demo.Service.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microservice.Demo.Service.Applications.Services
{
    public interface IMessageAppService : IApplicationServiceContract
    {
        void AddVerification(VerificationDTO verificationDTO);
    }

    public class MessageAppService : ApplicationServiceContract, IMessageAppService
    {
        readonly IVerificationRepository _verificationRepository;
        readonly IVerificationService _verificationService;
        readonly IEventBus _eventBus;
        readonly ITest _test;

        public MessageAppService(IDbUnitOfWork dbUnitOfWork,
            IVerificationRepository verificationRepo,
            IVerificationService verificationService,
            IEventBus eventBus,
            ITest test) : base(dbUnitOfWork)
        {
            _verificationRepository = verificationRepo;
            _verificationService = verificationService;
            _eventBus = eventBus;
            _test = test;
        }

        public void AddVerification(VerificationDTO verificationDTO)
        {
            using (var coordinator = new TransactionCoordinator(_dbUnitOfWork, _eventBus))
            {
                var verifications = _verificationRepository.GetFiltered(o => o.IsSuspend == false && o.To == verificationDTO.To && o.BizCode == verificationDTO.BizCode).ToList();
                _verificationService.SetVerificationsSuspend(verifications);

                var verification = VerificationFactory.CreateVerification(verificationDTO.BizCode, verificationDTO.To);
                verification.CreatedConfirm();
                _verificationRepository.Add(verification);

                coordinator.Commit();
            }
        }
    }
}
