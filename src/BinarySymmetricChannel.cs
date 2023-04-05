using System;

class BinarySymmetricChannel : Model
{
    private CommunicationChannel channel = new CommunicationChannel();
    private Statistics statistics = new Statistics();

    public override void Run(int iterations)
    {
        string randomData = "0100010101010011111111100000010010100101010101001";


        channel.AddReceiver(new Receiver(
            new NoDecoder(),
            new NoDecoder(),
            ref this.statistics
        ));

        channel.AddSender(new Sender(
            new NoEncoder(),
            new NoEncoder(),
            ref this.statistics,
            randomData
        ));

        channel.AddInterferenceGenerator(new BSCInterferenceGenerator());

        for (int i = 0; i < iterations; i++) {
            channel.TrasmitData(); // sent content depends on the sender
            channel.RetrieveData(); // method of handling received data depends on the receiver
        }

        Console.WriteLine("Bits corrupted: " + statistics.GetPercentageOfBitsCorrupted() * 100.0f + "%");

    }
}
