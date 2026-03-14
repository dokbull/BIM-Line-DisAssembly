#if USE_IJ
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using lj;

public class LabjackU12
{
    static bool m_debug = true;

        public LabjackU12()
        {
            int[] idList = GetAllLabJacks();

            if (idList.Length == 0)
            {
                throw new InvalidOperationException("연결된 LABJACK 이 없습니다.");
            }
        }

        ~LabjackU12()
        {
        }

        //---------------------------------------------------------------------
        //
        public static void ThrowErrorMessage(string msg, int errorCode)
        {
            StringBuilder errorString = new StringBuilder(50);
            LabJack.GetErrorString(errorCode, errorString);
            Console.WriteLine("U12Wrapper::ThrowErrorMessage msg:" + errorString);
            //throw new Exception(msg + ":\r\n\r\n" + errorString);
        }

        //---------------------------------------------------------------------
        // This returns an array of all the local IDs which we use.
        //
        public static int[] GetAllLabJacks()
        {
            // Make sure we allocate space for what is passed
            int[] productIDList = new int[127];
            int[] serialNumList = new int[127];
            int[] localIDList = new int[127];
            int[] powerList = new int[127];
            int[,] calMatrix = new int[127, 20];
            int numFound = 0;
            int reserved1 = 0, reserved2 = 0;

            // Call the ListAll function.  We must use the keyword ref for parameters 
            // that aren't arrays that return data
            int result = LabJack.ListAll(productIDList, serialNumList, localIDList,
                powerList, calMatrix, ref numFound, ref reserved1, ref reserved2);
            if (result != 0)
                ThrowErrorMessage("Unable to enumerate controllers", result);

            int[] ljs = new int[numFound];
            int i = 0;

            // count how many we found and set
            // the array which will be returned
            // to contain valid IDs
            foreach (int id in localIDList)
            {
                if (id != 9999)
                {
                    ljs[i] = id;
                    ++i;
                }
            }

            // return that array
            return ljs;
        }

        //---------------------------------------------------------------------
        // This is our function that read's analog inputs
        //
        public static float ReadAnalogInput(int ljID, int channel)
        {
            int overVoltage = 0;
            float voltage = 0.0f;

            int result = 0;

            try
            {
                result = LabJack.EAnalogIn(ref ljID, 0, channel, 0, ref overVoltage, ref voltage);
            }
            catch (Exception e)
            {
                Debug.debug("U12Wrapper::ReadAnalogInput error:" + e.Message);
            }

            if (result != 0)
                ThrowErrorMessage("Error reading analog input", result);

            return voltage;
        }

        public static float[] aiSample(int numScans)
        {
            int ljID = 0;
            int stateIOin = 0;
            int[] channels = { 0, 2, 4, 6 };
            int[] gains = { 0, 0, 0, 0 };
            int overVoltage = 0;
            float scanRate = 2048;
            float[] voltages = new float[4];
            int[] stateIOout = new int[4];

            int result = LabJack.AISample(ref ljID, 0, 
                ref stateIOin, 
                0,
                0, //ledOn 
                4,
                channels,
                gains,
                0, // disableCAL
                ref overVoltage,
                voltages);

            if (result != 0)
                ThrowErrorMessage("Error setting digital output", result);

            Debug.debug(voltages[0] + " " + voltages[1] + " " + voltages[2] + " " + voltages[3]);

            return voltages;
        }

        //---------------------------------------------------------------------
        // Set the analog outputs
        //
        public static void SetAnalogOutput(int channel, float voltage)
        {
            int ljID = -1;
            int result = 0;

            if (channel == 0)
                result = LabJack.EAnalogOut(ref ljID, 0, voltage, -1.0f);
            else if (channel == 1)
                result = LabJack.EAnalogOut(ref ljID, 0, -1.0f, voltage);
            else
                throw new Exception("Invalid analog output channel");

            if (result != 0)
                ThrowErrorMessage("Error reading analog input", result);
        }

        //---------------------------------------------------------------------
        // Read inputs and set stateD and stateIO
        //
        public static void ReadDigitalInputs(ref int stateD, ref int stateIO)
        {
            int ljID = -1;
            int trisD = 0, trisIO = 0;
            int outputD = 0;

            int result = LabJack.DigitalIO(ref ljID, 0, ref trisD, trisIO, ref stateD, ref stateIO, 0, ref outputD);
            if (result != 0)
                ThrowErrorMessage("Error reading digital inputs", result);
        }

        //---------------------------------------------------------------------
        // Read digital input
        //
        public static int ReadDigitalInput(int channel, bool readD)
        {
            int ljID = -1;
            int state = 0;

            int result = LabJack.EDigitalIn(ref ljID, 0, channel, readD ? 1 : 0, ref state);
            if (result != 0)
                ThrowErrorMessage("Error reading digital input", result);

            return state;
        }

        //---------------------------------------------------------------------
        // Set Digital Outputs
        //
        public static void SetDigitalOutput(int channel, bool writeD, int state)
        {
            int ljID = -1;

            int result = LabJack.EDigitalOut(ref ljID, 0, channel, writeD ? 1 : 0, state);
            if (result != 0)
                ThrowErrorMessage("Error setting digital output", result);
        }

        //---------------------------------------------------------------------
        // AIBurst
        //
        public static float[,] AIBurst(int numScans)
        {
            int ljID = -1;
            int stateIOin = 0;
            int[] channels = { 0, 0, 0, 0 };
            int[] gains = { 0, 0, 0, 0 };
            float scanRate = 2048;
            float[,] voltages = new float[4096, 4];
            int[] stateIOout = new int[4096];
            int overVoltage = 0;

            int result = LabJack.AIBurst(ref ljID, 0, stateIOin, 0,
                1, 1,
                channels,
                gains,
                ref scanRate,
                0, 0, 0,
                numScans, 5,
                voltages,
                stateIOout,
                ref overVoltage,
                2);

            if (result != 0)
                ThrowErrorMessage("Error setting digital output", result);

            return voltages;
        }

        //---------------------------------------------------------------------
        // AIStream
        //
        public static float[] AIStream(int ljID = -1, int numScans = 250, int numIts = 5, bool start = false, bool end = false)
        {
            int error = 0;
            int demo = 0, numChannels = 4, disableCal = 0;
            int[] channels = { 0, 2, 4, 6 };
            int[] gains = { 0, 0, 0, 0 };
            float sr = 250.0F;
            int timeout = 10;
            float[,] voltages = new float[4096, 4];
            int[] stateIOout = new int[4096];
            int ljb = -1;
            int ov = -1;
            int reserved = 0;

            float[] total = new float[channels.Length];
            float[] average = new float[channels.Length];

            if (start)
            {
                Console.WriteLine("AIStream");
                error = LabJack.AIStreamStart(ref ljID, demo, 0, 0, 1, numChannels, channels,
                    gains, ref sr, disableCal, 0, 0);
                if (error != 0)
                {
                    Console.WriteLine("AIStreamStart Error: {0}", error);
                }
            }

            int i = 0;
            while ((error == 0) && (i < numIts))
            {
                for (int j = 0; j < 4096; j++)
                {
                    stateIOout[j] = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        voltages[j, k] = 0;
                    }
                }

                error = LabJack.AIStreamRead(ljID, numScans, timeout, voltages, stateIOout, ref reserved, ref ljb, ref ov);
                if (error != 0)
                {
                    Console.WriteLine("AIStreamRead Error:", error);
                }

                if (false) // debug code
                {
                    for (int j = 0; j < 250; j++)
                        Console.WriteLine(j + " Scan:  V1={0}, V2={1}, V3={2}, V4={3}", voltages[j, 0], voltages[j, 1], voltages[j, 2], voltages[j, 3]);
                }

                Console.WriteLine("LabJack Scan Backlog = {0}", ljb);
                i++;
            }

            for (int k = 0; k < channels.Length; k++)
            {
                total[k] = 0;
                average[k] = 0;

                for (int j = 0; j < numIts * numScans; j++)
                    total[k] += voltages[j, k];

                average[k] = total[k] / (float)(numIts * numScans);
            }

            if (end)
            {
                LabJack.AIStreamClear(ljID);
            }

            return average;
        }
}
#endif