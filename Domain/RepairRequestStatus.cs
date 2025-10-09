using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum RepairRequestStatus
    {
        New,
        InProgress,
        WaitingPairs,
        Done,
    }

    public static class RepairRequestStatusExtensions
    {
        public static string ToPrettyString(this RepairRequestStatus type)
        {
            switch (type)
            {
                case RepairRequestStatus.New:
                    return "Новая заявка";
                case RepairRequestStatus.InProgress:
                    return "В процессе ремонта";
                case RepairRequestStatus.WaitingPairs:
                    return "Ожидание запчастей";
                case RepairRequestStatus.Done:
                    return "Заявка завершена";
                default:
                    return "Новая заявка";
            }
        }
    }
}