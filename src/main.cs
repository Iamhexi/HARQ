using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, world!");
        Packet p = new Packet("1010101000");
        Console.WriteLine(p.Id);
    }
}
