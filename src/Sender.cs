using System;

class Sender
{
    public static int PacketSizeInBits = 400; // 50 bytes

    private Encoder correctionEncoder = new NoEncoder();
    private Encoder detectionEncoder = new NoEncoder();
    private Statistics statistics;

    private string dataSource;
    private int lastPosition = 0;

    public Sender(Encoder detectionEncoder, Encoder correctionEncoder, ref Statistics statistics, string dataSource)
    {
        this.correctionEncoder = correctionEncoder;
        this.detectionEncoder = detectionEncoder;
        this.statistics = statistics;
        this.dataSource = dataSource;
    }

    private BinaryString GetNewMessage()
    {
        // TODO: read data from data source and progress lastPosition by PacketSizeInBits

        string message = "1010101";
        Console.WriteLine("Sent message: {0} ", message);
        return new BinaryString(message);
    }

    public bool HasData()
    {
        return lastPosition < dataSource.Length;
    }

    private bool HasEncoders()
    {
        try {
            if (detectionEncoder == null)
                throw new System.NullReferenceException("To transmit data detectionEncoder must not be null.");
            else if (correctionEncoder == null)
                throw new System.NullReferenceException("To transmit correctionEncoder must not be null.");
            return true;
        } catch (NullReferenceException e) {
            Console.WriteLine(e);
            return false;
        }
    }

    public Packet NextPacket()
    {
        if (!HasEncoders())
            return null;

        Packet packet = new Packet(GetNewMessage());
        packet = detectionEncoder.Encode(packet);
        packet = correctionEncoder.Encode(packet);

        statistics.ReportSentPacket(packet);

        return packet;
    }

    
}
