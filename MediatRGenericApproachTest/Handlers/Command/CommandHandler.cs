using MediatR;
using System;
using System.Threading.Tasks;
using FluentResults;
using System.Threading;

namespace MessageProcessorMediatR
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : IRequest<Result>
    {
        //public abstract Task<Result> Handle(TCommand request, CancellationToken cancellationToken);
       
    }

    public class UpdateTimezoneCommandHandler : ICommandHandler<UpdateTimezoneCommand>
    {
        public Task<Result> Handle(UpdateTimezoneCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Handle {nameof(UpdateTimezoneCommandHandler)} ...");
            return Task.FromResult(Result.Ok());
        }
    }

    public class PairStartButtonCommandHandler : ICommandHandler<PairStartButtonCommand>
    {
        public Task<Result> Handle(PairStartButtonCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Handle {nameof(PairStartButtonCommandHandler)} ...");
            return Task.FromResult(Result.Ok());
        }
    }
}
