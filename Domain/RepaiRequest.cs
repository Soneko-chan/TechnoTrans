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
        public RepairRequestStatus Status { get; set; }
        public string ResponsibleMechanic { get; set; }
        public string Comments { get; set; }
        public string SpareParts { get; set; }


        public RepairRequest()
        {
            CreationDate = DateTime.Now;
            Status = RepairRequestStatus.New;
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
            Status = RepairRequestStatus.New;
        }
    }
}