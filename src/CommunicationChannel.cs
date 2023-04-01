using System;
using System.Collections.Generic;

class CommunicationChannel
{
    private Queue<Packet> channel = new Queue<Packet>();

    private InterferenceGenerator interferenceGenerator = null;
    private Sender sender = null;
    private Receiver receiver = null;
    private Statistics statistics = new Statistics();

    private Encoder correctionEncoder = new NoEncoder();
    private Decoder correctionDecoder = new NoDecoder();

    private Encoder detectionEncoder = new NoEncoder();
    private Decoder detectionDecoder = new NoDecoder();

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

    public void PrintStatisticsOverview()
    {
        Console.WriteLine("Bits corrupted: " + statistics.GetPercentageOfBitsCorrupted() * 100.0f + "%");
    }

    private bool ErrorOccurs()
    {
        try {
            if (sender == null)
                throw new System.NullReferenceException("To transmit data in CommunicationChannel, sender must not be null.");
            else if (receiver == null)
                throw new System.NullReferenceException("To transmit data in CommunicationChannel, receiver must not be null.");
            else if (interferenceGenerator == null)
                throw new System.NullReferenceException("To transmit data in CommunicationChannel, interferenceGenerator must not be null.");
            else if (detectionEncoder == null)
                throw new System.NullReferenceException("To transmit data in CommunicationChannel, detectionEncoder must not be null.");
            else if (detectionDecoder == null)
                throw new System.NullReferenceException("To transmit data in CommunicationChannel, detectionDecoder must not be null.");
            else if (correctionEncoder == null)
                throw new System.NullReferenceException("To transmit data in CommunicationChannel, correctionEncoder must not be null.");
            else if (correctionDecoder == null)
                throw new System.NullReferenceException("To transmit data in CommunicationChannel, correctionDecoder must not be null.");
            
            return false;
        } catch (System.NullReferenceException e) {
            Console.WriteLine(e.Message);
            return true;
        }
    }

    public void TrasmitData()
    {
        if (ErrorOccurs()) {
            Console.WriteLine("Error occured, data could not be transmitted.");
            return;
        }
        // get the message from the receiver
        BinaryString encodedMessage = sender.GetNewMessage();

        // apply detection then correction encoding
        encodedMessage = detectionEncoder.Encode(encodedMessage);
        encodedMessage = correctionEncoder.Encode(encodedMessage);

        // pack data into a packet
        Packet packet = CommunicationChannel.CreatePacket(encodedMessage);

        // report the packet to statistics module before apply interferences
        statistics.ReportSentPacket(packet);

        // simulate interferences inside a communication channel
        interferenceGenerator.DeformPacket(packet);

        // pass on the packet to the channel
        channel.Enqueue(packet);
    }

    public void RetrieveData()
    {
        if (ErrorOccurs()) {
            Console.WriteLine("Error occured, cannot retrieve data.");
            return;
        } else if (channel.Count == 0) {
            Console.WriteLine("There are not messages in the communication channel available to be retrieved.");
            return;
        }
            

        Packet receivedPacket = channel.Dequeue();
        BinaryString message = CreateBinaryString( receivedPacket );

        message = correctionDecoder.Decode(message);
        message = detectionDecoder.Decode(message);

        receiver.ReceiveMessage(message);

        Packet correctedPacket = new Packet(message);
        statistics.ReportReceivedPacket(correctedPacket);
    }
}
