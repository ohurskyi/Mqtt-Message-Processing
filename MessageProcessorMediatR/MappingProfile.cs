using AutoMapper;

namespace MessageProcessorMediatR
{
    public class MappingProfile: Profile

    {
        public MappingProfile()
        {
            CreateMap<UpdateConfigMsg, UpdateConfigRequest>();
            CreateMap<ResetConfigMsg, ResetConfigRequest>();
        }
    }
}