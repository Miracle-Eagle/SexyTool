using SexyTool.Program.Core;
using System;
using System.IO;

/// <summary> Allows Reading and Writing Bits (0 and 1) as Streams. </summary>

public class BitsStream : IDisposable
{
/** <summary> Sets a Value which Contains info about the Buffers of this Instance. </summary>
<returns> The Buffers of this Instance. </returns> */

protected int streamBuffers;

/** <summary> Gets or Sets a Value which Contains info about the BaseStream of this Instance. </summary>
<returns> The BaseStream of this Instance. </returns> */

public Stream BaseStream;

/** <summary> Gets or Sets a Value which Contains info about how the Stream should be Handled after being Opened. </summary>
<returns> A Boolean that Determines if the Stream should be left Opened or not. </returns> */

public bool LeaveOpened = false;

/** <summary> Gets or Sets a Value which Contains info about the Position of the Bits in the Stream. </summary>
<returns> The Position of the Bits in the Stream. </returns> */

protected int BitsPosition { get; set; }

/** <summary> Checks if the BaseStream of this Instance is Actually a MemoryStream. </summary>
<returns> A Boolean that Determines if the BaseStream is a MemoryStream or not. </returns> */

public bool IsMemoryStream => BaseStream is MemoryStream;

/** <summary> Gets or Sets a Value which Contains info about the Length of the BaseStream from this Instance. </summary>
<returns> The Length of the BaseStream from this Instance. </returns> */

public long Length{ get => BaseStream.Length; set => BaseStream.SetLength(value); }

/** <summary> Gets or Sets a Value which Contains info about the Position of the BaseStream from this Instance. </summary>
<returns> The Position of the BaseStream from this Instance. </returns> */

public long Position{ get => BaseStream.Position; set => BaseStream.Position = value; }

/// <summary> Creates a new Instance of the BitsStream Class. </summary>

public BitsStream() : this(new MemoryStream() )
{
}

/** <summary> Creates a new Instance of the BitsStream Class with the given Stream. </summary>
<param name = "sourceStream"> The Stream where the Instance will be Created from. </param> */

public BitsStream(Stream sourceStream)
{
BaseStream = sourceStream;
BitsPosition = 0;
}

/** <summary> Creates a new Instance of the BitsStream Class with the given Buffers. </summary>
<param name = "sourceBuffers"> The Buffers where the Instance will be Created from. </param> */

public BitsStream(byte[] sourceBuffers) : this(new MemoryStream(sourceBuffers) )
{
}

/** <summary> Creates a new Instance of the BitsStream Class with the specific Location and opening Mode. </summary>
<param name = "targetPath"> The Path where the BitsStream will be Created. </param>

<param name = "openingMode"> The Opening Mode of the Stream. </param> */

public BitsStream(string targetPath, FileMode openingMode) : this(new FileStream(targetPath, openingMode) )
{
}

/// <summary> Closes the Stream and Releases all the Resources consumed by it. </summary>

public virtual void Close() => Dispose(true);

/** <summary> Creates a new BitsStream on the specific Location and with the specific Opening Mode. </summary>
<param name = "targetPath"> The Path where the Stream will be Created. </param>

<returns> The BitsStream that was Created. </returns> */

public static BitsStream Create(string targetPath, FileMode openingMode) => new BitsStream(targetPath, openingMode);

/// <summary> Releases all the Resources consumed by the Stream. </summary>

public void Dispose() => Dispose(true);

/** <summary> Releases all the Resources consumed by the Stream. </summary>
<param="disposing"> Determines if all the Resources should be Discarded. </param> */

protected virtual void Dispose(bool disposing)
{

if(disposing)
{

if(LeaveOpened)
{
BaseStream.Flush();
}    

else
{
BaseStream.Close();
}

}

}

/** <summary> Opens a BitsStream on the specific Location. </summary>
<param name = "targetPath"> The Path where the BitsStream to be Opened is Located. </param>

<returns> The BitsStream that was Opened. </returns> */

public static BitsStream Open(string targetPath) => new BitsStream(targetPath, FileMode.Open);

/** <summary> Reads a specific Number of Bits from a BitsStream. </summary>
<param name = "bitsCount"> The Number of Bits to Read. </param>

<returns> The Bits Read. </returns> */

public int ReadBits(int bitsCount)
{
int bitsRead = 0;

for(int i = bitsCount - 1; i >= 0; i--)
{
int singleBit = ReadOneBit();
bitsRead |= singleBit << i;
}

return bitsRead;
}

/** <summary> Reads one Bit from a BitsStream. </summary>
<returns> The Bit that was Read. </returns> */

public int ReadOneBit()
{

if(BitsPosition == 0)
{
streamBuffers = BaseStream.ReadByte();

if(streamBuffers == -1)
throw new IOException("Reached end of File.");
}

BitsPosition = (BitsPosition + 7) % 8;
return (streamBuffers >> BitsPosition) & 0b1;
}

public static implicit operator Stream(BitsStream a) => a.BaseStream;
}