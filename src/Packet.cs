using System;

class Packet
{
    public int Id { get; private set; }
    public int Length { get; } = 0;
    public BinaryString Content { get; private set; }
    protected static int idCounter = 0;

    public Packet(int id, string content)
    {
        Id = id;
        this.Content = new BinaryString(String.Copy(content));
        Length = ToString().Length;
    }

    public Packet(string content)
    {
        setId();
        this.Content = new BinaryString(content);
        Length = ToString().Length;
    }

    public Packet(BinaryString content)
    {
        setId();
        this.Content = content;
        Length = ToString().Length;
    }

    public Packet(Packet packet)
    {
        this.Id = packet.Id;
        this.Content = packet.Content;
        Length = ToString().Length;
    }

    public Packet Clone()
    {
        return new Packet(Id, Content.Content);
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

    private string GetHeader()
    {

        const int sizeOfPacketIdInBits = 32;
        const int sizeOfSourceSA = 32;
        const int sizeOfDestinationSA = 32;

        string idInBinary = Convert.ToString(Id, 2);
        string alignedId = Align(idInBinary, '0', sizeOfPacketIdInBits);

        string sourceSA = new string ('0', sizeOfSourceSA); // 32 zeros: 0.0.0.0
        string destinationSA = new string("1") + new string('0', sizeOfDestinationSA - 1); // 1 one and 31 zeros: 128.0.0.0

        return alignedId + sourceSA + destinationSA; // 92-bit header
    }

    public override string ToString()
    {
        return GetHeader() + Content;
    }   
}