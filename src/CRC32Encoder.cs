using System;
class CRC32Encoder : Encoder
{
    public Packet Encode(Packet message)
    {
        string msg = message.ToString();
        CRC32Calc calc = new CRC32Calc();
        string crc = calc.Compute_CRC32(msg);

        message.DetectionCode = new BinaryString(""); // clear previous detection code

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