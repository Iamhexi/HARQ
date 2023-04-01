class BinaryString
{
    public string Content {set;get;}

    public BinaryString(string content)
    {
        SetContent(content);
    }

    // BinaryString can contain only ones and zeros. Every non-zero character is set to 1.
    public void SetContent(string content)
    {
        Content = content;
        for (int i = 0; i < Content.Length; i++) {
            // TODO: rewrite with a notion that strings are immutable
            // if (Content[i] != '0')
            //     Content[i] = '1';
            // else
            //     Content[i] = '0';
        }
    }

    public void InvertBit(int index)
    {
        // TODO: implement
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