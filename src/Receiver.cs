using System;

class Receiver
{
    public void ReceiveMessage(BinaryString message)
    {
        Console.WriteLine("Received message: " + message);
    }
}