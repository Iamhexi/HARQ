using System;
using System.Collections.Generic;

class CommunicationChannel
{
    // provide initial message, attach codes
    // store, genrate inferences, and send back the message
    private Queue<Packet> channel = new Queue<Packet>();

    private InterferenceGenerator interferenceGenerator = null;
    private Sender sender = null;
    private Receiver receiver = null;

    private Encoder correctionEncoder;
    private Decoder correctionDecoder;

    private Encoder detectionEncoder;
    private Decoder detectionDecoder;

    public void AddReceiver(Receiver receiver)
    {
        this.receiver = receiver;
    }

    public void AddSender(Sender sender)
    {
        this.sender = sender;
    }

    public void AddInterferenceGenerator(InterferenceGenerator generator)
    {
        this.interferenceGenerator = generator;
    }

    private static Packet CreatePacket(BinaryString message)
    {
        return new Packet( message.ToString() );
    }

    private static BinaryString CreateBinaryString(Packet packet)
    {
        return packet.Content;
    }

    private bool ErrorOccurs()
    {
        return sender == null || receiver == null || interferenceGenerator == null
            || detectionEncoder == null || detectionDecoder == null
            || correctionEncoder == null || correctionDecoder == null;
    }

    public void TrasmitData()
    {
        if (ErrorOccurs()) {
            Console.WriteLine("Error occured, cannot trasmit data.");
            return;
        }
        // get the message from the receiver
        BinaryString encodedMessage = sender.GetNewMessage();

        // apply detection then correction encoding
        encodedMessage = detectionEncoder.Encode(encodedMessage);
        encodedMessage = correctionEncoder.Encode(encodedMessage);

        // pack data into a packet
        Packet packet = CommunicationChannel.CreatePacket(encodedMessage);

        // simulate interferences inside a communication channel
        interferenceGenerator.DeformPacket(packet);

        // pass on the packet to the output
        channel.Enqueue(packet);
    }

    public void RetrieveData()
    {
        if (ErrorOccurs()) {
            Console.WriteLine("Error occured, cannot retrieve data");
            return;
        } else if (channel.Count == 0) {
            Console.WriteLine("There are not messages in the communication channel available to be retrieved.");
            return;
        }
            

        Packet recievedPacket = channel.Dequeue();
        BinaryString message = CreateBinaryString( recievedPacket );

        message = correctionDecoder.Decode(message);
        message = detectionDecoder.Decode(message);

        receiver.ReceiveMessage(message);
    }
}
