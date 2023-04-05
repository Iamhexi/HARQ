using System.Collections.Generic;
using System;

class Statistics
{
    // fixme: these are the same items as c# uses references to store objects
    List<Packet> receivedPackets = new List<Packet>();
    List<Packet> sentPackets = new List<Packet>();

    public void ReportSentPacket(Packet packet)
    {
        sentPackets.Add(packet.Clone());   
        Console.WriteLine("sent packet added to stats has content: " + packet.Content);     
    }

    public void ReportReceivedPacket(Packet packet)
    {
        receivedPackets.Add(packet);
        Console.WriteLine("received packet added to stats has content: " + packet.Content);     
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

        if (totalBits == 0)
            return .0f;

        

        return (float) corruptedBits / totalBits;
    }

    private int GetNumberOfBitsCorrupted()
    {
        int corruptedBits = 0;
        for (int i = 0; i < receivedPackets.Count; i++) {
            corruptedBits += sentPackets[i].GetNumberOfMismatchingBits(receivedPackets[i]);
            Console.WriteLine("sent: {0}, received: {1}", sentPackets[i].Content, receivedPackets[i].Content);
        }
        return corruptedBits;
    }
}