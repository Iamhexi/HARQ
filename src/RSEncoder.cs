using ReedSolomon;
using System;
class RSEncoder : Encoder
{
    public Packet Encode(Packet message)
    {
        string toEncode = message.Content.ToString();

        byte[] bytes = new byte[toEncode.Length/ 8];

        for(int i = 0; i < toEncode.Length/8; ++i)
        {
            bytes[i] = Convert.ToByte(toEncode.Substring(8 * i, 8), 2);
        }

        byte[] ecc = ReedSolomonAlgorithm.Encode(bytes, bytes.Length - 1);
        string byteString = "";
        for(int i = 0; i < ecc.Length; i++)
        {
            byteString += Convert.ToString(ecc[i], 2).PadLeft(8, '0');
        }
        message.CorrectionCode = new BinaryString(byteString);
        return message;
    }
}