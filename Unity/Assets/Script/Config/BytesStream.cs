using UnityEngine;
using System;

namespace Script.Config
{
    public class BytesStream
    {
        static int DefaultSize = 64;
        static int packSize = 64;

        public byte[] Buf;
        public int Pos;
        public int Used;
        public int Capacity;

        public BytesStream()
        {
            RestBytes();
        }

        public byte[] GetUsedBytes()
        {
            byte[] bytes = new byte[Used];
            Buffer.BlockCopy(Buf, 0, bytes, 0, Used);
            return bytes;
        }

        public int GetUsed()
        {
            return Used;
        }

        public void Clear()
        {
            Pos = 0;
            Used = 0;
        }

        public BytesStream(byte[] bytes)
        {
            RestBytes(bytes);
        }

        public BytesStream(int size)
        {
            Buf = new byte[size];
            Pos = 0;
            Used = 0;
            Capacity = size;
        }

        public BytesStream(byte[] bytes, int pos, int Used)
        {
            Buf = bytes;
            Pos = 0;
            Used = Buf.Length;
            Capacity = Used;
        }

        public void RestBytes(byte[] bytes)
        {
            Buf = bytes;
            Pos = 0;
            Used = Buf.Length;
            Capacity = Used;
        }

        public void RestBytes()
        {
            Buf = new byte[DefaultSize];
            Pos = 0;
            Used = 0;
            Capacity = DefaultSize;
        }

        public void SetPos(int pos)
        {
            this.Pos = pos;
        }

        public void MovePos(int pos)
        {
            this.Pos += pos;
        }

        public int bytesAvailable
        {
            get
            {
                return Used - Pos;
            }
        }

        public int read()
        {
            return 0;
        }

        public void Grow(int size)
        {
            int left = Capacity - Pos;
            if (left < size)
            {
                int need = size - left;
                float p = (float)need / packSize;
                int pa = Mathf.CeilToInt(p);
                Capacity += (int)(Mathf.CeilToInt((float)need / packSize)) * packSize;
                byte[] newBuf = new byte[Capacity];
                if (Used > 0)
                {
                    Array.Copy(Buf, newBuf, Used);
                }
                Buf = newBuf;
            }
        }

        void Move(int size)
        {
            Pos += size;
            if (Pos > Used)
            {
                Used = Pos;
            }
        }

        public void Write(byte b1)
        {
            Grow(1);
            Buf[Pos] = b1;
            Move(1);
        }

        public void Write(byte[] p)
        {
            int len = p.Length;
            Grow(len);
            Buffer.BlockCopy(p, 0, Buf, Pos, len);
            Move(len);
        }

        public void Write(short n)
        {
            Grow(2);
            Buf[Pos + 0] = (byte)((n >> 8) & 0xff);
            Buf[Pos + 1] = (byte)(n & 0xff);
            Move(2);
        }

        public void Write(ushort n)
        {
            Grow(2);
            Buf[Pos + 0] = (byte)((n >> 8) & 0xff);
            Buf[Pos + 1] = (byte)(n & 0xff);
            Move(2);
        }

        public void Write(int n)
        {
            Grow(4);
            Buf[Pos + 0] = (byte)((n >> 24) & 0xff);
            Buf[Pos + 1] = (byte)((n >> 16) & 0xff);
            Buf[Pos + 2] = (byte)((n >> 8) & 0xff);
            Buf[Pos + 3] = (byte)(n & 0xff);
            Move(4);
        }

        public void Write(uint n)
        {
            Grow(4);
            Buf[Pos + 0] = (byte)((n >> 24) & 0xff);
            Buf[Pos + 1] = (byte)((n >> 16) & 0xff);
            Buf[Pos + 2] = (byte)((n >> 8) & 0xff);
            Buf[Pos + 3] = (byte)(n & 0xff);
            Move(4);
        }

        public void Write(long n)
        {
            //Write(BitConverter.GetBytes(n));
            Grow(8);
            Buf[Pos + 0] = (byte)((n >> 54) & 0xff);
            Buf[Pos + 1] = (byte)((n >> 48) & 0xff);
            Buf[Pos + 2] = (byte)((n >> 40) & 0xff);
            Buf[Pos + 3] = (byte)((n >> 32) & 0xff);
            Buf[Pos + 4] = (byte)((n >> 24) & 0xff);
            Buf[Pos + 5] = (byte)((n >> 16) & 0xff);
            Buf[Pos + 6] = (byte)((n >> 8) & 0xff);
            Buf[Pos + 7] = (byte)(n & 0xff);
            Move(8);
            //if (Pos > Used) {
            //    Used = Pos;
            //}
        }

        public void Write(ulong n)
        {
            Write(BitConverter.GetBytes(n));
        }

        public void Write(float n)
        {
            Write(BitConverter.GetBytes(n));
        }

        public void Write(double n)
        {
            Write(BitConverter.GetBytes(n));
        }

        public void Write(string str)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
            Write(bytes);
        }

        public void WriteStringByte(string str)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
            Write((byte)bytes.Length);
            Write(bytes);
        }

        public void WriteStringShort(string str)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
            Write((short)bytes.Length);
            Write(bytes);
        }

        public bool ReadBoolean()
        {
            Pos++;
            return Convert.ToBoolean(Buf[Pos - 1]);
        }
        public byte ReadByte()
        {
            Pos++;
            return Buf[Pos - 1];
        }
        public byte[] ReadBytes(int count)
        {
            byte[] bytes = new byte[count];
            Buffer.BlockCopy(Buf, Pos, bytes, 0, count);
            Pos += count;
            return bytes;
        }
        public char ReadChar()
        {
            return (char)ReadByte();
        }
        public double ReadDouble()
        {
            byte[] bytes = ReadBytes(8);
            return Convert.ToDouble(bytes);
        }
        public short ReadInt16()
        {
            int a = (int)ReadByte();
            int b = (int)ReadByte();
            return (short)((a << 8) | b);
        }
        public ushort ReadUInt16()
        {
            int a = (int)ReadByte();
            int b = (int)ReadByte();
            return (ushort)((a << 8) | b);
        }
        public int ReadInt32()
        {
            int c1 = (int)ReadByte();
            int c2 = (int)ReadByte();
            int c3 = (int)ReadByte();
            int c4 = (int)ReadByte();
            //Debug.Log("pos:" + Pos + "read int:" + c1 + ", " + c2 + ", " + c3 + ", " + c4);
            return (c1 << 24) | (c2 << 16) | (c3 << 8) | c4;

        }
        public uint ReadUInt32()
        {
            uint c1 = (uint)ReadByte();
            uint c2 = (uint)ReadByte();
            uint c3 = (uint)ReadByte();
            uint c4 = (uint)ReadByte();
            return (c1 << 24) | (c2 << 16) | (c3 << 8) | c4;
        }
        public long ReadInt64()
        {
            long c1 = (long)ReadByte();
            long c2 = (long)ReadByte();
            long c3 = (long)ReadByte();
            long c4 = (long)ReadByte();
            long c5 = (long)ReadByte();
            long c6 = (long)ReadByte();
            long c7 = (long)ReadByte();
            long c8 = (long)ReadByte();
            
            return (c1 << 56) | (c2 << 48) | (c3 << 40) | (c4 << 32) | (c5 << 24) | (c6 << 16) | (c7 << 8) | c8;
            //return Convert.ToInt64(ReadBytes(8));
        }
        public float ReadSingle()
        {
            return Convert.ToSingle(ReadBytes(8));
        }
        public string ReadString()
        {
            int len = (int)ReadByte();
            byte[] bytes = ReadBytes(len);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
        public string ReadString(int len)
        {
            byte[] bytes = ReadBytes(len);
            return System.Text.Encoding.UTF8.GetString(bytes);
            //return System.Text.Encoding.Default.GetString(bytes);
        }
        public ulong ReadUInt64()
        {
            return Convert.ToUInt64(ReadBytes(8));
        }

        public void Append(BytesStream recvBuf)
        {
            Grow(recvBuf.Used);

            int left = Capacity - Used;
            if (left < recvBuf.Used)
            {
                Capacity = Used + recvBuf.Used;
                byte[] newBuf = new byte[Capacity];
                if (Used > 0)
                {
                    Array.Copy(Buf, newBuf, Used);
                }
                Array.Copy(recvBuf.Buf, 0, newBuf, Used, recvBuf.Used);
                Buf = null;
                Buf = newBuf;
            }
            else
            {
                Array.Copy(recvBuf.Buf, 0, Buf, Used, recvBuf.Used);
            }
            
            Used += recvBuf.Used;
        }
    }
}
