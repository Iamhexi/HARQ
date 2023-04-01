class BinarySymmetricChannel : Model
{
    private CommunicationChannel channel = new CommunicationChannel();

    public override void Run(int iterations)
    {
        for (int i = 0; i < iterations; i++) {
            channel.AddReceiver(new Receiver());
            channel.AddSender(new Sender());
            channel.AddInterferenceGenerator(new BSCInterferenceGenerator());

            channel.TrasmitData(); // sent content depends on the sender
            channel.RetrieveData(); // method of handling received data depends on the receiver
        }

        channel.PrintStatisticsOverview();
    }
}
