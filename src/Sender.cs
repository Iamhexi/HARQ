using System;

class Sender
{
    public static int PayloadSizeInBytes = Settings.PacketPayloadSize;

    private Encoder correctionEncoder = new NoEncoder();
    private Encoder detectionEncoder = new NoEncoder();
    private Statistics statistics;

    private int lastPacketId = 0;
    private string dataSource;
    private int lastPosition = 0;
    private bool transmissionStarted = false;
    

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

        if (lastPosition == 0 && transmissionStarted == false) { // initialise transmission
            transmissionStarted = true;
            return new Packet(PacketType.Establish, "");
        } else if (!HasData()) { // terminate transmission 
            transmissionStarted = false;
            return new Packet(lastPacketId, PacketType.EndTransmission); // TODO: not reported in the statistics
        } else if (BitsLeft() < (bitsPerByte*PayloadSizeInBytes))
            payload = GetLastDataPayload();
        else
            payload = GetFullSizedPayload();

        Packet packet = new Packet(PacketType.Data, payload);
        packet = detectionEncoder.Encode(packet);
        packet = correctionEncoder.Encode(packet);

        statistics.ReportSentPacket(packet);

        lastPacketId++;
        return packet;
    }

    
}
