using System;

class CRC8Decoder: DetectionDecoder
{
    public bool Decode(Packet encodedMessage)
    {
        CRC8Calc calc = new CRC8Calc();
        string dataWithoutCodes = encodedMessage.GetHeader() + encodedMessage.Content.ToString();
        string crc = calc.checksum(dataWithoutCodes);

        return new BinaryString(crc).Equals(encodedMessage.DetectionCode);
    }
}