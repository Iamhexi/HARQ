using System;
using System.Collections.Generic;

class CommunicationChannel
{
    private Queue<Packet> channel = new Queue<Packet>();
    private Packet recentlyTransmittedPacket = null;
    private InterferenceGenerator interferenceGenerator = null;
    private Sender sender = null;
    private Receiver receiver = null;
    private int lastPacketRetransmissions = 0;

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

        Packet packet = new Packet(PacketType.Acknowledgement, "");

        switch (receiver.Feedback.Type) {
            case PacketType.Acknowledgement:
                packet = sender.NextPacket();
                Statistics.TrasmittedPackets++;
                recentlyTransmittedPacket = packet.Clone();
                Statistics.ReportRetransmissionsOfPacket(lastPacketRetransmissions);
                lastPacketRetransmissions = 0;
                break;

            case PacketType.NoAcknowledgement:
                lastPacketRetransmissions++;
                packet = recentlyTransmittedPacket.Clone();
                break;

            case PacketType.Establish:
            case PacketType.Data:
                // receiver can neither start a transmission or send data
                break;

            case PacketType.EndTransmission:
                sender.EndTransmission(); // generally a receiver does not end transmission but he or she can
                Statistics.ReportRetransmissionsOfPacket(lastPacketRetransmissions);
                break;
        }

        Console.WriteLine("Feedback: {0}", receiver.Feedback.Type);
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
        receiver.ReceiveMessage(receivedPacket);
    }
}
