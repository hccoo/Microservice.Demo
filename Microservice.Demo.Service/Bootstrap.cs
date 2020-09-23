using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microservice.Demo.Infrastructure.Events;
using Microservice.Demo.Service.Domain.Events;
using Microservice.Demo.Service.Events;
using Microservice.Demo.Service.Domain.Aggregates;
using Microservice.Demo.Service.Application.Dtos;

namespace Microservice.Demo.Service
{
    public static class Bootstrap
    {
        public static void SubscribeEvents()
        {
            //IocProvider.GetService<IEventBus>().Subscribe<VerificationCreatedEvent>(new SendSmsHandler());
        }

        public static void CreateMapper()
        {
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<User, UserDTO>().ForMember(dest => dest.RoleNames, opt => opt.Ignore()).ForMember(dest => dest.OrganizationName, opt => opt.Ignore());
            //    cfg.CreateMap<UserDTO, User>();
            //    cfg.CreateMap<InvoiceForm, InvoiceFormDTO>().ForMember(dest => dest.FromOrganizationName, opt => opt.Ignore())
            //                                                .ForMember(dest => dest.ToOrganizationName, opt => opt.Ignore())
            //                                                .ForMember(dest => dest.StatusDesc, opt => opt.Ignore());
            //});
        }
    }
}
