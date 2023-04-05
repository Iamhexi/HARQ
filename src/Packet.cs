using System;

class Packet
{
    public int Id { get; private set; }
    public int Length { get; } = 0;
    public BinaryString Content { get; }
    protected static int idCounter = 0;

    public Packet(int id, string content)
    {
        Id = id;
        this.Content = new BinaryString(String.Copy(content));
        Length = Content.GetLength(); // TODO: add header length as well
    }

    public Packet(string content)
    {
        setId();
        this.Content = new BinaryString(content);
        Length = Content.GetLength(); // TODO: add header length as well
    }

    public Packet(BinaryString content)
    {
        setId();
        this.Content = content;
        Length = Content.GetLength(); // TODO: add header length as well
    }

    public Packet(Packet packet)
    {
        this.Id = packet.Id;
        this.Content = packet.Content;
        Length = Content.GetLength(); // TODO: add header length as well
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
}
