using System;

class BSCInterferenceGenerator : InterferenceGenerator
{
    public readonly double ErrorProbability = 0.1;

    private Random randomNumberGenerator = new Random();

    // deform a packet by inverting a bit with probablity ErrorProbability
    public Packet DeformPacket(Packet packet)
    {
        int index = 0;

        while (index < packet.Length) {
            if (randomNumberGenerator.NextDouble() <= ErrorProbability)
                InvertBit(packet.Content, index);

            index++;
        }

        return packet;
    }

    private BinaryString InvertBit(BinaryString message, int index)
    {
        message.InvertBit(index);
        return message;
    }
}