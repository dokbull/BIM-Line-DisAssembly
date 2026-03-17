using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bim_base.data.CIM
{
    internal class CellDataInfo
    {
        public string CellID { get; set; } = string.Empty;

        public string ProductID { get; set; } = string.Empty;

        public string StepID { get; set; } = string.Empty;
    }

    internal class MessageData
    {
        public MessageData() { }
        public MessageData(string _ID,  string _Message)
        {
            this.ID = _ID;
            this.Message = _Message;
        }

        public string ID { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{this.ID} : {this.Message}";
        }

    }
}
