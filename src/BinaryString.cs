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
        if (index < Content.Length) {
            // if (Content[i] == '0')
            //     Content[i] = '1';
            // else if (Content[i] == '1')
            //     Content[i] = '0';
        }

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