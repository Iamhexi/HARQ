using System;

class Packet
{
    public int Id { get; private set; }
    public int Length { get; } = 0;
    public PacketType Type = PacketType.Data;
    public BinaryString Content { get; private set; }
    public BinaryString DetectionCode = new BinaryString("");
    public BinaryString CorrectionCode = new BinaryString("");
    protected static int idCounter = 0;

    public Packet(
            int id, 
            PacketType packetType = PacketType.Data,
            string content = "",
            BinaryString detectionCode = null,
            BinaryString correctionCode = null
        )
    {
        Id = id;
        this.Content = new BinaryString(String.Copy(content));
        Length = ToString().Length;
        this.Type = packetType;
        CorrectionCode = correctionCode;
        DetectionCode = detectionCode;
    }

    public Packet(PacketType packetType = PacketType.Data, string content = "")
    {
        setId();
        this.Content = new BinaryString(content);
        Length = ToString().Length;
        this.Type = packetType;
    }

    public Packet(PacketType packetType = PacketType.Data, BinaryString content = null)
    {
        if (content == null)
            content = new BinaryString("");
        setId();
        this.Content = content;
        Length = ToString().Length;
        this.Type = packetType;
    }

    public Packet(Packet packet, PacketType packetType = PacketType.Data)
    {
        this.Id = packet.Id;
        this.Content = packet.Content;
        Length = ToString().Length;
        this.Type = packet.Type;
    }

    public Packet Clone()
    {
        return new Packet(Id, Type, Content.Content, DetectionCode, CorrectionCode);
    }

    public int GetNumberOfMismatchingBits(Packet packet)
    {
        return Content.GetNumberOfMismatchingBits(packet.Content);
    }

    private void setId()
    {
        this.Id = idCounter++; 
    }

    private string Align(string content, char alignWith, int bits)
    {
        int missingBits = bits - content.Length;
        string alignment = new string(alignWith, missingBits);
        return alignment + content;
    }

    public string GetHeader()
    {
        const int sizeOfPacketIdInBits = 32;
        const int sizeOfSourceAddress = 32;
        const int sizeOfDestinationAddress = 32;
        const int sizeOfDataSize = 16;
        // const int sizeOfPacketType = 8;
        

        string idInBinary = Convert.ToString(Id, 2);
        string alignedId = Align(idInBinary, '0', sizeOfPacketIdInBits);

        string sourceAddress = new string ('0', sizeOfSourceAddress); // 32 zeros: 0.0.0.0
        string destinationAddress = new string("1") + new string('0', sizeOfDestinationAddress - 1); // 1 one and 31 zeros: 128.0.0.0 

        string dataSize =  Align( Convert.ToString(this.Content.GetLength(), 2) , '0' , sizeOfDataSize);
        string packetType = PacketTypeConverter.Convert(Type);

        return alignedId + sourceAddress + destinationAddress + dataSize + packetType;
    }

    public string getPayload()
    {
        return Content.ToString() + DetectionCode + CorrectionCode;
    }

    public override string ToString()
    {
        return GetHeader() + getPayload();
    }   
}