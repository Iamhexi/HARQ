class NoEncoder : Encoder
{
    // No encoding happens, just plain copy of the original message
    public Packet Encode(Packet message)
    {
        return message;
    }
}