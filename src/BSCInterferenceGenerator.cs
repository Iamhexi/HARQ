using System;

class BSCInterferenceGenerator : InterferenceGenerator
{
    public readonly float ErrorProbability = .1f;

    private Random randomNumberGenerator = new Random();

    public BSCInterferenceGenerator(float errorProbability)
    {
        this.ErrorProbability = errorProbability;
    }

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