namespace MediatRCommandQueryPipe.CommandsWithResultApproach.Tcu
{
    public class GetTcuConfigurationCommand : ICommand
    {
        public string ButtonId { get; set; }
        public string TcuId { get; set; }
    }
}