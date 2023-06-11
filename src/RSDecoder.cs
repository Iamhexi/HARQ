using ReedSolomon;
using System;
class RSDecoder : Decoder
{
    public Packet Decode(Packet encodedMessage)
    {
        string correctionBytes = encodedMessage.CorrectionCode.ToString();
        byte[] ecc = new byte[correctionBytes.Length/8];
        for(int i = 0; i < correctionBytes.Length/8; i++)
        {
            ecc[i] = Convert.ToByte(correctionBytes.Substring(8 * i, 8), 2);
        }
        string message = encodedMessage.Content.ToString();
        byte[] toDecode = new byte[message.Length/8];
        for(int i = 0; i < message.Length/8; i++)
        {
            toDecode[i] = Convert.ToByte(message.Substring(8 * i, 8), 2);
        }
        byte[] decodedBytes = ReedSolomonAlgorithm.Decode(toDecode, ecc); //returns null if message could not be fixed
        if(decodedBytes == null)
            return encodedMessage;
        string decodedString = "";
        for(int i = 0; i < decodedBytes.Length; i++)
        {
            decodedString += Convert.ToString(decodedBytes[i], 2).PadLeft(8, '0');
        }
        return new Packet(encodedMessage.Id, encodedMessage.Type, decodedString, encodedMessage.DetectionCode, encodedMessage.CorrectionCode);
    }
}