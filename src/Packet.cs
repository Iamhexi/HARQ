class Packet
{
    public int Id { get; private set; }
    public BinaryString Content { get; }
    protected static int idCounter = 0;

    public Packet(string content)
    {
        this.Id = idCounter++;
        this.Content = new BinaryString(content);
    }
}
