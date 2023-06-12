class CRC32Decoder: DetectionDecoder
{
    public bool Decode(Packet encodedMessage)
    {
        CRC32Calc calc = new CRC32Calc();
        string dataWithoutCodes = encodedMessage.GetHeader() + encodedMessage.Content.ToString();
        string crc = calc.Compute_CRC32(dataWithoutCodes);

        return new BinaryString(crc).Equals(encodedMessage.DetectionCode);
    }
}