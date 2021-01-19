using FluentResults;
using MediatR;

namespace MessageProcessorMediatR
{
    public class PairStartButtonCommand : IRequest<Result>
    {
        public string ButtonId { get; set; }
        public string TcuId { get; set; }
    }
}
