using System;

class BSCInterferenceGenerator : InterferenceGenerator
{
    private Random randomNumberGenerator = new Random();

    // deform a packet by inverting a single random bit
    public Packet DeformPacket(Packet packet)
    {
        // interference probability equal to 1.0
        Packet result = new Packet("");

        int randomIndex = randomNumberGenerator.Next(0, packet.Content.GetLength());
        InvertBit(result.Content, randomIndex);

        return result;
    }

    private BinaryString InvertBit(BinaryString message, int index)
    {
        message.InvertBit(index);
        return message;
    }
}