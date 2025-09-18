namespace Domain
{
    public class RepairRequest
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string CarType { get; set; }
        public string CarModel { get; set; }
        public string ProblemDescription { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public RepairRequestType Status { get; set; }
        public string ResponsibleMechanic { get; set; }
        public string Comments { get; set; }
        public string SpareParts { get; set; }

        // TODO Перенести это в UI слой
        public string StatusDisplay => Status.ToPrettyString();

        public RepairRequest()
        {
            CreationDate = DateTime.Now;
            Status = RepairRequestType.New;
        }

        public RepairRequest(string carType, string carModel, string problemDescription,
                           string clientName, string phoneNumber)
        {
            CreationDate = DateTime.Now;
            CarType = carType;
            CarModel = carModel;
            ProblemDescription = problemDescription;
            ClientName = clientName;
            PhoneNumber = phoneNumber;
            Status = RepairRequestType.New;
        }
    }
}