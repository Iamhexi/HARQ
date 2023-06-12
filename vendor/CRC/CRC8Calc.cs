using System;

//source: http://www.sunshine2k.de/articles/coding/crc/understanding_crc.html

public class CRC8Calc {

    private const byte generator = 0x07;
    private byte[] crctable = new byte[256];

    public CRC8Calc()
    {
        generateTable();
    }

    public string checksum(string message){
        byte[] output = new byte[message.Length/ 8];

        for (int i = 0; i < output.Length; i++)
        {
            for (int b = 0; b <= 7; b++)
            {
                output[i] |= (byte)((message[i * 8 + b] == '1' ? 1 : 0) << (7 - b));
            }
        }

        byte crc = 0;
        foreach (byte b in output)
        {
            byte data = (byte)(b ^ crc);
            crc = (byte)(crctable[data]);
        }


        string crc8 = Convert.ToString(crc, 2).PadLeft(8, '0');
        return crc8;
    }

    private void generateTable()
    {
        for (int divident = 0; divident < 256; divident++)
        {
            byte currByte = (byte)divident;
            for (byte bit = 0; bit < 8; bit++)
            {
                if ((currByte & 0x80) != 0)
                {
                    currByte <<= 1;
                    currByte ^= generator;
                }
                else
                {
                    currByte <<= 1;
                }
            }
            crctable[divident] = currByte;
        }

    }
}