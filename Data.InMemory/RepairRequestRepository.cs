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
            return _requests.OrderByDescending(r => r.CreationDate).ToList();
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