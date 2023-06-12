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

        Console.WriteLine("\n\nSummary\n");
        Console.WriteLine("Detection code: " + Settings.EmployedDetectionCode);
        Console.WriteLine("Correction code: "  +  Settings.EmployedCorrectionCode);
        Console.WriteLine("Model: " + Settings.EmployedModelType);
        if (Settings.EmployedModelType == ModelType.BinarySymmetricChannel)
            Console.WriteLine("Error probability: " + Settings.BSCErrorProbability);
        
        Statistics.SaveStatisticsToFile("statistics/retransmissions.csv");
    }
}