# D2S File Structure Outline

D2S File Structure Outline

I will outline how I broke down the D2S (Diablo 2 Save File Structure)

Some helpful sources:
 - https://github.com/WalterCouto/D2CE
 - https://github.com/dschu012/d2s-editor

[My Main inspiration to do this]
 - https://github.com/dschu012/D2SLib

Currently I am not providing any back-version support as I am only focused on the newest stuff first. However it is very possible to add it in.

## File Structure
(The total easily findable data is 765 bytes of data, a lot of it has empty area's for padding for some reason, but this is the closest I've gotten to a proper documentation on finding that data)  
  
## File Header Definitions
(typically a check to see if this is the correct file or not also some checks for modification outside the original intent)
| Offset bytes | Key           | bytes | bits  |
|--------------|---------------|-------|-------|
| 0            | Signature     | 4     | 32    |  |
| 4            | Version       | 4     | 32    |  |
| 8            | FileSize      | 4     | 32    |  |
| 12           | Checksum      | 4     | 32    |  |
|              |               |       |       |  |

## Player Information
(generic information about the character such as name, level, class, skills etc...)
| Offset bytes | Key                                               | bytes | bits  |
|--------------|---------------------------------------------------|-------|-------|
| 16           | Active Weapon                                     | 4     | 32    |  |
| 36           | Status                                            | 1     | 8     |  |
| 37           | Progression                                       | 1     | 8     |  |
| 40           | Class                                             | 1     | 8     |  |
| 43           | Player level                                      | 1     | 8     |  |
| 48           | LastPlayed                                        | 4     | 32    |  |
| 56           | AssignedSkills                                    | 64    | 512   |  |
| 120          | LeftMouseSkill                                    | 4     | 32    |  |
| 124          | RightMouseSkill                                   | 4     | 32    |  |
| 128          | SwapLeftMouseSkill                                | 4     | 32    |  |
| 132          | SwapRightMouseSkill                               | 4     | 32    |  |
| 168          | Difficulty                                        | 3     | 24    |  |
| 171          | Map ID                                            | 4     | 32    |  |
| 267          | Player Name                                       | 16    | 128   |  |
|              |                                                   |       |       |  |


## Other Information
(Just basic campaign information about the character)
| Offset bytes | Key                                               | bytes | bits  |
|--------------|---------------------------------------------------|-------|-------|
| 136          | Classic Character Menu Appearance                 | 32    | 256   |  |
| 219          | Diablo 2 Resurrected Character Menu Appearance    | 48    | 384   |  |
| 177          | Mercenary Is Dead                                 | 2     | 16    |  |
| 179          | Mercenary Seed                                    | 4     | 32    |  |
| 183          | Mercenary Name ID                                 | 2     | 16    |  |
| 185          | Mercenary Type ID                                 | 2     | 16    |  |
| 187          | Mercenary Experience                              | 4     | 32    |  |
| 713          | NPC Introduction Marker                           | 2     | 16    |  |
| 715          | NPC Introduction Size in bytes                    | 2     | 16    |  |
| 717          | NPC Introduction Normal (Not finished)            | 48    | 384   |  |
|              |                                                   |       |       |  |


## Quest Information
(Due to the multi-level bit/byte distrobution, and to avoid making this super long, I have devised a way to use bit/byte addition to pull each difficulties quest, without having to manually find each bit/byte)  
-(d = difficulty)  
-(a = act)  
formula to find a quest :  
-(d + a + x)
| Offset bytes | Key                                               | bytes | bits  |
|--------------|---------------------------------------------------|-------|-------|
| 335          | Quest Marker (Woo!)                               | 4     | 32    |  |
| 339          | Quest Version                                     | 4     | 32    |  |
| 343          | Quest Size (298 bytes)                            | 2     | 16    |  |
| 345          | Quest Normal Offset (start of byte addition)      | 96    | 768   |  |
| 441          | Quest Nightmare Offset (start of byte addition)   | 96    | 768   |  |
| 537          | Quest Hell Offset (start of byte addition)        | 96    | 768   |  |
|              | START of quest byte addition                      |       |       |  |
| d + 0        | Introduction to Warriv (Act 1)                    | 2     | 16    |  |
| d + 14       | Traveled To Act 2                                 | 2     | 16    |  |
| d + 16       | Introduction to Jerhyn                            | 2     | 16    |  |
| d + 30       | Traveled To Act 3                                 | 2     | 16    |  |
| d + 32       | Introduction to Hratli                            | 2     | 16    |  |
| d + 46       | Traveled To Act 4                                 | 2     | 16    |  |
| d + 48       | Introduction to Tyeral                            | 2     | 16    |  |
| d + 60       | Traveled To Act 5                                 | 2     | 16    |  |
| d + 62       | Introduction to Cain (Act 5)                      | 2     | 16    |  |
| d + 78       | Akara Stat Reset                                  | 1     | 8     |  |
| d + 79       | Act 5 Completed                                   | 1     | 8     |  |
|              | START of Act Offsets                              |       |       |  |
| d + 2        | Act 1 Quest Set                                   | 12    | 96    |  |
| d + 18       | Act 2 Quest Set                                   | 12    | 96    |  |
| d + 34       | Act 3 Quest Set                                   | 12    | 96    |  |
| d + 50       | Act 4 Quest Set                                   | 6     | 48    |  |
| d + 66       | Act 5 Quest Set                                   | 12    | 96    |  |
|              | START of Quest Offsets                            |       |       |  |
| d + a + 0    | Quest 1                                           | 2     | 16    |  |
| d + a + 2    | Quest 2                                           | 2     | 16    |  |
| d + a + 4    | Quest 3                                           | 2     | 16    |  |
| d + a + 6    | Quest 4                                           | 2     | 16    |  |
| d + a + 8    | Quest 5                                           | 2     | 16    |  |
| d + a + 10   | Quest 6                                           | 2     | 16    |  |
|              |                                                   |       |       |  |

## Waypoint Information
(Due to the multi-level bit/byte distrobution, and to avoid making this super long, I have devised a way to use bit/byte addition to pull each difficulties waypoints without having to manually type out each byte)  
-(d = difficulty)  
-(a = Act)
Formula to find a waypoint:
-(d + a + x)
| Offset bytes | Key                                               | bytes | bits  |
|--------------|---------------------------------------------------|-------|-------|
| 633          | Waypoint Marker (WS)                              | 2     | 16    |  |
| 635          | Waypoint Version                                  | 4     | 32    |  |
| 639          | Waypoint Size (80 bytes)                          | 2     | 16    |  |
| 641          | Waypoint Normal Offset (Marker 0x02 & 0x01)       | 2     | 16    |  |
| 665          | Waypoint Nightmare Offset (Marker 0x02 & 0x01)    | 2     | 16    |  |
| 689          | Waypoint Hell Offset (Marker 0x02 & 0x01)         | 2     | 16    |  |
|              | START of waypoint byte addition                   |       |       |  |
| d + 2        | Act 1 Waypoint Set                                | 1     | 8     |  |
| d + 3        | Act 2 Waypoint Set                                | 1     | 8     |  |
| d + 4        | Act 3 Waypoint Set                                | 1     | 8     |  |
| d + 5        | Act 4 Waypoint Set                                | 1     | 8     |  |
| d + 6        | Act 5 Waypoint Set                                | 1     | 8     |  |
|              |                                                   |       |       |  |



## Player Attribute Information
(The attribute field is a variable lengthed bit field describing specific stats such as Strength, Vitality, Energy, Dexterity etc...)
| Offset bytes | Key                                               | bytes | bits  |
|--------------|---------------------------------------------------|-------|-------|
| 765          | Attribute Marker (gf)                             | 2     | 16    |  |
|              |                                                   |       |       |  |

a = attribute offset  
n = number of attributes found  
x = attribute value which is of veriable bit length  

| Offset bits     | Key                                               | bits  |
|-----------------|---------------------------------------------------|-------|
| a + (n * 9)     | Attribute ID                                      | 9     |  |
| a + (n * 9 + x) | Attribute Value                                   | x     |  |
|                 |                                                   |       |  |  

The bit length of the attribute can be found within the **files of Diablo 2 Resurrected** which can be viewed using [CascViewer](http://www.zezula.net/en/casc/main.html)  
Specifically (**data\global\excel**) for Attributes you can find the Save Bits column in the **itemstatcost.txt**
    
## How to read/write checksumInformation
(At first calculating the checksum was a bit complicated, but I think I figured out a simple way to allow others to understand and use it)  
When calculating the checksum we first want pass all the bytes we read at the beginning, typically into a function.  
Then we want to remove the old checksum as we don't want to calculate it in our bytes. Those can be found at bytes 12-16, we can just set those to 0x00.  

Formula:  
```csharp
        public static byte[] ComputeChecksum(byte[] data)
        {
            // Remove the old checksum from the new checksum calculation
            for (int i = 12; i < 16; i++) data[i] = 0x00;
            int checksum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                /* first the current checksum is multiplied by 2
                 * second we check if checksum is less than 0, if it is we place a 1, if it's not place a 0
                 * Third data[i] takes the byte value from the index at i
                 * Finally we add everything together */
                checksum = data[i] + (checksum * 2) + (checksum < 0 ? 1 : 0);
            }
            byte[] checksumbytes = new byte[4];
            BinaryPrimitives.WriteInt32LittleEndian(checksumbytes, checksum);
            return checksumbytes;
        }
```


  
## How-To-Read Bits/Bytes
Initially you want to load all of the bytes into memory. I tend to do this to avoid having to deal with locked files. I achieved this by doing this:
```csharp
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

Which is the first 2 bytes of the signature of a D2S file...


TO BE CONTINUED
