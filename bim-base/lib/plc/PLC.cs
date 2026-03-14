using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.plc
{
    public enum Addr
    {
        W = 0,
        D = 1,
        B = 2,
        X = 3,
        Y = 4,
    }

    public enum ReadWrite
    {
        READ = 0,
        WRITE = 1,
    }

    interface IPLC
    {
        bool open();
        bool close();

        bool read(Addr type, int address, int count, ref int[] buffer);
        bool write(Addr type, int address, int count, int[] value);

        //TODO@tmdwn..추후 확장된 인터페이스 가져가기 위함
#if false
        public abstract void initMultiple(ReadWrite readWriteEnum);

        public abstract void addMultipleRead(ReadWrite readWriteEnum, Addr type, int address, int count);
        public abstract void addMultipleWrite(ReadWrite readWriteEnum, Addr type, int address, int count, int[] writeData);

        public abstract bool readMultiple();
        public abstract bool writeMultiple();
#endif
    }
}
