using System.Collections.Generic;
using Domain;

namespace Data.Interfaces
{
    public interface IRepairRequestRepository
    {
        void Add(RepairRequest entity);
        RepairRequest? GetById(int id);
        List<RepairRequest> GetAll(RepairRequestFilter filter);
        void Update(RepairRequest entity);
        void Delete(int id);
    }
}