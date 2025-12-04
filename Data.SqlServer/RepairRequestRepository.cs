using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Data.Interfaces;

namespace Data.SqlServer
{
    public class RepairRequestRepository : IRepairRequestRepository
    {
        private readonly TechnoTransDbContext _context;

        public RepairRequestRepository(TechnoTransDbContext context)
        {
            _context = context;
        }

        public void Add(RepairRequest entity)
        {

            _context.RepairRequests.Add(entity);
            _context.SaveChanges();

        }

        public RepairRequest? GetById(int id)
        {
            return _context.RepairRequests.Find(id);
        }

        public List<RepairRequest> GetAll(RepairRequestFilter filter)
        {
            var query = _context.RepairRequests.AsQueryable();

            if (filter.StartDate.HasValue)
                query = query.Where(x => x.CreationDate >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                query = query.Where(x => x.CreationDate <= filter.EndDate.Value);

            var result = query.ToList();

            return result;
        }

        public void Update(RepairRequest entity)
        {

            var existing = _context.RepairRequests.Find(entity.Id);
            if (existing == null)
                throw new InvalidOperationException($"Заявка с ID {entity.Id} не найдена");

            existing.CopyFrom(entity);
            
            existing.Id = entity.Id;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.RepairRequests.Find(id);
            if (entity == null)
                throw new InvalidOperationException($"Заявка с ID {id} не найдена");

            _context.RepairRequests.Remove(entity);
            _context.SaveChanges();
        }
    }
}