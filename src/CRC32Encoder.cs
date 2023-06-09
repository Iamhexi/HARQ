using System;
class CRC32Encoder : Encoder
{
    public Packet Encode(Packet message)
    {
        string msg = message.GetHeader() + message.Content.ToString();
        CRC32Calc calc = new CRC32Calc();
        string crc = calc.Compute_CRC32(msg);
        message.DetectionCode = new BinaryString(crc);
        return message;
    }
}