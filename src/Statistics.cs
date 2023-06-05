using System.Collections.Generic;
using System;

static class Statistics
{
    // List<Packet> receivedPackets = new List<Packet>();
    // List<Packet> sentPackets = new List<Packet>();
    public static int TrasmittedPackets = 0;
    public static int Retransmissions = 0;

    public static void ShowStatistics()
    {
        Console.WriteLine("Transmitted {0} packets and {1} retransmissions were required\n", TrasmittedPackets, Retransmissions);
    }

    // public void ReportSentPacket(Packet packet)
    // {
    //     sentPackets.Add(packet.Clone());
    // }

    // public void ReportReceivedPacket(Packet packet)
    // {
    //     receivedPackets.Add(packet);
    // }
    
    // public void Reset()
    // {
    //     receivedPackets.Clear();
    //     sentPackets.Clear();
    // }

    // public float GetPercentageOfBitsCorrupted()
    // {
    //     int totalBits = 0;
    //     receivedPackets.ForEach(packet => { totalBits += packet.Content.GetLength(); });

    //     int corruptedBits = GetNumberOfBitsCorrupted();

    //     if (totalBits == 0)
    //         return .0f;

        

    //     return (float) corruptedBits / totalBits;
    // }

    // private int GetNumberOfBitsCorrupted()
    // {
    //     int corruptedBits = 0;
    //     for (int i = 0; i < receivedPackets.Count; i++) {
    //         corruptedBits += sentPackets[i].GetNumberOfMismatchingBits(receivedPackets[i]);
    //         // Console.WriteLine("sent: {0}, received: {1}", sentPackets[i].Content, receivedPackets[i].Content);
    //     }
    //     return corruptedBits;
    // }
}