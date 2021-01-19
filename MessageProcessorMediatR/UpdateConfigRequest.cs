using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageProcessorMediatR
{
    public class UpdateConfigRequest : IRequest
    {
        public string Timezone { get; set; }
    }

    public class UpdateConfigRequestHandler : AsyncRequestHandler<UpdateConfigRequest>, IDisposable
    {
        private readonly Guid _id;

        public UpdateConfigRequestHandler()
        {
            Console.WriteLine($"{nameof(UpdateConfigRequestHandler)} Id = {_id = Guid.NewGuid()}");
        }
        
        protected override Task Handle(UpdateConfigRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Calling {nameof(UpdateConfigRequestHandler)}");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing {nameof(UpdateConfigRequestHandler)} Id = {_id}");
        }
    }

    public class ResetConfigRequest : IRequest
    {
        public string Name { get; set; }
    }
    
    public class ResetConfigRequestHandler : AsyncRequestHandler<ResetConfigRequest>, IDisposable
    {
        private readonly Guid _id;

        public ResetConfigRequestHandler()
        {
            Console.WriteLine($"{nameof(ResetConfigRequestHandler)} Id = {_id = Guid.NewGuid()}");
        }
        
        public void Dispose()
        {
            Console.WriteLine($"Disposing {nameof(ResetConfigRequestHandler)} Id = {_id}");
        }
        
        protected override Task Handle(ResetConfigRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Calling {nameof(ResetConfigRequestHandler)}");
            return Task.CompletedTask;
        }
    }
}
