using System;

class CRC8Encoder : Encoder
{
    public Packet Encode(Packet message)
    {
        string msg = message.GetHeader() + message.Content.ToString();
        CRC8Calc calc = new CRC8Calc();
        string crc = calc.checksum(msg);
        message.DetectionCode = new BinaryString(crc);
        return message;
    }
}
        