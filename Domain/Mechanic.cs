namespace Domain
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAvailable { get; set; }

        public Mechanic() { }

        public Mechanic(string fullName, string specialization, string phoneNumber)
        {
            FullName = fullName;
            Specialization = specialization;
            PhoneNumber = phoneNumber;
            IsAvailable = true;
        }
    }
}