using System;
using System.Collections;
using System.Text;

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

    public override void Run(string filename)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(filename); // from file to bytes
        BitArray bitArray = new BitArray(bytes); // from bytes to bits
        var dataToTransfer = ToBitString(bitArray); // from bits to string

        channel.AddReceiver(new Receiver(
            new NoDecoder(), // detection
            new NoDecoder(), // correction
            ref this.statistics
        ));

        channel.AddSender(new Sender(
            new CRC8Encoder(), // detection
            new RSEncoder(), // correction
            ref this.statistics,
            dataToTransfer
        ));

        channel.AddInterferenceGenerator(new BSCInterferenceGenerator(ErrorProbability));

        // TODO: trasmit data as long as required
        while (channel.TrasmissionDataAvailable()) {
            channel.TrasmitData(); // sent content depends on the sender
            channel.RetrieveData(); // method of handling received data depends on the receiver
        }

    }

    private static string ToBitString(BitArray bits)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < bits.Count; i++)
        {
            char c = bits[i] ? '1' : '0';
            sb.Append(c);
        }

        return sb.ToString();
    }
}
