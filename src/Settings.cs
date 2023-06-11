internal class Settings
{
    public const int PacketPayloadSize = 10; // in Bytes, between [10, 2^16] Bytes

    public const int MaxAllowedRetranmissionBeforePacketDrop = 100;
    public const DetectionCodeType EmployedDetectionCode = DetectionCodeType.CRC8; // CRC8, CRC32
    public const CorrectionCodeType EmployedCorrectionCode = CorrectionCodeType.ReedSolomon; //ReedSolomon, NoCorrection
    public const ModelType EmployedModelType = ModelType.GilbertElliotModel; // BSC, Gilbert-Elliot
    public const float BSCErrorProbability = .9f;
    public const float GilbertElliotModelGoodStateErrorProbability = .001f;
    public const float GilbertElliotModelBadStateErrorProbability = 0.5f;
    public const float GilbertElliotModelGoodToBadProbability = 0.15f;
    public const float GilbertElliotModelBadToGoodProbability = 0.9f;
    public static int PacketsPerSecond = 0; // 0 - no limit



    public static Encoder GetDetectionEncoder()
    {       
        if (Settings.EmployedDetectionCode == DetectionCodeType.CRC8)
            return new CRC8Encoder();
        else if (Settings.EmployedDetectionCode == DetectionCodeType.CRC32)
            return new CRC32Encoder();
    }

    public static DetectionDecoder GetDetectionDecoder()
    {
        if (Settings.EmployedDetectionCode == DetectionCodeType.CRC8)
            return new CRC8Decoder();
        else if (Settings.EmployedDetectionCode == DetectionCodeType.CRC32)
            return new CRC32Decoder();
    }

    public static Encoder GetCorrectionEncoder()
    {
        if(Settings.EmployedCorrectionCode == CorrectionCodeType.ReedSolomon)
            return new RSEncoder();
        else if(Settings.EmployedCorrectionCode == CorrectionCodeType.NoCorrection)
            return new NoEncoder();
    }

    public static Decoder GetCorrectionDecoder()
    {
        if(Settings.EmployedCorrectionCode == CorrectionCodeType.ReedSolomon)
            return new RSDecoder();
        else if(Settings.EmployedCorrectionCode == CorrectionCodeType.NoCorrection)
            return new NoDecoder();
    }
}

// ewentualnie bez Reed-Solomona
// retransmisja: 3 różne: losowo wybrane: dużo, mało - jeden, dwa,trzy, średnio; pojedynczne procenty