using System;

class Receiver
{
    public Packet Feedback = new Packet(PacketType.Acknowledgement, "");
    private DetectionDecoder detectionDecoder = null;
    private Decoder correctionDecoder = null;

    public Receiver(DetectionDecoder detectionDecoder, Decoder correctionDecoder)
    {
        this.detectionDecoder = detectionDecoder;
        this.correctionDecoder = correctionDecoder;
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
        if(receivedPacket.Type == PacketType.Data)
            receivedPacket = correctionDecoder.Decode(receivedPacket);

        if (receivedPacket.Type == PacketType.Establish) {
            Console.WriteLine("Connection established.");
        } else if (receivedPacket.Type != PacketType.Data) {
            Feedback = new Packet(receivedPacket.Id, PacketType.Acknowledgement);
        } else if (detectionDecoder.Decode(receivedPacket)) {
            Feedback = new Packet(receivedPacket.Id, PacketType.Acknowledgement);
            Statistics.ReportReceivedPacket(receivedPacket);
        } else
            Feedback = new Packet(receivedPacket.Id, PacketType.NoAcknowledgement);

        if (Settings.ConsoleOutput)
            Console.WriteLine("Received packet: {0}, {1}", receivedPacket.Type, receivedPacket.Content);

        int delay = (Settings.PacketsPerSecond == 0) ? 0 : 1000/Settings.PacketsPerSecond;
        System.Threading.Thread.Sleep( delay );
    }
}
