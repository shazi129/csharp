using System;

public class TestBytes
{
    public static void Test()
    {
        byte[] uintValue = {0, 0, 0, 36 };
        Console.WriteLine(BitConverter.ToUInt32(uintValue, 0));

        Array.Reverse(uintValue);
        Console.WriteLine(BitConverter.ToUInt32(uintValue, 0));


    }
}