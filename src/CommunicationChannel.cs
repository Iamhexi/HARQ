using System.Collections.Generic;

class CommunicationChannel
{
    // provide initial message, attach codes
    // store, genrate inferences, and send back the message
    private Queue<Packet> channel = new Queue<Packet>();


    public void AddReceiver(Receiver receiver)
    {
        this.receiver = receiver;
    }

    public void AddSend(Sender sender)
    {
        this.sender = sender;
    }

    private static Packet CreatePacket(BinaryString message)
    {
        return new Packet( message.toString() );
    }

    public void InsertMessage(BinaryString message)
    {
        Packet packet = CommunicationChannel::CreatePacket(message);

        // attach detection & correction codes 
        // encode the message with Coder

        // apply inferences
        //InterferenceGenerator:: // make noise according to model type


        channel.Enqueue(packet);
    }

    public BinaryString RetrieveMessage()
    {
        // decode the message
        // dequeue message from the queue
    }
}
