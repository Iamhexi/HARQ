class CRC8Decoder: DetectionDecoder
{
    public bool Decode(Packet encodedMessage)
    {
        CRC8Calc calc = new CRC8Calc();
        string dataWithoutCodes = encodedMessage.GetHeader() + encodedMessage.Content;
        string crc = calc.checksum(dataWithoutCodes);

        return new BinaryString(crc) == encodedMessage.DetectionCode;
    }
}