using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DIO : BaseDIO
{
    int m_inputCount = 0;
    int m_outputCount = 0;

    bool[] m_input = null;
    bool[] m_output = null;

    public DIO(int inputCount, int outputCount)
    {
        m_inputCount = inputCount;
        m_outputCount = outputCount;

        m_input = new bool[inputCount];
        m_output = new bool[outputCount];
    }

    public int inputCount()
    {
        return m_inputCount;
    }

    public int outputCount()
    {
        return m_outputCount;
    }

    public void setInputValue(int[] array)
    {
        int arrayCount = m_inputCount / 16;

        if (array.Length != arrayCount)
        {
            throw new ArgumentOutOfRangeException("데이터가 맞지 않습니다");
        }

        int count = 0;

        for (int i = 0; i < array.Length; i++)
        {
            List<bool> list = Util.toBit(array[i]);

            foreach (bool value in list)
            {
                m_input[count] = value;
                count++;
            }
        }
    }

    public override bool[] allInput()
    {
        return m_input;
    }

    public override bool[] allOutput()
    {
        return m_output;
    }

    public override bool input(int index)
    {
        if (index < 0 || index > m_inputCount - 1)
        {
            throw new ArgumentOutOfRangeException("DIO::input invalid index:" + index);
        }

        return m_input[index];
    }

    public override bool output(int index)
    {
        if (index < 0 || index > m_outputCount - 1)
        {
            throw new ArgumentOutOfRangeException("DIO::output invalid index:" + index);
        }

        return m_output[index];
    }

    public override void setOutput(int index, bool value)
    {
        if (index < 0 || index > m_outputCount - 1)
        {
            throw new ArgumentOutOfRangeException("DIO::setOutput invalid index:" + index);
        }

        //Debug.debug("DIO::setOutput index:" + index + " value:" + value);
        m_output[index] = value;
    } 

    public override void setOutput(bool[] valueArray)
    {
        if (valueArray.Length != m_outputCount)
        {
            throw new ArgumentException("DIO::setOutput invalid length.");
        }

        //Debug.debug("DIO::setOutput");

        for (int i = 0; i < m_outputCount; i++)
        {
            m_output[i] = valueArray[i];
        }
    }
}