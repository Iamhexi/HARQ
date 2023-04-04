using System.Collections.Generic;
using System;

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

    public float GetPercentageOfBitsCorrupted()
    {
        int totalBits = 0;
        receivedPackets.ForEach(packet => { totalBits += packet.Content.GetLength(); });

        int corruptedBits = GetNumberOfBitsCorrupted();

        if (corruptedBits == 0)
            return .0f;

        return (float) corruptedBits / totalBits;
    }

    private int GetNumberOfBitsCorrupted()
    {
        int corruptedBits = 0;
        for (int i = 0; i < receivedPackets.Count; i++) {
            corruptedBits += sentPackets[i].GetNumberOfMismatchingBits(receivedPackets[i]);
            // Console.WriteLine("sent: {0}, received: {1}", sentPackets[i].Content, receivedPackets[i].Content);
        }
        return corruptedBits;
    }
}