using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

class CNetworkClientFocusBlue : CNetworkClient
{
    public CNetworkClientFocusBlue(string ip, int port)
        : base(ip, port)
    {
    }

    public CNetworkClientFocusBlue(Socket socket, string ip, int port)
        : base(socket, ip, port)
    {
    }

    public new void sendString(string packet)
    {
        base.clearSendQueue();
        base.sendString(packet);
    }
}
