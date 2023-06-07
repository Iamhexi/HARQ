using System;

class Program
{
    static void Main(string[] args)
    {
        Model currentModel = null;

        string fileToTransfer = "model/from/file.txt";

        if (Settings.EmployedModelType == ModelType.BinarySymmetricChannel) {
            BinarySymmetricChannel bsc = new BinarySymmetricChannel(Settings.BSCErrorProbability);
            currentModel = bsc;
        } else if (Settings.EmployedModelType == ModelType.GilbertElliotModel) {
            

            GilberElliotModel gem = new GilberElliotModel(
                Settings.GilbertElliotModelGoodStateErrorProbability,
                Settings.GilbertElliotModelBadStateErrorProbability,
                Settings.GilbertElliotModelGoodToBadProbability,
                Settings.GilbertElliotModelBadToGoodProbability
            );

            currentModel = gem;
        } else {
            Console.WriteLine("The chosen model does NOT exist. Re-run program and enter the correct model's acronym.");
            return;
        }

        currentModel.Run(fileToTransfer);

        Statistics.ShowStatistics();
        Statistics.SaveRetransmissionDistributionToFile("statistics/info.csv");
    }
}