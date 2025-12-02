using System;
using Domain;

namespace Data.Interfaces
{
    public record RepairRequestFilter
    {
        public static RepairRequestFilter Empty => new();

        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public RepairRequestStatus? Status { get; init; }
        public string? MechanicName { get; init; }
    }
}