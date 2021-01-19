using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRGenericApproachTest.Mapping
{
    public class MessageCommandProfile : Profile
    {
        public MessageCommandProfile()
        {
            CreateMap<MessageProcessorMediatR.UpdateTimezoneMessage, MessageProcessorMediatR.UpdateTimezoneCommand>();
            CreateMap<MessageProcessorMediatR.PairStartButtonMessage, MessageProcessorMediatR.PairStartButtonCommand>();
        }
    }
}
