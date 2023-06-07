using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

static class Statistics
{
    public static int TrasmittedPackets = 0;
    
    private static int[] retransmissionDistribution = new int[Settings.MaxAllowedRetranmissionBeforePacketDrop + 1]; // retransmissionsDistributions[0] = 5; // 5 packets had 0 retransmissions

    private static List<Packet> originalData = new List<Packet>();
    private static List<Packet> dataAfterCorrection = new List<Packet>();
    
    public static void ReportSentPacket(Packet sentPacket)
    {
        if (sentPacket.Type == PacketType.Data)
            originalData.Add(sentPacket);
    }

    public static void ReportReceivedPacket(Packet receivedPacket)
    {
        if (receivedPacket.Type == PacketType.Data)
            dataAfterCorrection.Add(receivedPacket);
    }


    public static void ReportRetransmissionsOfPacket(int retransmissions)
    {
        if (retransmissions > Settings.MaxAllowedRetranmissionBeforePacketDrop)
            retransmissionDistribution[Settings.MaxAllowedRetranmissionBeforePacketDrop]++;
        else
            retransmissionDistribution[retransmissions]++;
    }

    public static void SaveStatisticsToFile(string filename)
    {
        int totalRetransmissions = 0;
        for (int i = 0; i <= Settings.MaxAllowedRetranmissionBeforePacketDrop; i++)
            totalRetransmissions += retransmissionDistribution[i] * i;

        SaveRetransmissionDistributionToFile(filename);
        int falsePostives = CountCorruptedPacketsMarkedAsCorrect();

        Console.WriteLine("Transmitted {0} packets and {2} of them were false positives ({3}%). {1} retransmissions were required.", 
            TrasmittedPackets, totalRetransmissions, falsePostives, Math.Round(100.0f * falsePostives / TrasmittedPackets, 2));
    }
    private static int CountCorruptedPacketsMarkedAsCorrect()
    {
        int misjudged = 0;
        foreach (var packet in originalData) {
            
            var counterpart = FindPacketWithIdOnList(packet.Id, ref dataAfterCorrection);

            if (counterpart == null)
                misjudged++;
            else if (packet.Content != counterpart.Content)
                misjudged++;
            
        }

        return misjudged;
    }

    private static Packet FindPacketWithIdOnList(int id, ref List<Packet> packetList)
    {
        foreach (var packet in packetList)
            if (packet.Id == id)
                return packet;
        return null;
    }
    private static void SaveRetransmissionDistributionToFile(string filename)
    {
        FileStream fs = File.OpenWrite(filename);
        string data = "retransmissions;packets (requiring that many retransmissions)\n";

        for (int i = 0; i <= Settings.MaxAllowedRetranmissionBeforePacketDrop; i++)
            data += i + ";" + retransmissionDistribution[i] + '\n'; 
            
        byte[] bytes = Encoding.UTF8.GetBytes(data);

        fs.Write(bytes, 0, bytes.Length);
    }
}