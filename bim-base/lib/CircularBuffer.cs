using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

class CircularBuffer
{
    object lockObj = new object();

    byte[] data_ = null;
    uint beg_index_, end_index_, size_, capacity_;

    public CircularBuffer(uint capacity)
    {
        beg_index_ = 0;
        end_index_ = 0;
        size_ = 0;
        capacity_ = capacity;
        data_ = new byte[capacity];
    }

    private void memcpy(ref byte[] dest, uint destOffset, byte[] src, uint srcOffset, uint count)
    {
        for (int i = 0; i < count; i++)
        {
            if (dest.Length < (destOffset + i)) // 데이터 영역 체크
            {
                Debug.warning("CircularBuffer::memcpy dest data out of range. length:" + dest.Length + " offset" + destOffset + " index:" + i);
                break;
            }
            if (src.Length < (srcOffset + i)) // 데이터 영역 체크
            {
                Debug.warning("CircularBuffer::memcpy src data out of range. length:" + src.Length + " offset" + srcOffset + " index:" + i);
                break;
            }

            dest[destOffset+i] = src[srcOffset+i];
        }
    }

    public uint size()
    {
        lock (lockObj)
        {
            return size_;
        }
    }

    public uint capacity()
    {
        lock (lockObj)
        {
            return capacity_;
        }
    }

    public uint write(byte[] data, uint bytes)
    {
        lock (lockObj)
        {
            if (bytes == 0) return 0;

            uint capacity = capacity_;
            uint bytes_to_write = Math.Min(bytes, capacity - size_);

            // Write in a single step
            if (bytes_to_write <= capacity - end_index_)
            {
                memcpy(ref data_, end_index_, data, 0, bytes_to_write);
                end_index_ += bytes_to_write;
                if (end_index_ == capacity) end_index_ = 0;
            }
            // Write in two steps
            else
            {
                uint size_1 = capacity - end_index_;
                memcpy(ref data_, end_index_, data, 0, size_1);
                uint size_2 = bytes_to_write - size_1;
                memcpy(ref data_, 0, data, size_1, size_2);
                end_index_ = size_2;
            }

            size_ += bytes_to_write;
            return bytes_to_write;
        }
    }

    public uint peek(ref byte[] data, uint bytes)
    {
        lock (lockObj)
        {
            if (bytes == 0) return 0;

            uint capacity = capacity_;
            uint bytes_to_read = Math.Min(bytes, size_);

            // Read in a single step
            if (bytes_to_read <= capacity - beg_index_)
            {
                memcpy(ref data, 0, data_, beg_index_, bytes_to_read);
                //beg_index_ += bytes_to_read;
                //if (beg_index_ == capacity) beg_index_ = 0;
            }
            // Read in two steps
            else
            {
                uint size_1 = capacity - beg_index_;
                memcpy(ref data, 0, data_, beg_index_, size_1);
                uint size_2 = bytes_to_read - size_1;
                memcpy(ref data, size_1, data_, 0, size_2);
                //beg_index_ = size_2;
            }

            //size_ -= bytes_to_read;
            return bytes_to_read;
        }
    }

    public uint read(ref byte[] data, uint bytes)
    {
        lock (lockObj)
        {
            if (bytes == 0) return 0;

            uint capacity = capacity_;
            uint bytes_to_read = Math.Min(bytes, size_);

            // Read in a single step
            if (bytes_to_read <= capacity - beg_index_)
            {
                memcpy(ref data, 0, data_, beg_index_, bytes_to_read);
                beg_index_ += bytes_to_read;
                if (beg_index_ == capacity) beg_index_ = 0;
            }
            // Read in two steps
            else
            {
                uint size_1 = capacity - beg_index_;
                memcpy(ref data, 0, data_, beg_index_, size_1);
                uint size_2 = bytes_to_read - size_1;
                memcpy(ref data, size_1, data_, 0, size_2);
                beg_index_ = size_2;
            }

            size_ -= bytes_to_read;
            return bytes_to_read;
        }
    }

    public void clear()
    {
        lock (lockObj)
        {
            for (int i = 0; i < data_.Length; i++)
                data_[i] = 0;

            beg_index_ = 0;
            end_index_ = 0;
            size_ = 0;
        }
    }
}
