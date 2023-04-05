using System;

class Packet
{
    public int Id { get; private set; }
    public BinaryString Content { get; }
    protected static int idCounter = 0;

    public Packet(int id, string content)
    {
        Id = id;
        this.Content = new BinaryString(String.Copy(content));
    }

    public Packet(string content)
    {
        setId();
        this.Content = new BinaryString(content);
    }

    public Packet(BinaryString content)
    {
        setId();
        this.Content = content;
    }

    public Packet(Packet packet)
    {
        this.Id = packet.Id;
        this.Content = packet.Content;
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
