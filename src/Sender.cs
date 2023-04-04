using System;

class Sender
{
    public BinaryString GetNewMessage()
    {
        string message = "1010101";
        Console.WriteLine("Sent message: {0} ", message);
        return new BinaryString(message);
    }
}
