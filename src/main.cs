using System;

class Program
{
    static void Main(string[] args)
    {
        Model currentModel = null;

        string fileToTransfer = "model/from/file.txt";

        if (Settings.EmployedModelType == ModelType.BinarySymmetricChannel) {
            BinarySymmetricChannel bsc = new BinarySymmetricChannel(.005f);
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
                badToGoodProbability
            );

            currentModel = gem;
        } else {
            Console.WriteLine("The chosen model does NOT exist. Re-run program and enter the correct model's acronym.");
            return;
        }

        currentModel.Run(fileToTransfer);

        Statistics.ShowStatistics();
    }
}
