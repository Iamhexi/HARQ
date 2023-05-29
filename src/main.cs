using System;

class Program
{
    static void Main(string[] args)
    {
        Statistics statistics = new Statistics();
        Model currentModel = null;

        string fileToTransfer = "model/from/file.txt";
        // TODO: Implement Simple Encoder and Decoder and construct them inside CommunicationChannel

        if (Settings.EmployedModelType == ModelType.BinarySymmetricChannel) {
            BinarySymmetricChannel bsc = new BinarySymmetricChannel(.15f, ref statistics);
            currentModel = bsc;
        } else if (Settings.EmployedModelType == ModelType.GilbertElliotModel) {
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
