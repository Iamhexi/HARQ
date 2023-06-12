using System;
public class CRC32Calc 
{
    private uint[] crcTable;
    public CRC32Calc()
    {
        CalculateCrcTable_CRC32();
    }
    public string Compute_CRC32(string message)
    {
        byte[] bytes = new byte[message.Length/ 8];

        for (int i = 0; i < bytes.Length; i++)
        {
            for (int b = 0; b <= 7; b++)
            {
                bytes[i] |= (byte)((message[i * 8 + b] == '1' ? 1 : 0) << (7 - b));
            }
        }
        uint crc = 0;
        foreach (byte b in bytes)
        {
            byte pos = (byte)((crc ^ (b << 24)) >> 24);
            crc = (uint)((crc << 8) ^ (uint)(crcTable[pos]));
        }
        return Convert.ToString(crc, 2).PadLeft(32, '0');
    }    
    private void CalculateCrcTable_CRC32()
    {
        const uint polynomial = 0x04C11DB7;
        crcTable = new uint[256];

        for (int divident = 0; divident < 256; divident++)
        {
            uint curByte = (uint)(divident << 24);
            for (byte bit = 0; bit < 8; bit++)
            {
                if ((curByte & 0x80000000) != 0)
                {
                    curByte <<= 1;
                    curByte ^= polynomial;
                }
                else
                {
                    curByte <<= 1;
                }
            }

            crcTable[divident] = curByte;
        }
    } 
}