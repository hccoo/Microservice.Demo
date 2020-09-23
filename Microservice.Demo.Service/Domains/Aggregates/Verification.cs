using System;
using Microservice.Demo.Service.Domain.Events;

namespace Microservice.Demo.Service.Domain.Aggregates
{
    public class Verification : Entity<int>
    {
        public string Code { get; set; } = string.Empty;

        public string To { get; set; } = string.Empty;

        public DateTime ExpiredOn { get; set; } = DateTime.Now.AddHours(1);

        public BizCode BizCode { get; set; } = default(int);

        public bool IsUsed { get; set; } = false;

        public bool IsSuspend { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime LastUpdOn { get; set; } = DateTime.Now;

        //public VerificationStatus Status { get; set; } = VerificationStatus.Init;

        public void SetSuspend()
        {
            IsSuspend = true;
            LastUpdOn = DateTime.Now;
        }

        public void SetUsed()
        {
            IsSuspend = true;
            IsUsed = true;
            LastUpdOn = DateTime.Now;
        }

        public void GenerateNewCode()
        {
            Code = VerificationCodeGenerator.GenerateVerificationCode(4);
        }

        public void CreatedConfirm()
        {
            DomainEvent.Publish(new VerificationCreatedEvent(this)
            {
                CreatedTime = DateTime.Now,
                ID = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                VerificationCode = Code,
                VerificationTo = To,
                BizCode = BizCode
            });
        }
    }

    public enum BizCode
    {
        Register = 1,
        ForgetPassword = 2
    }
    public static class VerificationFactory
    {
        public static Verification CreateVerification(BizCode bizCode, string to)
        {
            var result = new Verification();
            result.GenerateNewCode();

            var now = DateTime.Now;

            result.BizCode = bizCode;
            result.CreatedOn = now;
            result.ExpiredOn = now.AddHours(1);
            result.IsSuspend = result.IsUsed = false;
            result.To = to;

            return result;
        }
    }
}
