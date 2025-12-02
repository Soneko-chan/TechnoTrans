using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Statistics;
using Data.Interfaces;

namespace Services
{
    public class StatisticsService
    {
        private readonly IRepairRequestRepository _repairRequestRepository;

        public StatisticsService(IRepairRequestRepository repairRequestRepository)
        {
            _repairRequestRepository = repairRequestRepository;
        }

        // Статистика по статусам заявок
        public List<StatusStatisticItem> GetByStatus(RepairRequestFilter filter)
        {
            var requests = _repairRequestRepository.GetAll(filter);

            return requests
                .GroupBy(r => r.Status)
                .Select(g => new StatusStatisticItem
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .OrderBy(s => s.Status)
                .ToList();
        }

        // Статистика по месяцам
        public List<MonthStatisticItem> GetByMonth(RepairRequestFilter filter)
        {
            var requests = _repairRequestRepository.GetAll(filter);

            return requests
                .GroupBy(r => new { r.CreationDate.Year, r.CreationDate.Month })
                .Select(g => new MonthStatisticItem
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(m => m.Year)
                .ThenBy(m => m.Month)
                .ToList();
        }

        // Статистика по механикам
        public List<MechanicStatisticItem> GetByMechanic(RepairRequestFilter filter)
        {
            var requests = _repairRequestRepository.GetAll(filter);

            return requests
                .Where(r => !string.IsNullOrEmpty(r.ResponsibleMechanic))
                .GroupBy(r => r.ResponsibleMechanic)
                .Select(g => new MechanicStatisticItem
                {
                    MechanicName = g.Key ?? "Не назначен",
                    Count = g.Count()
                })
                .OrderByDescending(m => m.Count)
                .ToList();
        }

        // Статистика по типам автомобилей
        public List<CarTypeStatisticItem> GetByCarType(RepairRequestFilter filter)
        {
            var requests = _repairRequestRepository.GetAll(filter);

            return requests
                .Where(r => !string.IsNullOrEmpty(r.CarType))
                .GroupBy(r => r.CarType)
                .Select(g => new CarTypeStatisticItem
                {
                    CarType = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.CarType)
                .ToList();
        }
    }
}