using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class CNetwork_IV2 : CNetworkClient
{
    public static string CMD_TRG = "T2";
    public static string CMD_MODEL = "PW";

    public CNetwork_IV2(string ip, int port) : base(ip, port)
    {
       // this.recvData += CNetwork_IV2_recvData;
    }

    public CNetwork_IV2(Socket socket, string ip, int port) : base(socket, ip, port)
    {
    }

    void sendPacket(string message)
    {
        byte[] messageData = ASCIIEncoding.ASCII.GetBytes(message);
        byte[] packet = new byte[messageData.Length + 1];

        Array.Copy(messageData, packet, messageData.Length);
        packet[packet.Length - 1] = 0x0D; //CR

        send(packet);
    }

    public void sendModelNo(int modelNo)
    {
        sendPacket(CMD_MODEL + "," + modelNo.ToString("D3"));
    }

    public void sendTRG()
    {
        sendPacket(CMD_TRG);
    }

    private void CNetwork_IV2_recvData(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
    }
}
