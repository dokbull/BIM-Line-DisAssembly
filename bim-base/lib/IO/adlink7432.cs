using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ADLINK - 7432
public class Adlink7432 : BaseDIO
{
    private static int INPUT_COUNT = 32;
    private static int OUTPUT_COUNT = 32;

    private short m_dev = -1;
    private ushort m_cardNumber = 0;
    private bool m_existBoard = false;

    public Adlink7432(ushort cardNumber = 0)
    {
        m_cardNumber = cardNumber;

        try
        {
            m_dev = DASK.Register_Card(DASK.PCI_7432, m_cardNumber);

            if (m_dev < 0)
            {
                Debug.warning("Adlink7432::Adlink7432 not found dio card");

                return;
            }

            m_existBoard = true;
        }
        catch (Exception ex)
        {
            Debug.warning("Adlink7432::Adlink7432 exception error:" + ex.Message);
        }
    }

    ~Adlink7432()
    {
        short ret;

        if (m_dev > -1)
            ret = DASK.Release_Card((ushort)m_dev);
    }

    public bool existBoard()
    {
        return m_existBoard;
    }

    public override bool[] allInput()
    {
        bool[] array = new bool[INPUT_COUNT];

        if (m_existBoard == false)
            return array;

        for (int i = 0; i < INPUT_COUNT; i++)
            array[i] = false;

        uint value = 0;
        short ret = DASK.DI_ReadPort((ushort)m_dev, 0, out value);

        if (ret < 0)
            return array;

        if (value == 0)
            return array;

        // 101011 이런식으로 전달되기 때문에 파싱한다
        string diText = Convert.ToString(value, 2).PadLeft(INPUT_COUNT, '0');

        for (int i = 0; i < INPUT_COUNT; i++)
        {
            if (diText.Substring(INPUT_COUNT - 1 - i, 1) == "1")
                array[i] = true;
        }

        return array;
    }

    public override bool[] allOutput()
    {
        bool[] array = new bool[OUTPUT_COUNT];

        if (m_existBoard == false)
            return array;

        for (int i = 0; i < OUTPUT_COUNT; i++)
            array[i] = false;

        uint value = 0;
        short ret = DASK.DO_ReadPort((ushort)m_dev, 0, out value);

        if (ret < 0)
            return array;

        if (value == 0)
            return array;

        // 101011 이런식으로 전달되기 때문에 파싱한다
        string diText = Convert.ToString(value, 2).PadLeft(OUTPUT_COUNT, '0');

        for (int i = 0; i < OUTPUT_COUNT; i++)
        {
            if (diText.Substring(OUTPUT_COUNT - 1 - i, 1) == "1")
                array[i] = true;
        }

        return array;
    }

    public override bool input(int index)
    {
        if (m_existBoard == false)
            return false;

        if (index < 0 || index > INPUT_COUNT - 1)
        {
            Debug.warning("Adlink7432::input invalid index:" + index);
            return false;
        }

        ushort value = 0;

        short ret = DASK.DI_ReadLine((ushort)m_dev, 0, (ushort)index, out value);

        if (ret < 0)
            return false;

        if (value == 0)
            return false;

        return true;
    }

    public override bool output(int index)
    {
        if (m_existBoard == false)
            return false;

        if (index < 0 || index > OUTPUT_COUNT - 1)
        {
            Debug.warning("Adlink7432::output invalid index:" + index);
            return false;
        }

        ushort value = 0;

        short ret = DASK.DO_ReadLine((ushort)m_dev, 0, (ushort)index, out value);

        if (ret < 0)
            return false;

        if (value == 0)
            return false;

        return true;
    }

    public override void setOutput(int index, bool value)
    {
        if (m_existBoard == false)
            return;

        if (index < 0 || index > OUTPUT_COUNT - 1)
        {
            Debug.warning("Adlink7432::setOutput invalid index:" + index);
            return;
        }

        ushort convertValue = 0;

        if (value == true)
            convertValue = 1;

        short ret = DASK.DO_WriteLine((ushort)m_dev, 0, (ushort)index, convertValue);

        if (ret < 0)
        {
            Debug.warning("Adlink7432::setOutput error result:" + ret);
        }
    }

    public override void setOutput(bool[] valueArray)
    {
        // 미구현
    }

    public bool isConnected()
    {
        return m_existBoard;
    }

    public void update(ref bool[] input, ref bool[] output)
    {
        bool[] retInput = allInput();
        bool[] retOutput = allOutput();

        if (input.Length != INPUT_COUNT)
        {
            Debug.warning("Adlink7432::update error by input length:" + input.Length);
            return;
        }

        if (retInput.Length != INPUT_COUNT)
        {
            Debug.warning("Adlink7432::update error by retInput length:" + retInput.Length);
            return;
        }

        if (output.Length != OUTPUT_COUNT)
        {
            Debug.warning("Adlink7432::update error by output length:" + output.Length);
            return;
        }

        if (retOutput.Length != OUTPUT_COUNT)
        {
            Debug.warning("Adlink7432::update error by retOutput length:" + retOutput.Length);
            return;
        }

        for (int i = 0; i < INPUT_COUNT; i++)
        {
            input[i] = retInput[i];
        }

        for (int i = 0; i < OUTPUT_COUNT; i++)
        {
            output[i] = retOutput[i];
        }
    }
}
