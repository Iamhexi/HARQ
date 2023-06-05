using System;

class PacketTypeConverter
{
    public static string Convert(PacketType packetType) 
    {
        switch(packetType) 
        {
            case PacketType.Data:
                return "00000000"; 

            case PacketType.Acknowledgement:
                return "10101010";

            case PacketType.NoAcknowledgement:
                return "11111111";

            case PacketType.Establish:
                return "10010010";

            case PacketType.EndTransmission:
                return "00100100";

            default:
                throw new Exception("An attempt to convert unrecognised packet type has been made!");
        }
    }

    public static PacketType Convert(string binaryPacketType) 
    {
        switch(binaryPacketType) 
        {
            case "00000000":
                return PacketType.Data;

            case "10101010":
                return PacketType.Acknowledgement;

            case "11111111":
                return PacketType.NoAcknowledgement;

            case "10010010":
                return PacketType.Establish;

            case "00100100":
                return PacketType.EndTransmission;

            default:
                throw new Exception("An attempt to convert unrecognised packet type has been made!");
        }
    }

    // Data = "00000000",
    // Acknowledgement = "10101010",
    // NoAcknowledgement = "11111111",
    // Establish = "10010010",
    // End = "00100100"

}