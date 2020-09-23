using Microservice.Demo.Infrastructure;
using Microservice.Demo.Infrastructure.Events;
using Microservice.Demo.Infrastructure.Transactions;
using Microservice.Demo.Service.Application.Dtos;
using Microservice.Demo.Service.Domain.Aggregates;
using Microservice.Demo.Service.Domain.Repositories;
using Microservice.Demo.Service.Domain.Services;
using System;
using System.Linq;

namespace Microservice.Demo.Service.Applications.Services
{
    public interface IVerificationAppService : IApplicationServiceContract
    {
        void AddVerification(VerificationDTO verificationDTO);

        VerificationDTO GetAvailableVerification(BizCode bizCode, string to);

        void SetVerificationUsed(VerificationDTO verificationDTO);
    }

    public class VerificationAppService : ApplicationServiceContract, IVerificationAppService
    {
        readonly IVerificationRepository _verificationRepository;
        readonly IVerificationService _verificationService;
        readonly IEventBus _eventBus;
        readonly ITest _test;

        public VerificationAppService(
            IVerificationRepository verificationRepository,
            IVerificationService verificationService,
            IDbUnitOfWork dbUnitOfWork,
            IEventBus eventBus,
            ITest test) : base(dbUnitOfWork)
        {
            _verificationRepository = verificationRepository;
            _verificationService = verificationService;
            _eventBus = eventBus;// IocProvider.GetService<IEventBus>();
            _test = test;
        }

        public void AddVerification(VerificationDTO verificationDTO)
        {
            using (ITransactionCoordinator coordinator = new TransactionCoordinator(_dbUnitOfWork, _eventBus))
            {
                var verifications = _verificationRepository.GetFiltered(o => o.IsSuspend == false && o.To == verificationDTO.To && o.BizCode == verificationDTO.BizCode).ToList();
                _verificationService.SetVerificationsSuspend(verifications);

                var verification = VerificationFactory.CreateVerification(verificationDTO.BizCode, verificationDTO.To);
                verification.CreatedConfirm();
                _verificationRepository.Add(verification);

                coordinator.Commit();
            }
        }

        public void SetVerificationUsed(VerificationDTO verificationDTO)
        {
            var verification = _verificationRepository.GetFiltered(o => o.To == verificationDTO.To && o.BizCode == verificationDTO.BizCode && o.Code == verificationDTO.Code && o.IsUsed == false && o.IsSuspend == false).OrderByDescending(g => g.CreatedOn).FirstOrDefault();

            if (verification != null)
            {
                verification.SetUsed();
                _dbUnitOfWork.Commit();
            }
        }

        public VerificationDTO GetAvailableVerification(BizCode bizCode, string to)
        {
            var now = DateTime.Now;
            var result = _verificationRepository.GetFiltered(o =>
                            o.BizCode == bizCode
                            && to == o.To
                            && o.IsSuspend == false
                            && o.IsUsed == false
                            && o.ExpiredOn > now).OrderByDescending(g => g.CreatedOn).FirstOrDefault();

            return result == null ? null : new VerificationDTO { BizCode = result.BizCode, Code = result.Code, To = result.To };
        }
    }
}
