using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;

using lib.lightController.Jlail.DCV_35024;

namespace lib.network.lightController
{
    public class CSerialJDC
    {
        static int READ_BUFF_SIZE = 1024;

        public event EventHandler IncomingData;

        SerialPort m_serialPort = null;

        CircularBuffer m_recvBuffer = new CircularBuffer(1024 * 1024); // 1MB
        Queue<byte[]> m_sendQueue = new Queue<byte[]>();

        byte[] m_readBuffer = new byte[READ_BUFF_SIZE];

        Thread m_thread = null;
        bool m_stop = false;

        bool m_simulation = false;

        public CSerialJDC(SerialPort serialPort)
        {
            m_serialPort = serialPort;

            m_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

            if (File.Exists(pathUtil.savePath() + "\\simulation"))
                m_simulation = true;

            m_thread = new Thread(run);
        }

        ~CSerialJDC()
        {
            m_stop = true;
        }


        public void setChValue(int ch, int value)
        {
            string text = "";
            if (ch == 16)
                text = "$3" + "G" + value.ToString("X3");
            else
                text = "$3" + ch.ToString("X1") + value.ToString("X3");
            string checksum = calcChecksum(text);
            string sendData = text + checksum + "\r\n";

            send(ASCIIEncoding.ASCII.GetBytes(sendData));
        }

        public string calcChecksum(string data)
        {
            int checksum = 0;
            foreach (char c in data)
            {
                checksum ^= (byte)c;
            }
            return checksum.ToString("X2");
        }

        public void setAllOnOff(bool value)
        {
#if false
            string packet = PacketBuilder.SetBrightnessAll(value ? 100 : 0);
            byte[] data = ASCIIEncoding.ASCII.GetBytes(packet);

            send(data);
#endif
            string text;
            text = "$3000017"; //ALL 0
            //text = "$300FF17"; //ALL 255
            send(ASCIIEncoding.ASCII.GetBytes(text));
        }

        public bool isConnected()
        {
            return m_serialPort.IsOpen;
        }

        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = (SerialPort)sender;

            int n = serialPort.BytesToRead;

            for (int i = 0; i < 1024; i++)
                m_readBuffer[i] = 0;

            try
            {
                serialPort.Read(m_readBuffer, 0, n);

                lock (m_recvBuffer)
                    m_recvBuffer.write(m_readBuffer, (uint)n);
            }
            catch (Exception ex)
            {
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();

                Debug.debug("CSerial::dataReceived error reason:" + ex.Message);
            }
        }

        public void setSimulation(bool value)
        {
            m_simulation = value;
        }

        public void start()
        {
            m_thread.Start();
        }

        public void stop()
        {
            m_stop = true;
        }

        private void run()
        {
            Debug.debug("CSerialJDC::run START name:" + m_serialPort.PortName);

            while (true)
            {
                if (m_stop)
                {
                    Debug.debug("CSerialJDC::run STOP");
                    break;
                }

                if (m_simulation)
                {
                    sendProcess();
                    recvProcess();

                    Thread.Sleep(50);

                    continue;
                }

                if (!m_serialPort.IsOpen)
                {
                    try
                    {
                        m_serialPort.Open();
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(1000);
                    }
                }

                try
                {
                    sendProcess();
                    recvProcess();
                }
                catch (Exception ex)
                {
                    Debug.critical(ex,
                            "CSerialJDC::run port:" + m_serialPort.PortName);
                }

                Thread.Yield();
                Thread.Sleep(30);
            }

            Debug.debug("CSerialJDC::run END");
        }
        public void sendString(string data)
        {
            data = data + "\r\n";
            m_serialPort.Write(data);
        }
        public void send(byte[] data)
        {
            lock (m_sendQueue)
            {
                m_sendQueue.Enqueue(data);
            }
        }

        private void sendProcess()
        {
            lock (m_sendQueue)
            {
                if (m_sendQueue.Count == 0)
                    return;

                byte[] buff = m_sendQueue.Dequeue();
                m_serialPort.Write(buff, 0, buff.Length);
            }
        }

        private bool recvProcess()
        {
            lock (m_recvBuffer)
            {
                uint size = m_recvBuffer.size();

                if (size == 0)
                    return false;

                bool isStx = false;

                byte[] buff = new byte[m_recvBuffer.size()];
                m_recvBuffer.peek(ref buff, m_recvBuffer.size());

                byte[] data = new byte[buff.Length];
                m_recvBuffer.read(ref data, (uint)buff.Length);
                processIncmonig(data);

                return true;
            }
        }

        private void processIncmonig(byte[] data)
        {
            string text = "";

            for (int i = 0; i < data.Length; i++)
                text += data[i].ToString("X2") + " ";

            //Debug.debug("CSerial::processIncoming data:" + text);

            if (IncomingData != null)
                IncomingData(data, null);
        }
    }
}
