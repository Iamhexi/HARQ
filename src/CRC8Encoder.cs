using System;

class CRC8Encoder : Encoder
{
    public Packet Encode(Packet message)
    {
        string msg = message.GetHeader() + message.Content;
        CRC8Calc calc = new CRC8Calc();
        string crc = calc.checksum(msg);

        for(int i = 0; i < crc.Length; i++)
        {
            if(crc[i] == '0')
            {
                message.DetectionCode.AttachZero();
            }
            else
            {
                message.DetectionCode.AttachOne();
            }
        }
        return message;
    }
}
        