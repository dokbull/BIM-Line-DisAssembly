using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battery_attach
{
    public class CSerial_DACELL_DN70 : CModbus
    {
        READ_DATA m_readMeasData = new READ_DATA("NetDisplayValue", 30, 2);

        public CSerial_DACELL_DN70(TYPE type, SerialPort port, byte sloave) :
            base(type, port, sloave)
        {
            addReadData(m_readMeasData);
        }
    }
}
