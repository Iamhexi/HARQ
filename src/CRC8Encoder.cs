using System;

class CRC8Encoder : Encoder
{
    public Packet Encode(Packet message)
    {
        string msg = message.ToString();
        CRC8Calc calc = new CRC8Calc();
        string crc = calc.checksum(msg);
        for(int i = 0; i < crc.Length; i++)
        {
            if(crc[i] == '0')
            {
                message.Content.AttachZero();
            }
            else
            {
                message.Content.AttachOne();
            }
        }
        return message;
    }
}
        