using System;
using System.Collections;
using System.Text;

class BinarySymmetricChannel : Model
{
    public float ErrorProbability = .5f;
    private CommunicationChannel channel = new CommunicationChannel();

    public BinarySymmetricChannel(float errorProbability)
    {
        this.ErrorProbability = errorProbability;
    }

    public override void Run(string filename)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(filename); // from file to bytes
        BitArray bitArray = new BitArray(bytes); // from bytes to bits
        var dataToTransfer = ToBitString(bitArray); // from bits to string

        // DetectionDecoder decoder = 
        //     (Settings.EmployedDetectionCode == DetectionCodeType.CRC8) ? new CRC8Decoder() : new CRC32Decoder();

        // Encoder encoder =
        //     (Settings.EmployedDetectionCode == DetectionCodeType.CRC8) ? new CRC8Encoder() : new CRC32Encoder();

        channel.AddReceiver(new Receiver(
            new CRC8Decoder(),
            new NoDecoder() // correction
        ));

        channel.AddSender(new Sender(
            new CRC8Encoder(), // detection
            new NoEncoder(), // correction
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
