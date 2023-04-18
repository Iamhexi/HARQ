using System;

class BinarySymmetricChannel : Model
{
    public float ErrorProbability = .5f;
    private CommunicationChannel channel = new CommunicationChannel();
    public Statistics statistics;

    public BinarySymmetricChannel(float errorProbability, ref Statistics statistics)
    {
        this.ErrorProbability = errorProbability;
        this.statistics = statistics;
    }

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

        channel.AddInterferenceGenerator(new BSCInterferenceGenerator(ErrorProbability));

        for (int i = 0; i < iterations; i++) {
            channel.TrasmitData(); // sent content depends on the sender
            channel.RetrieveData(); // method of handling received data depends on the receiver
        }

    }
}
