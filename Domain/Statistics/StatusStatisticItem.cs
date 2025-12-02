namespace Domain.Statistics
{
    public record StatusStatisticItem
    {
        public required RepairRequestStatus Status { get; set; }
        public required int Count { get; set; }
    }
}