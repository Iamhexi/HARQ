using System;

class Program
{
    static void Main(string[] args)
    {
        Statistics statistics = new Statistics();
        Model currentModel = null;
        int iterations = 0;

        string fileToTransfer = "model/from/file.txt";
        // TODO: Implement Simple Encoder and Decoder and construct them inside CommunicationChannel
    
        Console.WriteLine("Choose a model: Binary Symmetric Channel [bsc] or Gilbert-Elliot Model [gem]: ");
        string userInput = Console.ReadLine();
        Console.WriteLine("Enter a number of packets to trasmit: ");
        iterations = Convert.ToInt32(Console.ReadLine());

        if (userInput == "bsc") {
            BinarySymmetricChannel bsc = new BinarySymmetricChannel(.15f, ref statistics);
            currentModel = bsc;
        } else if (userInput == "gem") {
            float goodStateErrorProbability = .01f;
            float badStateErrorProbability = .75f;
            float goodToBadProbability = 0.1f;
            float badToGoodProbability = 0.7f;

            GilberElliotModel gem = new GilberElliotModel(
                goodStateErrorProbability,
                badStateErrorProbability,
                goodToBadProbability,
                badToGoodProbability,
                ref statistics
            );

            currentModel = gem;
        } else {
            Console.WriteLine("The chosen model does NOT exist. Re-run program and enter the correct model's acronym.");
            return;
        }

        currentModel.Run(fileToTransfer);
        Console.WriteLine("Bits corrupted: " + statistics.GetPercentageOfBitsCorrupted() * 100.0f + "%");
    }
}
