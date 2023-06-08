using System;
using System.Collections;
using System.Text;
class GilberElliotModel : Model 
{
    private CommunicationChannel channel = new CommunicationChannel();

    public GilberElliotModel
    (
        float goodStateErrorProbability,
        float badStateErrorProbability,
        float goodToBadProbability,
        float badToGoodProbability
    )
    {
        channel.SetInterferenceGenerator(new GEInterferenceGenerator(
            goodStateErrorProbability,
            badStateErrorProbability,
            goodToBadProbability,
            badToGoodProbability
        ));
    }


    public override void Run(string filename)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(filename); // from file to bytes
        BitArray bitArray = new BitArray(bytes); // from bytes to bits
        var dataToTransfer = ToBitString(bitArray); // from bits to string

        Decoder correctionDecoder = new RSDecoder();
        Encoder correctionEncoder = new RSEncoder();
        // TODO: assign the above decoder and encoder in the place of NoDecoder/NoEncoder

        channel.SetSender(
            new Sender(
                Settings.GetDetectionEncoder(),
                new NoEncoder(),
                dataToTransfer
            )
        );

        channel.SetReceiver(
            new Receiver(
                Settings.GetDetectionDecoder(),
                new NoDecoder()
            )
        );
        
        while (channel.TrasmissionDataAvailable()) {
            channel.TrasmitData();
            channel.RetrieveData();
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