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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(RepairRequest entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

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

            // Попробуйте материализовать с отладкой:
            var result = query.ToList();

            // Проверка типов
            foreach (var item in result.Take(3))
            {
                Console.WriteLine($"DB Id: {item.Id}, Type in C#: {item.Id.GetType()}");
            }

            return result;
        }

        public void Update(RepairRequest entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Безопасное преобразование ID
            int id;
            try
            {
                id = Convert.ToInt32(entity.Id);
                Console.WriteLine($"Update: Converting Id {entity.Id} ({entity.Id.GetType()}) to int: {id}");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Некорректный ID: {entity.Id}. Ожидается число.", ex);
            }

            var existing = _context.RepairRequests.Find(id);
            if (existing == null)
                throw new InvalidOperationException($"Заявка с ID {id} не найдена");

            existing.CopyFrom(entity);
            // Сохраняем оригинальный ID
            existing.Id = id;

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