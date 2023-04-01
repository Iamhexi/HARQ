class NoDecoder : Decoder
{
    // This is counterpart of NoEncoder, no decoding required
    public BinaryString Decode(BinaryString encodedMessage)
    {
        return encodedMessage;
    }
}