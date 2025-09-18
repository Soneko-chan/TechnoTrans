using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum RepairRequestType
    {
        New,
        InProgress,
        WaitingPairs,
        Done,
    }

    public static class RepairRequestTypeExtensions
    {
        public static string ToPrettyString(this RepairRequestType type)
        {
            switch (type)
            {
                case RepairRequestType.New:
                    return "Новая заявка";
                case RepairRequestType.InProgress:
                    return "В процессе ремонта";
                case RepairRequestType.WaitingPairs:
                    return "Ожидание запчастей";
                case RepairRequestType.Done:
                    return "Заявка завершена";
                default:
                    return "Новая заявка";
            }
        }
    }
}