public class Settings
{
    public const int PacketPayloadSize = 20; // in Bytes, between [0, 2^16] Bytes
    public const DetectionCodeType EmployedDetectionCode = DetectionCodeType.CRC8; // CRC8, CRC32
    public const ModelType EmployedModelType = ModelType.BinarySymmetricChannel;
}