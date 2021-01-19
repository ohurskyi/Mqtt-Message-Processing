namespace MediatRCommandQueryPipe.CommandsWithResultApproach.Timezone
{
    public class GetTimezoneCommand : ICommand
    {
        public string Timezone { get; set; }
    }
}