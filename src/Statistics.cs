using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

static class Statistics
{
    public static int TrasmittedPackets = 0;
    
    private static int[] retransmissionDistribution = new int[Settings.MaxAllowedRetranmissionBeforePacketDrop + 1]; // retransmissionsDistributions[0] = 5; // 5 packets had 0 retransmissions

    public static void ReportRetransmissionsOfPacket(int retransmissions)
    {
        if (retransmissions > Settings.MaxAllowedRetranmissionBeforePacketDrop)
            retransmissionDistribution[Settings.MaxAllowedRetranmissionBeforePacketDrop]++;
        else
            retransmissionDistribution[retransmissions]++;
    }

    public static void SaveRetransmissionDistributionToFile(string filename)
    {
        FileStream fs = File.OpenWrite(filename);
        string data = "retransmissions;packets (requiring that many retransmissions)\n";

        for (int i = 0; i <= Settings.MaxAllowedRetranmissionBeforePacketDrop; i++)
            data += i + ";" + retransmissionDistribution[i] + '\n'; 
            
        byte[] bytes = Encoding.UTF8.GetBytes(data);

        fs.Write(bytes, 0, bytes.Length);
    }

    public static void ShowStatistics()
    {
        int totalRetransmissions = 0;
        for (int i = 0; i <= Settings.MaxAllowedRetranmissionBeforePacketDrop; i++)
            totalRetransmissions += retransmissionDistribution[i] * i;

        Console.WriteLine("Transmitted {0} packets and {1} retransmissions were required.", TrasmittedPackets, totalRetransmissions);
    }
}