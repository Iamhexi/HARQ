class NoEncoder : Encoder
{
    // No encoding happens, just plain copy of the original message
    public BinaryString Encode(BinaryString message)
    {
        return message;
    }
}