using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bim_base.data.CIM
{
    internal class HistoryItem
    {
        public HistoryItem() { }

        public HistoryItem(DateTime receivedTime, string id, string message)
        {
            ReceivedTime = receivedTime;
            ID = id;
            Message = message;
        }   

        public DateTime ReceivedTime { get; set; } = default(DateTime);

        public string ID { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
