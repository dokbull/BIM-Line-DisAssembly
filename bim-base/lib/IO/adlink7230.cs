using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ADLINK - 7230
public class Adlink7230 : BaseDIO
{
    private static int INPUT_COUNT = 16;
    private static int OUTPUT_COUNT = 16;

    private short m_dev = -1;
    private ushort m_cardNumber = 0;
    private bool m_existBoard = false;

    public Adlink7230(ushort cardNumber = 0)
    {
        m_cardNumber = cardNumber;

        m_dev = DASK.Register_Card(DASK.PCI_7230, m_cardNumber);

        if (m_dev < 0)
        {
            Debug.warning("Adlink7230::Adlink7230 not found dio card");
            return;
        }

        m_existBoard = true;
    }

    ~Adlink7230()
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

        for (int i = 0; i < INPUT_COUNT; i++)
            array[i] = false;

        if (m_existBoard == false)
            return array;

        uint value = 0;
        short ret = DASK.DI_ReadPort((ushort)m_dev, 0, out value);

        if (ret < 0)
            return array;

        if (value == 0)
            return array;

        // 101011 이런식으로 전달되기 때문에 파싱한다
        string diText = Convert.ToString(value, 2).PadLeft(INPUT_COUNT, '0');

        for (int i = 0; i < 16; i++)
        {
            if (diText.Substring(INPUT_COUNT - 1 - i, 1) == "1")
                array[i] = true;
        }

        return array;
    }

    public override bool[] allOutput()
    {
        bool[] array = new bool[OUTPUT_COUNT];

        for (int i = 0; i < OUTPUT_COUNT; i++)
            array[i] = false;

        if (m_existBoard)
        {
            uint value = 0;
            short ret = DASK.DO_ReadPort((ushort)m_dev, 0, out value);

            if (ret < 0)
                return array;

            if (value == 0)
                return array;

            // 101011 이런식으로 전달되기 때문에 파싱한다
            string diText = Convert.ToString(value, 2).PadLeft(OUTPUT_COUNT, '0');

            for (int i = 0; i < 16; i++)
            {
                if (diText.Substring(OUTPUT_COUNT - 1 - i, 1) == "1")
                    array[i] = true;
            }
        }

        return array;
    }

    public override bool input(int index)
    {
        if (index < 0 || index > INPUT_COUNT - 1)
        {
            Debug.warning("AdLink7230::input invalid index:" + index);
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
        if (index < 0 || index > OUTPUT_COUNT - 1)
        {
            Debug.warning("AdLink7230::output invalid index:" + index);
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
        if (index < 0 || index > OUTPUT_COUNT - 1)
        {
            Debug.warning("Adlink7230::setOutput invalid index:" + index);
            return;
        }
    }

    public override void setOutput(bool[] valueArray)
    {
    }
}
