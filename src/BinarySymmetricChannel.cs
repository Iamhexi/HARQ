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

        DetectionDecoder decoder;
        if (Settings.EmployedDetectionCode == DetectionCodeType.CRC8)
            decoder = new CRC8Decoder();
        else if (Settings.EmployedDetectionCode == DetectionCodeType.CRC32)
            decoder = new CRC32Decoder();
            
        Encoder encoder;
        if (Settings.EmployedDetectionCode == DetectionCodeType.CRC8)
            encoder = new CRC8Encoder();
        else if (Settings.EmployedDetectionCode == DetectionCodeType.CRC32)
            encoder = new CRC32Encoder();

        channel.AddReceiver(new Receiver(
            decoder, // detection
            new NoDecoder() // correction
        ));

        channel.AddSender(new Sender(
            new CRC8Encoder(), // detection
            encoder, // correction
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
