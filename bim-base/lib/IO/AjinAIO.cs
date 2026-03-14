using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using static CNetworkPF;

public class AjinAIO
{
    static int LENGTH = 4;

    bool m_simualtion = false;

    int[] channel = new int[LENGTH];
    double[] m_input = new double[LENGTH];

    public AjinAIO()
    {
        string path = Common.PATH + "\\simulation";

        if (File.Exists(path))
            m_simualtion = true;

        for (int i = 0; i < LENGTH; i++)
            channel[i] = i;
    }

    public void readVoltage()
    {
        if (m_simualtion)
            return;

        uint result = CAXA.AxaiSwReadMultiVoltage(LENGTH, channel, m_input);

        if (result != 0)
        {
            Debug.debug("AjinAIO::allInput AxaiSwReadMultiVoltage error:" + result);
        }
    }

    public double input(int index)
    {
        if (index < 0 || index > (LENGTH - 1))
        {
            return 0.0d;
        }

        return m_input[index];
    }
} // class
