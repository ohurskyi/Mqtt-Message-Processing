using Autofac;
using Autofac.Features.Variance;
using MediatR;
using MessageProcessorMediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRGenericApproachTest.Autofac
{
    public class CommunicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterSource(new ContravariantRegistrationSource());

            //builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
            //.AsImplementedInterfaces();

            //// Register all the Command classes (they implement IRequestHandler)
            //// in assembly holding the Commands
            //builder.RegisterAssemblyTypes(
            //                      typeof(CommunicationModule).Assembly).
            //                           AsClosedTypesOf(typeof(IRequestHandler<,>));

            // generic approach
            builder.RegisterType<MessageCommandProcessorTest<UpdateTimezoneMessage, UpdateTimezoneCommand>>()
              .As<IMessageProcessorTest>();

             builder.RegisterType<MessageCommandProcessorTest<PairStartButtonMessage, PairStartButtonCommand>>()
                .As<IMessageProcessorTest>();

            builder.RegisterType<QueryProcessorTest<GetTimezoneQuery>>().As<IQueryProcessorTest>();
            builder.RegisterType<QueryProcessorTest<GetTcuConfigQuery>>().As<IQueryProcessorTest>();
        }
    }
}
