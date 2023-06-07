using System;

class BinaryString : IEquatable<BinaryString>
{
    public string Content;

    public BinaryString(string content)
    {
        Content = content;
    }

    public virtual bool Equals(BinaryString other)
    {
        return this.Content.Equals(other.Content);
    }

    public void InvertBit(int index)
    {
        char[] copyAsCharArray = Content.ToCharArray();
        
        if (index < Content.Length) {
            if (copyAsCharArray[index] == '0')
                copyAsCharArray[index] = '1';
            else if (copyAsCharArray[index] == '1')
                copyAsCharArray[index] = '0';
        }

        Content = new string(copyAsCharArray);

    }

    public int GetNumberOfMismatchingBits(BinaryString anotherBinaryString)
    {
        int mismatchingBits = 0;

        for (int i = 0; i < Content.Length; i++)
            if (Content[i] != anotherBinaryString.Content[i]) 
                mismatchingBits++;
        
        return mismatchingBits;
    }

    public int GetLength()
    {
        return Content.Length;
    }

    public void AttachZero()
    {
        Content += '0';
    }

    public void AttachOne()
    {
        Content += '1';
    }

    public override string ToString()
    {
        return Content == "" ? "[Empty BinaryString]" : Content;
    }


}