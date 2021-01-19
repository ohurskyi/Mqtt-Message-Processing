namespace MessageProcessorMediatR
{
    public interface IQueryResult
    {
    }

    public class TimezoneQueryResult : IQueryResult
    {
        public string Timezone { get; set; } = "Zhovkva";
    }

    public class TcuConfigQueryResult : IQueryResult
    {
        public string ButtonId { get; set; } = "Button - 2";
        public string TcuId { get; set; } = "Tcu - B";
    }

}