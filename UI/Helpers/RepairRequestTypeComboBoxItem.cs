using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Helpers
{
    internal class RepairRequestTypeComboBoxItem
    {
        public required RepairRequestType Value { get; init; }
        public required string Text { get; init; }
    }
}
