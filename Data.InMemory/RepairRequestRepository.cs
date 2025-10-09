using Data.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.InMemory
{
    public class RepairRequestRepository : IRepairRequestRepository
    {
        private readonly List<RepairRequest> _requests = new List<RepairRequest>();
        private int _nextId = 1;

        public RepairRequestRepository()
        {
            InitializeTestData();
        }

        private void InitializeTestData()
        {
            
            if (_requests.Any()) return;

            _requests.Add(new RepairRequest
            {
                Id = _nextId++,
                CarType = "Toyota",
                CarModel = "Camry",
                ClientName = "Иванов Иван Иванович",
                PhoneNumber = "+7 (999) 123-45-67",
                ProblemDescription = "Замена масла и фильтров",
                Status = RepairRequestStatus.New,
                ResponsibleMechanic = "Петров А.С.",
                CreationDate = DateTime.Now.AddDays(-5)
            });

            _requests.Add(new RepairRequest
            {
                Id = _nextId++,
                CarType = "BMW",
                CarModel = "X5",
                ClientName = "Сидоров Петр Васильевич",
                PhoneNumber = "+7 (888) 765-43-21",
                ProblemDescription = "Диагностика двигателя",
                Status = RepairRequestStatus.InProgress,
                ResponsibleMechanic = "Козлов Д.М.",
                CreationDate = DateTime.Now.AddDays(-2)
            });

            _requests.Add(new RepairRequest
            {
                Id = _nextId++,
                CarType = "Lada",
                CarModel = "Vesta",
                ClientName = "Кузнецова М.С.",
                PhoneNumber = "+7 (777) 555-44-33",
                ProblemDescription = "Замена тормозных колодок",
                Status = RepairRequestStatus.Done,
                ResponsibleMechanic = "Никитин В.П.",
                CreationDate = DateTime.Now.AddDays(-7)
            });
        }

        public int Add(RepairRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            request.Id = _nextId++;
            request.CreationDate = DateTime.Now;
            _requests.Add(request);
            return request.Id;
        }

        public RepairRequest GetById(int id)
        {
            return _requests.FirstOrDefault(r => r.Id == id);
        }

        public List<RepairRequest> GetAll()
        {
            return _requests.OrderByDescending(r => r.Id).ToList();
        }

        public bool Update(RepairRequest request)
        {
            if (request == null)
                return false;

            var existingRequest = GetById(request.Id);
            if (existingRequest == null)
                return false;

            existingRequest.CarType = request.CarType;
            existingRequest.CarModel = request.CarModel;
            existingRequest.ProblemDescription = request.ProblemDescription;
            existingRequest.ClientName = request.ClientName;
            existingRequest.PhoneNumber = request.PhoneNumber;
            existingRequest.Status = request.Status;
            existingRequest.ResponsibleMechanic = request.ResponsibleMechanic;
            existingRequest.Comments = request.Comments;
            existingRequest.SpareParts = request.SpareParts;

            return true;
        }

        public bool Delete(int id)
        {
            var request = GetById(id);
            if (request == null)
                return false;

            return _requests.Remove(request);
        }
    }
}