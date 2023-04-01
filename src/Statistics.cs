using System.Collections.Generic;

class Statistics
{
    List<Packet> receivedPackets = new List<Packet>();
    List<Packet> sentPackets = new List<Packet>();

    public void ReportSentPacket(Packet packet)
    {
        sentPackets.Add(packet);        
    }

    public void ReportReceivedPacket(Packet packet)
    {
        receivedPackets.Add(packet);
    }
    
    public void Reset()
    {
        receivedPackets.Clear();
        sentPackets.Clear();
    }

    // fixme: always returns 0.0
    public float GetPercentageOfBitsCorrupted()
    {
        int totalBits = 0;
        receivedPackets.ForEach(packet => { totalBits += packet.Content.GetLength(); });

        int corruptedBits = GetNumberOfBitsCorrupted();

        if (corruptedBits == 0)
            return .0f;

        return (float) totalBits / corruptedBits;
    }

    private int GetNumberOfBitsCorrupted()
    {
        int corruptedBits = 0;
        for (int i = 0; i < receivedPackets.Count; i++)
           corruptedBits += sentPackets[i].GetNumberOfMismatchingBits(receivedPackets[i]);
        return corruptedBits;
    }
}