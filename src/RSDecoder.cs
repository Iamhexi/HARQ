using ReedSolomon;
using System;
class RSDecoder : Decoder
{
    public Packet Decode(Packet encodedMessage)
    {
        if(encodedMessage.CorrectionCode.Equals(new BinaryString("")))
            return encodedMessage;
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
        string decodedString = "";
        for(int i = 0; i <= toDecode.Length/256; i++)
        {
            if(i < (toDecode.Length/256))
            {
                byte[] temp = new byte[256];
                Array.Copy(toDecode, i * 256, temp, 0, 256);
                byte[] eccTemp = new byte[255];
                Array.Copy(ecc, i * 255, eccTemp, 0, 255);
                byte[] decodedBytes = ReedSolomonAlgorithm.Decode(temp, eccTemp);
                if(decodedBytes == null)
                {
                    decodedString += message.Substring(i * 256 * 8, 256 * 8);
                }
                else
                {
                    for(int j = 0; j < decodedBytes.Length; j++)
                    {
                        decodedString += Convert.ToString(decodedBytes[j], 2).PadLeft(8, '0');
                    }
                }
            }
            else
            {
                byte[] temp = new byte[toDecode.Length - (i * 256)];
                Array.Copy(toDecode, i * 256, temp, 0, temp.Length);
                if(ecc.Length - (i * 256) > 0)
                {
                    byte[] eccTemp = new byte[ecc.Length - (i * 255)];
                    Array.Copy(ecc, i * 255, eccTemp, 0, eccTemp.Length);
                    byte[] decodedBytes = ReedSolomonAlgorithm.Decode(temp, eccTemp);
                    if(decodedBytes == null)
                    {
                        decodedString += message.Substring(i * 256 * 8);
                    }
                    else
                    {
                        for(int j = 0; j < decodedBytes.Length; j++)
                        {
                            decodedString += Convert.ToString(decodedBytes[j], 2).PadLeft(8, '0');
                        }
                    }
                }
                else
                {
                    for(int j = 0; j < temp.Length; j++)
                    {
                        decodedString += Convert.ToString(temp[j], 2).PadLeft(8, '0');
                    }
                }
            }
        }
        return new Packet(encodedMessage.Id, encodedMessage.Type, decodedString, encodedMessage.DetectionCode, encodedMessage.CorrectionCode);
    }
}