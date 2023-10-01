# D2S File Structure Outline

D2S File Structure Outline

I will outline how I broke down the D2S (Diablo 2 Save File Structure)

Some helpful sources:
 - https://github.com/WalterCouto/D2CE
 - https://github.com/dschu012/d2s-editor
[My Main inspiration to do this]
 - https://github.com/dschu012/D2SLib

Currently I am not providing any back-version support as I was only focused on the newest stuff first. However it is very possible to add it in.

Initially you want to load all of the bytes into memory. I tend to do this to avoid having to deal with locked files. I achieved this by doing this:
```
    File.ReadAllBytes(filepath);
```

Typically to traverse through a files bytes you will want a byte reader. When it comes to bytes and bits we also want to pay attention to the Endianness (which is the order of bits during conversion)

Here is an example of a 32-bit (int type 4 bytes total) decimal number 305419896:
 - Little Endian (LE):
    - In binary: 1000 1000 0101 0110 0011 0100 0001 0010
 - Big Endian (BE):
    - In binary: 0001 0010 0011 0100 0101 0110 1000 1000

With every 8 bits (1 byte) that is read in Little Endian, Big Endian reads in the opposite direction; I like to think of it as reading words backwards. If you Think of it like a sentence:
 - Little Endian (LE):
    - This would be normal
 - Big Endian (BE):
    - lamron eb dluow sihT

The fundamental difference between the two byte orderings:
 - In Little Endian, the least significant byte is stored at the lowest memory address, and subsequent bytes are stored in increasing order of significance.
 - In Big Endian, the most significant byte is stored at the lowest memory address, and subsequent bytes are stored in decreasing order of significance.

In binary representation, each digit (bit) can be either 0 or 1. The terms "least significant bit" (LSB) and "most significant bit" (MSB) refer to the importance or weight of a bit within a binary number.

Least Significant Bit (LSB): This is the rightmost bit in a binary number. It has the least weight or significance in determining the value of the number. It represents the smallest power of 2 in the number's binary representation.

Most Significant Bit (MSB): This is the leftmost bit in a binary number. It has the highest weight or significance in determining the value of the number. It represents the largest power of 2 in the number's binary representation.

The significance of these terms becomes more apparent when you have multi-bit numbers. For example, in an 8-bit binary number, the MSB represents a value of 2^7 (128), while the LSB represents a value of 2^0 (1). So, the MSB contributes the most to the number's value, and the LSB contributes the least.

In Little Endian and Big Endian byte orderings, the "least significant byte" and "most significant byte" follow a similar concept. The least significant byte (LSB) contains the least significant bits, and the most significant byte (MSB) contains the most significant bits of a multi-byte value.

Now that we have that out of the way a simple class to read bits is the most ideal as the structure of the D2S file contents is not always byte aligned (8 bits per byte).
```csharp
    public class BitReader
    {
        public bool[] bitArray;
        public byte[] byteArray;
        public int bitPosition = 0;
        public int bytePosition => bitPosition / 8;

        public BitReader(byte[] fileBytes)
        {
            // I like to keep the bytes handy just incase
            byteArray = fileBytes;

            // Convert the bytes into bits (bool)
            int totalBits = fileBytes.Length * 8; // Total number of bits in the byte array
            bitArray = new bool[totalBits]; // Array to hold the Bit structs

            for (int i = 0; i < fileBytes.Length; i++)
            {
                byte currentByte = fileBytes[i];
                for (int j = 0; j < 8; j++) // Loop through each bit in the byte
                {
                    int BitPosition = i * 8 + j; // Calculate the position in the Bit array
                    /*  EXAMPLE
                        Right Shift by 1 bit (Big Endian):
                        Big Endian (BE) Result: 11010101

                        Right Shift by 1 bit (Little Endian):
                        Little Endian (LE) Result: 00101010
                    */
                    int bitValue = (currentByte >> j) & 1; // Extract the bit value using little-endian order
                    bitArray[BitPosition] = (bitValue >= 1); // Check if the value is greater or equal to 1 as true is always 1 or higher
                }
            }
        }

        public bool ReadBit()
        {
            // We grab the current position in the bit array and then increase the position,
            //  because we read the bit
            return bitArray[bitPosition++];
        }

        public bool[] ReadBits(int numBits)
        {
            // We initialize a return array of bits (bools)
            bool[] result = new bool[numBits];
            
            // We loop through the number of bits requested
            // And since we didn't access the bitArray directly,
            // we used our original ReadBit() function we don't
            // have to iterate the bitPosition
            for(int i=0;i<numBits;i++) result[i] = ReadBit();

            // We return the result
            return result;
        }
    }
```

Now we have a simple way to read bits from a file read into bytes. What can we do with those bits? Well nothing yet! We may want to actually convert them to numbers or readable characters! I like to use method extenders, which you can add onto specific types i.e:
```csharp
    bool[] someBits = new bool[8] { false, false, true, false, false, true, true, true };

    public static class BoolArrayExtensions
    {
        // Only using little endian order
        public static short ToShort(this bool[] bArray)
        {
            short result = 0;
            for (int i = 0; i < bArray.Length; i++)
            {
                if (bArray[i])
                {
                    result |= (short)(1 << i);
                }
            }
            return result;
        }
    }
```

With that function we now have a way to convert that bool array into something more meaningful:
```csharp
    Console.WriteLine(someBits.ToShort());

    /**************
        OUTPUT
         228
    ***************/
```

So if you combine all of that knowledge I just gave you... And you attempt to load your D2S file, you should get an output of a negative number: (-21931)
```csharp
BitReader reader = new BitReader(File.ReadAllBytes("C:\\Users\\<USER>>\\Saved Games\\Diablo II Resurrected\\SomeBarbarian.d2s"));

Console.WriteLine(reader.ReadBits(16).ToShort());
// Output:
//  -21931

public static class BoolArrayExtensions
{
    // Only using little endian order
    public static short ToShort(this bool[] bArray)
    {
        short result = 0;
        for (int i = 0; i < bArray.Length; i++)
        {
            if (bArray[i])
            {
                result |= (short)(1 << i);
            }
        }
        return result;
    }
}

public class BitReader
{
    public bool[] bitArray;
    public byte[] byteArray;
    public int bitPosition = 0;
    public int bytePosition => bitPosition / 8;

    public BitReader(byte[] fileBytes)
    {
        // I like to keep the bytes handy just incase
        byteArray = fileBytes;

        // Convert the bytes into bits (bool)
        int totalBits = fileBytes.Length * 8; // Total number of bits in the byte array
        bitArray = new bool[totalBits]; // Array to hold the Bit structs

        for (int i = 0; i < fileBytes.Length; i++)
        {
            byte currentByte = fileBytes[i];
            for (int j = 0; j < 8; j++) // Loop through each bit in the byte
            {
                int BitPosition = i * 8 + j; // Calculate the position in the Bit array
                /*  EXAMPLE
                    Right Shift by 1 bit (Big Endian):
                    Big Endian (BE) Result: 11010101

                    Right Shift by 1 bit (Little Endian):
                    Little Endian (LE) Result: 00101010
                */
                int bitValue = (currentByte >> j) & 1; // Extract the bit value using little-endian order
                bitArray[BitPosition] = (bitValue >= 1); // Check if the value is greater or equal to 1 as true is always 1 or higher
            }
        }
    }

    public bool ReadBit()
    {
        // We grab the current position in the bit array and then increase the position,
        //  because we read the bit
        return bitArray[bitPosition++];
    }

    public bool[] ReadBits(int numBits)
    {
        // We initialize a return array of bits (bools)
        bool[] result = new bool[numBits];

        // We loop through the number of bits requested
        // And since we didn't access the bitArray directly,
        // we used our original ReadBit() function we don't
        // have to iterate the bitPosition
        for (int i = 0; i < numBits; i++) result[i] = ReadBit();

        // We return the result
        return result;
    }
}
```

However if you want to format things nicely, you could also convert that short into HEX which I like better for junk data:
```csharp
    Console.WriteLine(reader.ReadBits(16).ToShort().ToString("X2"));
    // Output:
    //  AA55
```

Which is the first 2 bytes of the signature of a D2S file. Now we can get into structure and bringing things to life:

## File Header Definitions
(typically a check to see if this is the correct file or not also some checks for modification outside the original intent)
| Offset bytes | Key           | bytes | bits  |
|--------------|---------------|-------|-------|
| 0 | Signature | 4 | 32 |  |
| 4 | Version | 4 | 32 |  |
| 8 | FileSize | 4 | 32 |  |
| 12 | Checksum | 4 | 32 |  |

## Player Information
(generic information about the character such as name, level, class, skills etc...)
| Offset bytes | Key           | bytes | bits  |
|--------------|---------------|-------|-------|
| 16 | Active Weapon | 4 | 32 |  |
| 36 | Status | 1 | 8 |  |
| 37 | Progression | 1 | 8 |  |
| 40 | Class | 1 | 8 |  |
| 43 | Player level | 1 | 8 |  |
| 48 | LastPlayed | 4 | 32 |  |
| 56 | AssignedSkills | 64 | 512 |  |


TO BE CONTINUED
