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
}