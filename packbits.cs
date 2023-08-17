public static byte[]? PackBitsEncode(byte[] decodedArray)
{
    List<byte> encodedList = new();
    byte prevDb = 128;
    byte reps = 1;
    byte cpy = 1;
    List<byte> cpyList = new();

    for (int i = 0; i < decodedArray.Length; i++)
    {
        if (decodedArray[i] == decodedArray[i + 1])
        {
            reps++;
            continue;
        }
        if (reps >= 2)
        {
            var temp = decodedArray[i];
            encodedList.Add((byte)(257 - reps));
            encodedList.Add(decodedArray[i]);
            reps = 1;
        }
        else
        {

        }
    }
    return encodedList.ToArray();
}

public static byte[] PackBitsDecode(byte[] encodedArray)
{
    List<byte> decodedList = new();
    int leap = 1;
    for (int i = 0; i < encodedArray.Length; i += leap)
    {
        int convertedBytes = 0;
        byte conv = encodedArray[i];
        if (conv >= 0 && conv <= 127)
        {
            byte cpy = (byte)(conv + 1);
            for (int j = 0; j < cpy; j++)
            {
                decodedList.Add(encodedArray[i + 1 + j]);
                convertedBytes++;
            }
            leap = 1 + convertedBytes;
        }
        else if (conv >= 129 && conv <= 255)
        {
            byte rep = (byte)((256 - conv) + 1);
            for (int j = 0; j < rep; j++)
            {
                decodedList.Add(encodedArray[i + 1]);
            }
            leap = 2;
        }
        else leap = 2;
    }
    return decodedList.ToArray();
}