namespace Domain
{
    public enum RepairRequestStatus
    {
        New,
        InProgress,
        WaitingPairs,
        Done,
    }

    public static class RepairRequestStatusExtensions
    {
        public static string ToPrettyString(this RepairRequestStatus type)
        {
            return type switch
            {
                RepairRequestStatus.New => "Новая заявка",
                RepairRequestStatus.InProgress => "В процессе ремонта",
                RepairRequestStatus.WaitingPairs => "Ожидание запчастей",
                RepairRequestStatus.Done => "Заявка завершена",
                _ => "Новая заявка",
            };
        }
    }
}