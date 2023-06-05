using System;
using System.Collections.Generic;

class CommunicationChannel
{
    private Queue<Packet> channel = new Queue<Packet>();
    private Packet recentlyTransmittedPacket = null;

    private InterferenceGenerator interferenceGenerator = null;
    private Sender sender = null;
    private Receiver receiver = null;

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
    
    private bool ErrorOccurs()
    {
        try {
            if (sender == null)
                throw new System.NullReferenceException("To transmit data in CommunicationChannel, sender must not be null.");
            else if (receiver == null)
                throw new System.NullReferenceException("To transmit data in CommunicationChannel, receiver must not be null.");
            else if (interferenceGenerator == null)
                throw new System.NullReferenceException("To transmit data in CommunicationChannel, interferenceGenerator must not be null.");
            return false;
            

        } catch (System.NullReferenceException e) {
            Console.WriteLine(e.Message);
            return true;
        }
    }

    public bool TrasmissionDataAvailable()
    {
        return sender.HasData();
    }

    public void TrasmitData()
    {
        if (ErrorOccurs()) {
            Console.WriteLine("Error occured, data could not be transmitted.");
            return;
        }

        if (!sender.HasData()) {
            Console.WriteLine("The sender does not have more data. An attempt to trasmit an empty data packet has been skipped!");
            return;
        }

        Packet packet;

        if (recentlyTransmittedPacket != null && receiver.Feedback != null && receiver.Feedback.Type == PacketType.NoAcknowledgement) {
            packet = recentlyTransmittedPacket;
            Statistics.Retransmissions++;
        } else {
            packet = sender.NextPacket();
            Statistics.TrasmittedPackets++;
        }
        
        recentlyTransmittedPacket = packet.Clone();
        Console.WriteLine("Recently transmitted: {0}", recentlyTransmittedPacket.Id);

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

        if (receiver.Feedback != null) {
            Console.WriteLine("Receiver: " + receiver.Feedback.Type);
            receiver.Feedback = null;
        }

        Packet receivedPacket = channel.Dequeue();
        receiver.ReceiveMessage(receivedPacket);
    }
}
