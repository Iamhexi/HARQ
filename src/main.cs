using System;

class Program
{
    static void Main(string[] args)
    {
        // TODO: [important] Implement Simple Encoder and Decoder and construct them inside CommunicationChannel
        // TODO: Implement Gilbert-Elliot model
        // TODO: Add an input to choose to choose model: BSC or Gilbert-Elliot model
        BinarySymmetricChannel bsc = new BinarySymmetricChannel();
        bsc.Run(5);
    }
}
