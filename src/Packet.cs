class Packet
{
    public int Id { get; private set; }
    public BinaryString Content { get; }
    protected static int idCounter = 0;

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

    public int GetNumberOfMismatchingBits(Packet packet)
    {
        return Content.GetNumberOfMismatchingBits(packet.Content);
    }

    private void setId()
    {
        this.Id = idCounter++; 
    }
}
