using System;

class Sender
{
    public static int PayloadSizeInBytes = Settings.PacketPayloadSize;

    private Encoder correctionEncoder = new NoEncoder();
    private Encoder detectionEncoder = new NoEncoder();

    private int lastPacketId = 0;
    private string dataSource;
    private int lastPosition = 0;
    private bool transmissionStarted = false;
    

    public Sender(Encoder detectionEncoder, Encoder correctionEncoder, string dataSource)
    {
        this.correctionEncoder = correctionEncoder;
        this.detectionEncoder = detectionEncoder;
        this.dataSource = dataSource;
    }

    public void EndTransmission()
    {
        transmissionStarted = false;
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
            return new Packet(lastPacketId, PacketType.EndTransmission);
        } else if (BitsLeft() < (bitsPerByte*PayloadSizeInBytes))
            payload = GetLastDataPayload();
        else
            payload = GetFullSizedPayload();

        Packet packet = new Packet(PacketType.Data, payload);
        packet = detectionEncoder.Encode(packet);
        packet = correctionEncoder.Encode(packet);

        Statistics.ReportSentPacket(packet);
        lastPacketId++;
        return packet;
    }

    
}
