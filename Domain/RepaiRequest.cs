namespace Domain
{
    public class RepairRequest
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string CarType { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public string ProblemDescription { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public RepairRequestStatus Status { get; set; }

        // Сделайте эти поля nullable
        public string? ResponsibleMechanic { get; set; }
        public string? Comments { get; set; }
        public string? SpareParts { get; set; }

        public RepairRequest()
        {
            CreationDate = DateTime.Now;
            Status = RepairRequestStatus.New;
        }

       

        public void CopyFrom(RepairRequest other)
        {
            // Id НЕ копируем!
            this.CreationDate = other.CreationDate;
            this.CarType = other.CarType;
            this.CarModel = other.CarModel;
            this.ProblemDescription = other.ProblemDescription;
            this.ClientName = other.ClientName;
            this.PhoneNumber = other.PhoneNumber;
            this.Status = other.Status;
            this.ResponsibleMechanic = other.ResponsibleMechanic;
            this.Comments = other.Comments;
            this.SpareParts = other.SpareParts;
        }
    }
}