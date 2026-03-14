using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CDeviceManager
{
    // Key -> "X000", "Y001" 등.... 을 입력한다
    Dictionary<int, CDevice> m_outputDevice = new Dictionary<int, CDevice>();

    List<CDevice> m_deviceList = new List<CDevice>();

    public bool m_ready = false;

    bool m_isRun = false;
    bool m_isCycle = false;

    public void setCycle(bool value)
    {
        m_isCycle = value;
    }
    
    public void setRun(bool value)
    {
        m_isRun = value;
    }

    public bool isCycle()
    {
        return m_isCycle;
    }

    public bool isAutoRun()
    {
        return (m_isRun);
    }

    public bool append(CDevice device)
    {
        if (m_deviceList.Contains(device))
        {
            throw new InvalidProgramException("CDeviceManager::append duplicate list:" + device);
        }

        m_deviceList.Add(device);

        Type type = device.GetType();

        // only input device
        if (type == typeof(ProximitySensor))
            return true;

        // output device add
        if (device.GetType() == typeof(SingleCyl))
        {
            SingleCyl dev = (SingleCyl)device;
            int addr = dev.outputList()[0].address;

            if (addr == 0)
                throw new InvalidOperationException("CDeviceManager::append invalid addr");

            if (m_outputDevice.ContainsKey(addr))
            {
                Debug.warning("CDeviceManager::append duplicate key:" + addr);
            }

            try
            {
                m_outputDevice.Add(addr, device);
            }
            catch (Exception e)
            {
                Debug.warning("CDeviceManager::append error reason:" + e.Message);
            }
        }
        else if (device.GetType() == typeof(DoubleCyl))
        {
            DoubleCyl dev = (DoubleCyl)device;

            List<EContact> outputList = dev.outputList();

            int addr1 = outputList[0].address;
            int addr2 = outputList[1].address;

            try
            {
                m_outputDevice.Add(addr1, device);
                m_outputDevice.Add(addr2, device);
            }
            catch (Exception e)
            {
                Debug.warning("CDeviceManager::append error reason:" + e.Message);
            }
        }

        return true;
    }

    public void setInitValue(DIO dio)
    {
        foreach (KeyValuePair<int, CDevice> eter in m_outputDevice)
        {
            CDevice device = eter.Value;

            Type type = device.GetType();

            if (type == typeof(SingleCyl))
            {
                SingleCyl dev = (SingleCyl)device;
                dev.setOutput(dio.output(eter.Key));
            }
            else if (type == typeof(DoubleCyl))
            {
                DoubleCyl dev = (DoubleCyl)device;

                List<EContact> outputList = dev.outputList();

                for (int i = 0; i < 2; i++)
                {
                    if (outputList[i].address == eter.Key)
                        dio.setOutput(eter.Key, outputList[i].value);
                }
            }
            else if (type == typeof(ProximitySensor))
            {
            }
        }
    }

    public void setInitValue(int address, bool value)
    {
        foreach (KeyValuePair<int, CDevice> eter in m_outputDevice)
        {
            CDevice device = eter.Value;

            Type type = device.GetType();

            if (type == typeof(SingleCyl))
            {
                if (eter.Key == address)
                {
                    SingleCyl dev = (SingleCyl)device;
                    dev.setOutput(value);
                }
            }
            else if (type == typeof(DoubleCyl))
            {
                DoubleCyl dev = (DoubleCyl)device;

                List<EContact> outputList = dev.outputList();

                for (int i = 0; i < 2; i++)
                {
                    if (outputList[i].address == address)
                    {
                        dev.outputList()[i].value = value;
                    }
                }
            }
            else if (type == typeof(ProximitySensor))
            {
            }
        }
    }

    public void resetError()
    {
        foreach (CDevice device in m_deviceList)
        {
            device.errorReset();
        }
    }

    public void run(DIO dio)
    {
        // 검색해서 값 집어넣고 루프 돌리기

        foreach (CDevice device in m_deviceList)
        {
            foreach (EContact input in device.inputList())
            {
                input.value = dio.input(input.address);
            }

            device.run();

#if false
            if (device.isError() && device.errorConfirm() == false)
            {
                if (device.inputList().Count > 0)
                {
                    MainForm.inst().processMain().addAlarm(
                        (INPUT)device.inputList()[0].address);

                    device.setErrorConfirm();
                }
            }
#endif
        }

        outputRun(dio); 

#if false // 특정 상황에서만 io가 동작
        if (isAutoRun() || m_ready || isCycle() || m_isCycle)
            outputRun(dio);
#endif
    }

    public void outputRun(DIO dio)
    {
        // OUTPUT 의 경우는 자동일때에만 동작한다.
        foreach (KeyValuePair<int, CDevice> eter in m_outputDevice)
        {
            CDevice device = eter.Value;

            Type type = device.GetType();

            if (type == typeof(SingleCyl))
            {
                SingleCyl dev = (SingleCyl)device;
                dio.setOutput(eter.Key, dev.output());
            }
            else if (type == typeof(DoubleCyl))
            {
                DoubleCyl dev = (DoubleCyl)device;

                List<EContact> outputList = dev.outputList();

                for (int i = 0; i < 2; i++)
                {
                    if (outputList[i].address == eter.Key)
                        dio.setOutput(eter.Key, outputList[i].value);
                }
            }
            else if (type == typeof(ProximitySensor))
            {
            }
        }
    }
}
