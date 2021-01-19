using FluentResults;
using MediatR;

namespace MediatRCommandQueryPipe.Commands
{
    public interface ICommand : IRequest<Result>
    {
        
    }
}