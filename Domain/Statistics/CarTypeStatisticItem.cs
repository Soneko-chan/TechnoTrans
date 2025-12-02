namespace Domain.Statistics
{
    public record CarTypeStatisticItem
    {
        public required string CarType { get; set; }
        public required int Count { get; set; }
    }
}