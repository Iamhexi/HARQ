public class Settings
{
    public const int PacketPayloadSize = 10; // in Bytes, between [10, 2^16] Bytes

    public const int MaxAllowedRetranmissionBeforePacketDrop = 100;
    public const DetectionCodeType EmployedDetectionCode = DetectionCodeType.CRC8; // CRC8, CRC32
    public const ModelType EmployedModelType = ModelType.BinarySymmetricChannel;
    public const float BSCErrorProbability = .9f;
    public const float GilbertElliotModelGoodStateErrorProbability = .01f;
    public const float GilbertElliotModelBadStateErrorProbability = .75f;
    public const float GilbertElliotModelGoodToBadProbability = 0.1f;
    public const float GilbertElliotModelBadToGoodProbability = 0.7f;
    public static int PacketsPerSecond = 0; // 0 - no limit
}

// ewentualnie bez Reed-Solomona
// retransmisja: 3 różne: losowo wybrane: dużo, mało - jeden, dwa,trzy, średnio; pojedynczne procenty
// ile pakietów oznaczonych jako dobre mimo że faktycznie błędnych? statystyka