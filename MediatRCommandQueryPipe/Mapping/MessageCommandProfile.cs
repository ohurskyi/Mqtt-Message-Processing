using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using MediatRCommandQueryPipe.CommandsWithResultApproach.Tcu;
using MediatRCommandQueryPipe.CommandsWithResultApproach.Timezone;
using MediatRCommandQueryPipe.Messaging.MessageExamples;
using MediatRCommandQueryPipe.Queries;

namespace MediatRGenericApproachTest.Mapping
{
    public class MessageCommandProfile : Profile
    {
        public MessageCommandProfile()
        {
            // first test
            CreateMap<TimezoneRequest, TimezoneCommandRequest>();
            CreateMap<TcuRequest, TcuCommandRequest>();
            
            CreateMap<GetTimezoneMessage, GetTimezoneCommand>();
            CreateMap<GetTcuConfigurationMessage, GetTcuConfigurationCommand>();
        }
    }
}
