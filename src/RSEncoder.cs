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

        if(bytes.Length < 2)
        {
            message.CorrectionCode = new BinaryString("");
            return message;
        }

        string byteString = "";
        for(int i = 0; i <= bytes.Length/256; i++)
        {
            if(i < bytes.Length/256)
            {
                byte[] temp = new byte[256];
                Array.Copy(bytes, i * 256, temp, 0, 256);
                byte[] ecc = ReedSolomonAlgorithm.Encode(temp, 255);
                for(int j = 0; j < ecc.Length; j++)
                {
                    byteString += Convert.ToString(ecc[j], 2).PadLeft(8, '0');
                }
            }
            else
            {
                if(bytes.Length - (i * 256) > 0)
                {
                    byte[] temp = new byte[bytes.Length - (i * 256)];
                    Array.Copy(bytes, i * 256, temp, 0, temp.Length);
                    byte[] ecc = ReedSolomonAlgorithm.Encode(temp, temp.Length - 1);
                    for(int j = 0; j < ecc.Length; j++)
                    {
                        byteString += Convert.ToString(ecc[j], 2).PadLeft(8, '0');
                    }
                }
            }
        }
        message.CorrectionCode = new BinaryString(byteString);
        return message;
    }
}
