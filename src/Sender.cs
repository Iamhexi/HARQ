using System;

class Sender
{
    public static int PayloadSizeInBytes = Settings.PacketPayloadSize;

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

    private BinaryString GetFullSizedPayload()
    {
        const int bitsPerByte = 8;
        string output = dataSource.Substring(lastPosition, PayloadSizeInBytes*bitsPerByte);
        lastPosition += PayloadSizeInBytes * bitsPerByte;

        return new BinaryString(output);
    }

    public bool HasData()
    {
        return lastPosition < dataSource.Length - 1;
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

    private int BitsLeft()
    {
        return dataSource.Length - lastPosition - 1;
    }

    public BinaryString GetLastDataPayload()
    {
        string output = dataSource.Substring(lastPosition, BitsLeft());
        lastPosition = dataSource.Length;
        return new BinaryString(output);
    }

    public Packet NextPacket()
    {
        if (!HasEncoders())
            return null;
        
        BinaryString payload;
        const int bitsPerByte = 8;

        if (!HasData())
            return null;
        else if (BitsLeft() < (bitsPerByte*PayloadSizeInBytes))
            payload = GetLastDataPayload();
        else
            payload = GetFullSizedPayload();

        Packet packet = new Packet(payload);
        packet = detectionEncoder.Encode(packet);
        packet = correctionEncoder.Encode(packet);

        statistics.ReportSentPacket(packet);

        return packet;
    }

    
}
