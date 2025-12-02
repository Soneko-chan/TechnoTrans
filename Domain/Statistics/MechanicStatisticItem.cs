namespace Domain.Statistics
{
    public record MechanicStatisticItem
    {
        public required string MechanicName { get; set; }
        public required int Count { get; set; }
    }
}