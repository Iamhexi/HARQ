using System;

class Receiver
{
    private Decoder detectionDecoder = null;
    private Decoder correctionDecoder = null;
    private Statistics statistics;

    public Receiver(Decoder detectionDecoder, Decoder correctionDecoder, ref Statistics statistics)
    {
        this.detectionDecoder = detectionDecoder;
        this.correctionDecoder = correctionDecoder;
        this.statistics = statistics;
    }

    private bool HasDecoders()
    {
        try {
            if (detectionDecoder == null)
                throw new System.NullReferenceException("To decode a packet detectionDecoder must not be null.");
            else if (correctionDecoder == null)
                throw new System.NullReferenceException("To decode a packet correctionDecoder must not be null.");
            return true;
        } catch (NullReferenceException e) {
            Console.WriteLine(e);
            return false;
        }
    }

    public void ReceiveMessage(Packet receivedPacket)
    {
        // TODO: apply only to the packet's content
        receivedPacket = correctionDecoder.Decode(receivedPacket);
        
        // TODO: apply to the whole packet
        receivedPacket = detectionDecoder.Decode(receivedPacket);

        statistics.ReportReceivedPacket(receivedPacket);

        Console.WriteLine("Received message: {0} ",  receivedPacket.Content );
    }
}
