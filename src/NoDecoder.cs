class NoDecoder : Decoder
{
    // This is counterpart of NoEncoder, no decoding required
    public Packet Decode(Packet encodedMessage)
    {
        return encodedMessage;
    }
}