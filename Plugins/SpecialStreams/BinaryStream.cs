using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary> Allows Stream Reading and Writing in a Binary Format. </summary>

public class BinaryStream : IDisposable
{
/** <summary> Sets a Value which Contains info about the Buffers of this Instance. </summary>
<returns> The Buffers of this Instance. </returns> */

protected readonly byte[] streamBuffers;

/** <summary> Gets or Sets a Value which Contains info about the BaseStream of this Instance. </summary>
<returns> The BaseStream of this Instance. </returns> */

public Stream BaseStream;

/** <summary> Gets or Sets a Value which Contains info about how the Stream should be Handled after being Opened. </summary>
<returns> <b>true</b> if the Stream should be left Opened; otherwise, <b>false</b>. </returns> */

public bool LeaveOpened = false;

/** <summary> Checks if the BaseStream of this Instance is Actually a MemoryStream. </summary>
<returns> <b>true</b> if the BaseStream is a MemoryStream; otherwise, <b>false</b>. </returns> */

public bool IsMemoryStream => BaseStream is MemoryStream;

/** <summary> Gets or Sets a Value which Contains info about the Length of the BaseStream from this Instance. </summary>
<returns> The Length of the BaseStream from this Instance. </returns> */

public long Length{ get => BaseStream.Length; set => BaseStream.SetLength(value); }

/** <summary> Gets or Sets a Value which Contains info about the Position of the BaseStream from this Instance. </summary>
<returns> The Position of the BaseStream from this Instance. </returns> */

public long Position{ get => BaseStream.Position; set => BaseStream.Position = value; }

/// <summary> Creates a new Instance of the <c>BinaryStream</c> Class. </summary>

public BinaryStream() : this(new MemoryStream() )
{
}

/** <summary> Creates a new Instance of the <c>BinaryStream</c> Class with the given Stream. </summary>
<param name = "sourceStream"> The Stream where the Instance will be Created from. </param> */

public BinaryStream(Stream sourceStream)
{
BaseStream = sourceStream;
streamBuffers = new byte[16];
}

/** <summary> Creates a new Instance of the <c>BinaryStream</c> Class with the given Buffers. </summary>
<param name = "sourceBuffers"> The Buffers where the Instance will be Created from. </param> */

public BinaryStream(byte[] sourceBuffers) : this(new MemoryStream(sourceBuffers) )
{
}

/** <summary> Creates a new Instance of the <c>BinaryStream</c> Class with the specific Location and opening Mode. </summary>

<param name = "targetPath"> The Path where the BinaryStream will be Created. </param>
<param name = "openingMode"> The Opening Mode of the Stream. </param> */

public BinaryStream(string targetPath, FileMode openingMode) : this(new FileStream(targetPath, openingMode) )
{
}

/** <summary> Checks if the Provided data Encoding is Set or not. </summary>

<param name = "sourceEncoding"> The Encoding to be Analized. </param>

<returns> The Validated Encoding. </returns> */

private static void CheckDataEncoding(ref Encoding sourceEncoding) => sourceEncoding ??= Encoding.UTF8;

/** <summary> Checks if the Provided endian Order is Set or not. </summary>

<param name = "sourceOrder"> The Order to be Analized. </param>
<param name = "isForStrings"> A Boolean that Determines if the Endian must be used for Strings Encoding. </param>

<returns> The Validated Order. </returns> */

private static void CheckEndianOrder(ref Endian sourceOrder, bool isForStrings = false)
{

if(isForStrings)
sourceOrder = (sourceOrder == Endian.None) ? Endian.Big : sourceOrder;

else
sourceOrder = (sourceOrder == Endian.None) ? Endian.Little : sourceOrder;

}

/** <summary> Flips the provided Bytes in case their endian Order is LitteEndian. </summary>

<param name = "sourceBytes"> The Bytes to be Sorted. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

private static void SortEndianBytes(ref byte[] sourceBytes, Endian endianOrder)
{

if(endianOrder != Endian.Little)
return;

else
Array.Reverse(sourceBytes);

}

/// <summary> Closes the Stream and Releases all the Resources consumed by it. </summary>

public virtual void Close() => Dispose(true);

/** <summary> Reads a 8-bits Integer and Compares it with the given one as an expected Value. </summary>
<param name = "expectedValue"> The Value to be Expected. </param> */

public void CompareByte(byte expectedValue)
{
byte inputValue = ReadByte();
GenericValueComparisson(inputValue, expectedValue);
}

/** <summary> Reads a 8-bits signed Integer and Compares it with the given one as an expected Value. </summary>
<param name = "expectedValue"> The Value to be Expected. </param> */

public void CompareSByte(sbyte expectedValue)
{
sbyte inputValue = ReadSByte();
GenericValueComparisson(inputValue, expectedValue);
}

/** <summary> Reads a 16-bits Integer and Compares it with the given one as an expected Value. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void CompareShort(short expectedValue, Endian endianOrder = default)
{
short inputValue = ReadShort(endianOrder);
GenericValueComparisson(inputValue, expectedValue);
}

/** <summary> Reads a 16-bits unsigned Integer and Compares it with the given one as an expected Value. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void CompareUShort(ushort expectedValue, Endian endianOrder = default)
{
ushort inputValue = ReadUShort(endianOrder);
GenericValueComparisson(inputValue, expectedValue);
}

/** <summary> Reads a 32-bits Integer and Compares it with the given one as an expected Value. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void CompareInt(int expectedValue, Endian endianOrder = default)
{
int inputValue = ReadInt(endianOrder);
GenericValueComparisson(inputValue, expectedValue);
}

/** <summary> Reads a 32-bits unsigned Integer and Compares it with the given one as an expected Value. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void CompareUInt(uint expectedValue, Endian endianOrder = default)
{
uint inputValue = ReadUInt(endianOrder);
GenericValueComparisson(inputValue, expectedValue);
}

/** <summary> Reads a 64-bits Integer and Compares it with the given one as an expected Value. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void CompareLong(long expectedValue, Endian endianOrder = default)
{
long inputValue = ReadLong(endianOrder);
GenericValueComparisson(inputValue, expectedValue);
}

/** <summary> Reads a 64-bits unsigned Integer and Compares it with the given one as an expected Value. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void CompareULong(ulong expectedValue, Endian endianOrder = default)
{
ulong inputValue = ReadULong(endianOrder);
GenericValueComparisson(inputValue, expectedValue);
}

/** <summary> Reads an Array of Bytes and Compares it with the given one as an Expected Value. </summary>
<param name = "expectedBytes"> The Bytes to be Expected. </param> */

public void CompareBytes(byte[] expectedBytes)
{
int expectedBytesCount = expectedBytes.Length;
byte[] inputBytes = ReadBytes(expectedBytesCount);

for(int i = 0; i < expectedBytesCount; i++)
GenericValueComparisson(inputBytes[i], expectedBytes[i]);

}

/** <summary> Reads an Array of Bytes and Compares it with the given one as an Expected Value. </summary>

<param name = "expectedBytes"> The Bytes to be Expected. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void CompareString(string expectedString, Encoding dataEncoding = default, Endian endianOrder = default)
{
string inputString = ReadString(expectedString.Length, dataEncoding, endianOrder);
GenericValueComparisson(inputString, expectedString);
}

/** <summary> Copies the specified Stream to this Instance. </summary>
<param name = "sourceStream"> The Stream to Copy. </param> */

public void CopyTo(Stream sourceStream) => BaseStream.CopyTo(sourceStream);

/** <summary> Creates a new BinaryStream on the specific Location and with the specific Opening Mode. </summary>

<param name = "targetPath"> The Path where the Stream will be Created. </param>

<returns> The BinaryStream that was Created. </returns> */

public static BinaryStream Create(string targetPath, FileMode openingMode) => new(targetPath, openingMode);

/// <summary> Releases all the Resources consumed by the Stream. </summary>

public void Dispose() => Dispose(true);

/** <summary> Releases all the Resources consumed by the Stream. </summary>
<param name ="disposing"> Determines if all the Resources should be Discarded. </param> */

protected virtual void Dispose(bool disposing)
{

if(disposing)
{

if(LeaveOpened)
BaseStream.Flush();

else
BaseStream.Close();

}

}

/** <summary> Fills the Buffer of the current BinaryStream with the given amount of Bytes. </summary>
<param name = "bytesCount"> The Number of Bytes to Fill. </param>> */

protected void FillBuffer(int bytesCount)
{
int totalBytes = 0;
int bytesRead;

if(bytesCount == 1)
{
bytesRead = BaseStream.ReadByte();

if(bytesRead == -1)
throw new IOException("Reached end of File");

streamBuffers[0] = (byte)bytesRead;
return;
}

do
{
bytesRead = BaseStream.Read(streamBuffers, totalBytes, bytesCount - totalBytes);

if(bytesCount == 0)
throw new IOException("The File is Empty");

totalBytes += bytesRead;
}

while(totalBytes < bytesCount);
}

/** <summary> Makes a Comparisson between two Values of the same Type. </summary>
<remarks> If the Value read is different from the Value expected, this method throws an Exception. </remarks>

<param name = "sourceValue"> The Value to be Compared. </param>
<param name = "expectedValue"> The Value expected. </param> */

private static void GenericValueComparisson<T>(T sourceValue, T expectedValue)
{

if(!sourceValue.Equals(expectedValue) )
{
string errorMsg = $"The \"{typeof(T).Name}\" read ({sourceValue}) differs from the expected Value ({expectedValue})";
throw new DataMisalignedException(errorMsg);
}

else
return;

}

/** <summary> Reads a String until Zero (0x00 in Bytes) is Reached, and then, returns to the Initial Offset. </summary>

<param name = "dataOffset"> The Offset where the Data Starts. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The String Obtained. </returns> */

public string GetStringUntilZero(long dataOffset, Encoding dataEncoding = default, Endian endianOrder = default)
{
long initialOffset = BaseStream.Position;
BaseStream.Position = dataOffset;

string inputString = ReadStringUntilZero(dataEncoding, endianOrder);
BaseStream.Position = initialOffset;

return inputString;
}

/** <summary> Opens a BinaryStream on the specific Location. </summary>

<param name = "targetPath"> The Path where the BinaryStream to be Opened is Located. </param>

<returns> The BinaryStream that was Opened. </returns> */

public static BinaryStream Open(string targetPath) => new(targetPath, FileMode.Open);

/** <summary> Opens a BinaryStream for Writing. </summary>

<param name = "targetPath"> The Path where the BinaryStream to be Opened is Located. </param>

<returns> The BinaryStream that was Opened. </returns> */

public static BinaryStream OpenWrite(string targetPath) => new(targetPath, FileMode.OpenOrCreate);

/** <summary> Reads a 8-bits Integer from a BinaryStream and then goes back in the Stream. </summary>
<returns> The Byte that was Read. </returns> */

public byte PeekByte()
{
byte inputByte = ReadByte();
Position--;

return inputByte;
}

/** <summary> Reads an unsigned 16-bits Integer from a BinaryStream and then goes back in the Stream. </summary>
<returns> The UShort that was Read. </returns> */

public ushort PeekUShort(Endian endianOrder = default)
{
ushort inputValue = ReadUShort(endianOrder);
Position -= 2;

return inputValue;
}

/** <summary> Reads a 32-bits Integer from a BinaryStream and then goes back in the Stream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The Byte that was Read. </returns> */

public int PeekInt(Endian endianOrder = default)
{
int inputValue = ReadInt(endianOrder);
Position -= 4;

return inputValue;
}

/** <summary> Reads a String from a BinaryStream and then goes back in the Stream. </summary>

<param name = "stringLength"> The Length of the String. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The Byte that was Read. </returns> */

public string PeekString(int stringLength, Encoding dataEncoding = default, Endian endianOrder = default)
{
string inputString = ReadString(stringLength, dataEncoding, endianOrder);
Position -= stringLength;

return inputString;
}

/** <summary> Reads the Buffers from a BinaryStream. </summary>

<param name = "targetBuffers"> The Buffers to be Read. </param>

<returns> The Number of Bytes read from the Memory Buffers. </returns> */

public int Read(Span<byte> targetBuffers) => BaseStream.Read(targetBuffers);

/** <summary> Reads the Buffers from a BinaryStream. </summary>

<param name = "targetBuffers"> The Buffers to be Read. </param>
<param name = "dataOffset"> The Offset where the Data Starts. </param>
<param name = "blockSize"> The Number of Bytes to be Read. </param>

<returns> The Number of Bytes read from the Memory Buffers. </returns> */

public int Read(byte[] targetBuffers, int dataOffset, int blockSize) => BaseStream.Read(targetBuffers, dataOffset, blockSize);

/** <summary> Reads a Boolean from a BinaryStream. </summary>
<returns> The Bool Value that was Read. </returns> */

public bool ReadBool()
{
FillBuffer(1);
return streamBuffers[0] != 0;
}

/** <summary> Reads a Unicode Character from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The Char that was Read. </returns> */

public char ReadChar(Endian endianOrder = default) => (char)ReadUShort(endianOrder);

/** <summary> Reads a 8-bits Integer from a BinaryStream. </summary>
<returns> The Byte that was Read. </returns> */

public byte ReadByte()
{
FillBuffer(1);
return streamBuffers[0];
}

/** <summary> Reads a 8-bits signed Integer from a BinaryStream. </summary>
<returns> The SByte that was Read. </returns> */

public sbyte ReadSByte()
{
FillBuffer(1);
return (sbyte)streamBuffers[0];
}

/** <summary> Reads a 16-bits Integer from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The Short that was Read. </returns> */

public short ReadShort(Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);
FillBuffer(2);

if(endianOrder == Endian.Big)
return (short)(streamBuffers[1] | (streamBuffers[0] << 8) );

return (short)(streamBuffers[0] | (streamBuffers[1] << 8) );
}

/** <summary> Reads a 16-bits unsigned Integer from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The UShort that was Read. </returns> */

public ushort ReadUShort(Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);
FillBuffer(2);

if(endianOrder == Endian.Big)
return (ushort)(streamBuffers[1] | (streamBuffers[0] << 8) );

return (ushort)(streamBuffers[0] | (streamBuffers[1] << 8) );
}

/** <summary> Reads a 24-bits Integer from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The Triple-Byte that was Read. </returns> */

public int ReadTripleByte(Endian endianOrder = default)
{
uint inputValue = ReadUTripleByte(endianOrder);

if( (inputValue & 0x800000) != 0) 
inputValue |= 0xff000000;
	
return (int)inputValue;
}

/** <summary> Reads a 24-bits unsigned Integer from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The unsigned Triple-Byte that was Read. </returns> */

public uint ReadUTripleByte(Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);
FillBuffer(3);

if(endianOrder == Endian.Big)
return (uint)(streamBuffers[2] | (streamBuffers[1] << 8) | (streamBuffers[0] << 16) );

return (uint)(streamBuffers[0] | (streamBuffers[1] << 8) | (streamBuffers[2] << 16) );
}

/** <summary> Reads a 32-bits Integer from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The Integer that was Read. </returns> */

public int ReadInt(Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);
FillBuffer(4);

if(endianOrder == Endian.Big)
return streamBuffers[3] | (streamBuffers[2] << 8) | (streamBuffers[1] << 16) | (streamBuffers[0] << 24);

return streamBuffers[0] | (streamBuffers[1] << 8) | (streamBuffers[2] << 16) | (streamBuffers[3] << 24);
}

/** <summary> Reads an 32-bits unsigned Integer from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The unsigned Integer that was Read. </returns> */

public uint ReadUInt(Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);
FillBuffer(4);

if(endianOrder == Endian.Big)
return (uint)(streamBuffers[3] | (streamBuffers[2] << 8) | (streamBuffers[1] << 16) | (streamBuffers[0] << 24) );

return (uint)(streamBuffers[0] | (streamBuffers[1] << 8) | (streamBuffers[2] << 16) | (streamBuffers[3] << 24) );
}

/** <summary> Reads a 64-bits Integer from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The Long that was Read. </returns> */

public long ReadLong(Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);
FillBuffer(8);

if(endianOrder == Endian.Big)
return (long)( ( ( (ulong)(uint)(streamBuffers[3] | (streamBuffers[2] << 8) | (streamBuffers[1] << 16) | (streamBuffers[0] << 24) ) ) << 32) | ( (uint)(streamBuffers[7] | (streamBuffers[6] << 8) | (streamBuffers[5] << 16) | (streamBuffers[4] << 24) ) ) );

return (long)( ( ( (ulong)(uint)(streamBuffers[4] | (streamBuffers[5] << 8) | (streamBuffers[6] << 16) | (streamBuffers[7] << 24) ) ) << 32) | ( (uint)(streamBuffers[0] | (streamBuffers[1] << 8) | (streamBuffers[2] << 16) | (streamBuffers[3] << 24) ) ) );
}

/** <summary> Reads a 64-bits unsigned Integer from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The ULong that was Read. </returns> */

public ulong ReadULong(Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);
FillBuffer(8);

if(endianOrder == Endian.Big)
return ( ( (ulong)(uint)(streamBuffers[3] | (streamBuffers[2] << 8) | (streamBuffers[1] << 16) | (streamBuffers[0] << 24) ) ) << 32) | ( (uint)(streamBuffers[7] | (streamBuffers[6] << 8) | (streamBuffers[5] << 16) | (streamBuffers[4] << 24) ) );

return ( ( (ulong)(uint)(streamBuffers[4] | (streamBuffers[5] << 8) | (streamBuffers[6] << 16) | (streamBuffers[7] << 24) ) ) << 32) | ( (uint)(streamBuffers[0] | (streamBuffers[1] << 8) | (streamBuffers[2] << 16) | (streamBuffers[3] << 24) ) );
}

/** <summary> Reads a VarInt (32-bits variant Integer) from a BinaryStream. </summary>
<returns> The VarInt read represented as a 32-bits Value. </returns> */

public int ReadVarInt()
{
int varInt = 0;
int integerBase = 0;

byte inputValue;

do
{

if(integerBase == 35)
throw new Exception("Not a 32-bits Integer");

inputValue = ReadByte();
varInt |= (inputValue & 0x7F) << integerBase;

integerBase += 7;
}

while( (inputValue & 0x80) != 0);
	
return varInt;
}

/** <summary> Reads an unsigned VarInt (32-bits variant Integer) from a BinaryStream. </summary>
<returns> The VarInt read represented as an unsigned 32-bits Value. </returns> */

public uint ReadUVarInt() => (uint)ReadVarInt();

/** <summary> Reads a VarLong (64-bits variant Integer) from a BinaryStream. </summary>
<returns> The VarInt read represented as a 64-bits Value. </returns> */

public long ReadVarLong()
{
long varInt = 0;
int integerBase = 0;

byte inputValue;

do
{

if(integerBase == 70)
throw new Exception("Not a 64-bits Integer");

inputValue = ReadByte();
varInt |= ( (long)(inputValue & 0x7F) ) << integerBase;

integerBase += 7;
}

while( (inputValue & 0x80) != 0);

return varInt;
}

/** <summary> Reads an unsigned VarLong (64-bits variant Integer) from a BinaryStream. </summary>
<returns> The VarInt read represented as an unsigned 64-bits Value. </returns> */

public ulong ReadUVarLong() => (ulong)ReadVarLong();

/** <summary> Reads a VarInt from a BinaryStream as a ZigZag Integer. </summary>
<returns> The ZigZag Int that was Read. </returns> */

public int ReadZigZagInt()
{
uint inputValue = ReadUVarInt();

if( (inputValue & 0b1) == 0)
return (int)(inputValue >> 1);

return -(int)( (inputValue + 1) >> 1);
}

/** <summary> Reads a VarInt from a BinaryStream as an unsigned ZigZag Integer. </summary>
<returns> The unsigned ZigZag Int that was Read. </returns> */

public uint ReadUZigZagInt() => (uint)ReadZigZagInt();

/** <summary> Reads a VarInt from a BinaryStream as a ZigZag Long. </summary>
<returns> The ZigZag Long that was Read. </returns> */

public long ReadZigZagLong()
{
ulong inputValue = ReadUVarLong();

if( (inputValue & 0b1) == 0)
return (long)(inputValue >> 1);

return -(long)( (inputValue + 1) >> 1);
}

/** <summary> Reads a VarInt from a BinaryStream as an unsigned ZigZag Long. </summary>
<returns> The unsigned ZigZag Long that was Read. </returns> */

public ulong ReadUZigZagLong() => (ulong)ReadZigZagLong();

/** <summary> Reads a 32-bits Float-point from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The Float Value that was Read. </returns> */

public float ReadFloat(Endian endianOrder = default)
{
byte[] inputBytes = BitConverter.GetBytes(ReadUInt(endianOrder) );
return BitConverter.ToSingle(inputBytes, 0);
}

/** <summary> Reads a 64-bits Float-point from a BinaryStream. </summary>

<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The Double Value that was Read. </returns> */

public double ReadDouble(Endian endianOrder = default)
{
byte[] inputBytes = BitConverter.GetBytes(ReadULong(endianOrder) );
return BitConverter.ToDouble(inputBytes, 0);
}

/** <summary> Reads an Array of Bytes from a BinaryStream. </summary>

<param name = "bytesCount"> The Number of Bytes to Read. </param>
<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The Array of Bytes that was Read. </returns> */

public byte[] ReadBytes(long bytesCount, Endian endianOrder = default)
{
byte[] inputBytes;

if(bytesCount == 0)
inputBytes = Array.Empty<byte>();

else
{
inputBytes = new byte[bytesCount];			
int totalBytes = 0;

do
{
int bytesRead = BaseStream.Read(inputBytes, totalBytes, (int)bytesCount);

if(bytesRead == 0)
break;

totalBytes += bytesRead;
bytesCount -= bytesRead;
}

while(bytesCount > 0);

if(totalBytes != inputBytes.Length)
{
byte[] fillingBuffer = new byte[totalBytes];
Array.Copy(inputBytes, 0, fillingBuffer, 0, fillingBuffer.Length);

inputBytes = fillingBuffer;
}

}

SortEndianBytes(ref inputBytes, endianOrder);
return inputBytes;
}

/** <summary> Reads a String from a BinaryStream. </summary>

<param name = "stringLength"> The Length of the String. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadString(int stringLength, Encoding dataEncoding = default, Endian endianOrder = default)
{
CheckDataEncoding(ref dataEncoding);
CheckEndianOrder(ref endianOrder, true);

byte[] inputBytes = ReadBytes(stringLength, endianOrder);
return dataEncoding.GetString(inputBytes);
}

/** <summary> Reads a String from a BinaryStream along with its Length as an unsigned 8-bits Integer. </summary>

<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadStringByByteLength(Encoding dataEncoding = default, Endian endianOrder = default)
{
byte stringLength = ReadByte();
return ReadString(stringLength, dataEncoding, endianOrder);
}

/** <summary> Reads a String from a BinaryStream along with its Length as an unsigned 16-bits Integer. </summary>

<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadStringByUShortLength(Encoding dataEncoding = default, Endian endianOrder = default)
{
ushort stringLength = ReadUShort(endianOrder);
return ReadString(stringLength, dataEncoding, endianOrder);
}

/** <summary> Reads a String from a BinaryStream along with its Length as an unsigned 32-bits Integer. </summary>

<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadStringByIntLength(Encoding dataEncoding = default, Endian endianOrder = default)
{
int stringLength = ReadInt(endianOrder);
return ReadString(stringLength, dataEncoding, endianOrder);
}

/** <summary> Reads a String from a BinaryStream along with its Length as a 32-bits Variant Header. </summary>

<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadStringByVarIntLength(Encoding dataEncoding = default, Endian endianOrder = default)
{
int stringLength = ReadVarInt();
return ReadString(stringLength, dataEncoding, endianOrder);
}

/** <summary> Reads a String from a BinaryStream until Zero (0x00 in Bytes) is Reached. </summary>

<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadStringUntilZero(Encoding dataEncoding = default, Endian endianOrder = default)
{
CheckDataEncoding(ref dataEncoding);
CheckEndianOrder(ref endianOrder, true);

List<byte> bytesList = new();
byte inputByte;

while(true)
{

if( (inputByte = ReadByte() ) == 0x0)
break;

bytesList.Add(inputByte);
}

byte[] bytesRead = bytesList.ToArray();
SortEndianBytes(ref bytesRead, endianOrder);

return dataEncoding.GetString(bytesRead);
}

/** <summary> Sets the Position within the Current BinaryStream. </summary>

<param name = "targetOffset"> The Offset to be Set. </param>
<param name = "seekOrigin"> The Origin of the Seek. </param> */

public void Seek(long targetOffset, SeekOrigin seekOrigin) => BaseStream.Seek(targetOffset, seekOrigin);

/** <summary> Writes the specified Buffers into a BinaryStream. </summary>
<param name = "targetBuffers"> The Buffers to be Written. </param> */

public void Write(Span<byte> targetBuffers) => BaseStream.Write(targetBuffers);

/** <summary> Writes the specified Buffers into a BinaryStream. </summary>

<param name = "targetBuffers"> The Buffers to be Written. </param>
<param name = "dataOffset"> The Offset where the Data Starts. </param>
<param name = "blockSize"> The Number of Bytes to be Written. </param> */

public void Write(byte[] targetBuffers, int dataOffset, int blockSize) => BaseStream.Write(targetBuffers, dataOffset, blockSize);

/** <summary> Writes the specified Boolean into a BinaryStream. </summary>
<param name = "targetValue"> The Bool to be Written. </param> */

public void WriteBool(bool targetValue)
{
streamBuffers[0] = (byte)(targetValue ? 1u : 0u);
BaseStream.Write(streamBuffers, 0, 1);
}

/** <summary> Writes the specified Unicode Character into a BinaryStream. </summary>

<param name = "targetValue"> The Char to be Written. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteChar(char targetValue, Endian endianOrder = default) => WriteUShort(targetValue, endianOrder);

/** <summary> Writes the specified 8-bits Integer into a BinaryStream. </summary>
<param name = "targetValue"> The Byte to be Written. </param> */

public void WriteByte(byte targetValue) => BaseStream.WriteByte(targetValue);

/** <summary> Writes the specified 8-bits signed Integer into a BinaryStream. </summary>
<param name = "targetValue"> The SByte to be Written. </param> */

public void WriteSByte(sbyte targetValue) => BaseStream.WriteByte( (byte)targetValue);

/** <summary> Writes the specified 16-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The Short to be Written. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteShort(short targetValue, Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);

if(endianOrder == Endian.Big)
{
streamBuffers[1] = (byte)targetValue;
streamBuffers[0] = (byte)(targetValue >> 8);
}

else
{
streamBuffers[0] = (byte)targetValue;
streamBuffers[1] = (byte)(targetValue >> 8);
}

BaseStream.Write(streamBuffers, 0, 2);
}

/** <summary> Writes the specified 16-bits unsigned Integer into a BinaryStream. </summary>

<param name = "targetValue"> The UShort to be Written. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteUShort(ushort targetValue, Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);

if(endianOrder == Endian.Big)
{
streamBuffers[1] = (byte)targetValue;
streamBuffers[0] = (byte)(targetValue >> 8);
}

else
{
streamBuffers[0] = (byte)targetValue;
streamBuffers[1] = (byte)(targetValue >> 8);
}

BaseStream.Write(streamBuffers, 0, 2);
}

/** <summary> Writes the specified 24-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The Triple-Byte to be Written. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteTripleByte(int targetValue, Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);

if(endianOrder == Endian.Big)
{
streamBuffers[2] = (byte)targetValue;
streamBuffers[1] = (byte)(targetValue >> 8);
streamBuffers[0] = (byte)(targetValue >> 16);
}

else
{
streamBuffers[0] = (byte)targetValue;
streamBuffers[1] = (byte)(targetValue >> 8);
streamBuffers[2] = (byte)(targetValue >> 16);
}

BaseStream.Write(streamBuffers, 0, 3);
}

/** <summary> Writes the specified 24-bits unsigned Integer into a BinaryStream. </summary>

<param name = "targetValue"> The unsigned Triple-Byte to be Written. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteUTripleByte(uint targetValue, Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);

if(endianOrder == Endian.Big)
{
streamBuffers[2] = (byte)targetValue;
streamBuffers[1] = (byte)(targetValue >> 8);
streamBuffers[0] = (byte)(targetValue >> 16);
}

else
{
streamBuffers[0] = (byte)targetValue;
streamBuffers[1] = (byte)(targetValue >> 8);
streamBuffers[2] = (byte)(targetValue >> 16);
}

BaseStream.Write(streamBuffers, 0, 3);
}

/** <summary> Writes the specified 32-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The Integer to be Written. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteInt(int targetValue, Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);

if(endianOrder == Endian.Big)
{
streamBuffers[3] = (byte)targetValue;
streamBuffers[2] = (byte)(targetValue >> 8);
streamBuffers[1] = (byte)(targetValue >> 16);
streamBuffers[0] = (byte)(targetValue >> 24);
}

else
{
streamBuffers[0] = (byte)targetValue;
streamBuffers[1] = (byte)(targetValue >> 8);
streamBuffers[2] = (byte)(targetValue >> 16);
streamBuffers[3] = (byte)(targetValue >> 24);
}

BaseStream.Write(streamBuffers, 0, 4);
}

/** <summary> Writes the specified 32-bits unsigned Integer into a BinaryStream. </summary>

<param name = "targetValue"> The unsigned Integer to be Written. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteUInt(uint targetValue, Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);

if(endianOrder == Endian.Big)
{
streamBuffers[3] = (byte)targetValue;
streamBuffers[2] = (byte)(targetValue >> 8);
streamBuffers[1] = (byte)(targetValue >> 16);
streamBuffers[0] = (byte)(targetValue >> 24);
}

else
{
streamBuffers[0] = (byte)targetValue;
streamBuffers[1] = (byte)(targetValue >> 8);
streamBuffers[2] = (byte)(targetValue >> 16);
streamBuffers[3] = (byte)(targetValue >> 24);
}

BaseStream.Write(streamBuffers, 0, 4);
}

/** <summary> Writes the specified 64-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The Long to be Written. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteLong(long targetValue, Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);

if(endianOrder == Endian.Big)
{
streamBuffers[7] = (byte)targetValue;
streamBuffers[6] = (byte)(targetValue >> 8);
streamBuffers[5] = (byte)(targetValue >> 16);
streamBuffers[4] = (byte)(targetValue >> 24);
streamBuffers[3] = (byte)(targetValue >> 32);
streamBuffers[2] = (byte)(targetValue >> 40);
streamBuffers[1] = (byte)(targetValue >> 48);
streamBuffers[0] = (byte)(targetValue >> 56);
}

else
{
streamBuffers[0] = (byte)targetValue;
streamBuffers[1] = (byte)(targetValue >> 8);
streamBuffers[2] = (byte)(targetValue >> 16);
streamBuffers[3] = (byte)(targetValue >> 24);
streamBuffers[4] = (byte)(targetValue >> 32);
streamBuffers[5] = (byte)(targetValue >> 40);
streamBuffers[6] = (byte)(targetValue >> 48);
streamBuffers[7] = (byte)(targetValue >> 56);
}

BaseStream.Write(streamBuffers, 0, 8);
}

/** <summary> Writes the specified 64-bits unsigned Integer into a BinaryStream. </summary>

<param name = "targetValue"> The ULong to be Written. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteULong(ulong targetValue, Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);

if(endianOrder == Endian.Big)
{
streamBuffers[7] = (byte)targetValue;
streamBuffers[6] = (byte)(targetValue >> 8);
streamBuffers[5] = (byte)(targetValue >> 16);
streamBuffers[4] = (byte)(targetValue >> 24);
streamBuffers[3] = (byte)(targetValue >> 32);
streamBuffers[2] = (byte)(targetValue >> 40);
streamBuffers[1] = (byte)(targetValue >> 48);
streamBuffers[0] = (byte)(targetValue >> 56);
}

else
{
streamBuffers[0] = (byte)targetValue;
streamBuffers[1] = (byte)(targetValue >> 8);
streamBuffers[2] = (byte)(targetValue >> 16);
streamBuffers[3] = (byte)(targetValue >> 24);
streamBuffers[4] = (byte)(targetValue >> 32);
streamBuffers[5] = (byte)(targetValue >> 40);
streamBuffers[6] = (byte)(targetValue >> 48);
streamBuffers[7] = (byte)(targetValue >> 56);
}

BaseStream.Write(streamBuffers, 0, 8);
}

/** <summary> Writes the specified VarInt (32-bits variant Integer) into a BinaryStream. </summary>
<param name = "targetValue"> The VarInt to be Written. </param> */

public void WriteVarInt(int targetValue)
{
uint outputValue;

for(outputValue = (uint)targetValue; outputValue >= 128; outputValue >>= 7)
WriteByte( (byte)(outputValue | 0x80) );

WriteByte( (byte)outputValue);
}

/** <summary> Writes the specified UVarInt (32-bits variant Integer) into a BinaryStream. </summary>
<param name = "targetValue"> The unsigned VarInt to be Written. </param> */

public void WriteUVarInt(uint targetValue)
{
uint outputValue;

for(outputValue = targetValue; outputValue >= 128; outputValue >>= 7)
WriteByte( (byte)(outputValue | 0x80) );

WriteByte( (byte)outputValue);
}

/** <summary> Writes the specified VarLong (64-bits variant Integer) into a BinaryStream. </summary>
<param name = "targetValue"> The VarLong to be Written. </param> */

public void WriteVarLong(long targetValue)
{
ulong outputValue;

for(outputValue = (ulong)targetValue; outputValue >= 128; outputValue >>= 7)
WriteByte( (byte)(outputValue | 0x80) );

WriteByte( (byte)outputValue);
}

/** <summary> Writes the specified UVarLong (64-bits variant Integer) into a BinaryStream. </summary>
<param name = "targetValue"> The unsigned VarLong to be Written. </param> */

public void WriteUVarLong(ulong targetValue)
{
ulong outputValue;

for(outputValue = targetValue; outputValue >= 128; outputValue >>= 7)
WriteByte( (byte)(outputValue | 0x80) );

WriteByte( (byte)outputValue);
}

/** <summary> Writes the specified ZigZag 32-bits Integer into a BinaryStream. </summary>
<param name = "targetValue"> The ZigZag Int to be Written. </param> */

public void WriteZigZagInt(int targetValue) => WriteVarInt( (targetValue << 1) ^ (targetValue >> 31) );

/** <summary> Writes the specified ZigZag 64-bits Integer into a BinaryStream. </summary>
<param name = "targetValue"> The ZigZag Long to be Written. </param> */

public void WriteZigZagLong(long targetValue) => WriteVarLong( (targetValue << 1) ^ (targetValue >> 63) );

/** <summary> Writes the specified 32-bits Floating-point into a BinaryStream. </summary>
<param name = "targetValue"> The ZigZag Long to be Written. </param>

<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteFloat(float targetValue, Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);
uint outputValue = BitConverter.ToUInt32(BitConverter.GetBytes(targetValue), 0);

if(endianOrder == Endian.Big)
{
streamBuffers[3] = (byte)outputValue;
streamBuffers[2] = (byte)(outputValue >> 8);
streamBuffers[1] = (byte)(outputValue >> 16);
streamBuffers[0] = (byte)(outputValue >> 24);
}

else
{
streamBuffers[0] = (byte)outputValue;
streamBuffers[1] = (byte)(outputValue >> 8);
streamBuffers[2] = (byte)(outputValue >> 16);
streamBuffers[3] = (byte)(outputValue >> 24);
}

BaseStream.Write(streamBuffers, 0, 4);
}

/** <summary> Writes the specified 64-bits Floating-point into a BinaryStream. </summary>

<param name = "targetValue"> The Double to be Written. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteDouble(double targetValue, Endian endianOrder = default)
{
CheckEndianOrder(ref endianOrder);
ulong outputValue = BitConverter.ToUInt64(BitConverter.GetBytes(targetValue), 0);

if(endianOrder == Endian.Big)
{
streamBuffers[7] = (byte)outputValue;
streamBuffers[6] = (byte)(outputValue >> 8);
streamBuffers[5] = (byte)(outputValue >> 16);
streamBuffers[4] = (byte)(outputValue >> 24);
streamBuffers[3] = (byte)(outputValue >> 32);
streamBuffers[2] = (byte)(outputValue >> 40);
streamBuffers[1] = (byte)(outputValue >> 48);
streamBuffers[0] = (byte)(outputValue >> 56);
}

else
{
streamBuffers[0] = (byte)outputValue;
streamBuffers[1] = (byte)(outputValue >> 8);
streamBuffers[2] = (byte)(outputValue >> 16);
streamBuffers[3] = (byte)(outputValue >> 24);
streamBuffers[4] = (byte)(outputValue >> 32);
streamBuffers[5] = (byte)(outputValue >> 40);
streamBuffers[6] = (byte)(outputValue >> 48);
streamBuffers[7] = (byte)(outputValue >> 56);
}

BaseStream.Write(streamBuffers, 0, 8);
}

/** <summary> Writes the specified String into a BinaryStream. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteString(string targetStr, Encoding dataEncoding = default, Endian endianOrder = default)
{
CheckDataEncoding(ref dataEncoding);
CheckEndianOrder(ref endianOrder, true);

if(string.IsNullOrEmpty(targetStr) )
return;

byte[] outputBytes = dataEncoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endianOrder);

BaseStream.Write(outputBytes, 0, outputBytes.Length);
}

/** <summary> Writes the specified String within its specific Length into a BinaryStream. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "stringLength"> The Length of the String. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteString(string targetStr, int stringLength, Encoding dataEncoding = default, Endian endianOrder = default)
{
CheckDataEncoding(ref dataEncoding);
CheckEndianOrder(ref endianOrder, true);

byte[] fillingBuffer = new byte[stringLength];

if(string.IsNullOrEmpty(targetStr) )
{
BaseStream.Write(fillingBuffer, 0, stringLength);
return;
}

byte[] outputBytes = dataEncoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endianOrder);

if(outputBytes.Length >= stringLength)
Array.Copy(outputBytes, 0, fillingBuffer, 0, stringLength);

else
Array.Copy(outputBytes, 0, fillingBuffer, 0, fillingBuffer.Length);

BaseStream.Write(fillingBuffer, 0, stringLength);
}

/** <summary> Writes the specified String into a BinaryStream along with its Length as a 8-bits Integer. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteStringByByteLength(string targetStr, Encoding dataEncoding = default, Endian endianOrder = default)
{
CheckDataEncoding(ref dataEncoding);
CheckEndianOrder(ref endianOrder, true);

if(string.IsNullOrEmpty(targetStr) )
{
WriteByte(0x00);
return;
}

byte[] outputBytes = dataEncoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endianOrder);

int outputBytesCount = outputBytes.Length;
WriteByte( (byte)outputBytesCount);

BaseStream.Write(outputBytes, 0, outputBytesCount);
}

/** <summary> Writes the specified String into a BinaryStream along with its Length as an unsigned 16-bits Integer. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteStringByUShortLength(string targetStr, Encoding dataEncoding = default, Endian endianOrder = default)
{
CheckDataEncoding(ref dataEncoding);
CheckEndianOrder(ref endianOrder, true);

if(string.IsNullOrEmpty(targetStr) )
{
WriteUShort(0);
return;
}

byte[] outputBytes = dataEncoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endianOrder);

int outputBytesCount = outputBytes.Length;
WriteUShort( (ushort)outputBytesCount, endianOrder);

BaseStream.Write(outputBytes, 0, outputBytesCount);
}

/** <summary> Writes the specified String into a BinaryStream along with its Length as a 32-bits Integer. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteStringByIntLength(string targetStr, Encoding dataEncoding = default, Endian endianOrder = default)
{
CheckDataEncoding(ref dataEncoding);
CheckEndianOrder(ref endianOrder, true);

if(string.IsNullOrEmpty(targetStr) )
{
WriteInt(0);
return;
}

byte[] outputBytes = dataEncoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endianOrder);

int outputBytesCount = outputBytes.Length;
WriteInt(outputBytesCount, endianOrder);

BaseStream.Write(outputBytes, 0, outputBytesCount);
}

/** <summary> Writes the specified String into a BinaryStream along with its Length a 32-bits variant Integer. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteStringByVarIntLength(string targetStr, Encoding dataEncoding = default, Endian endianOrder = default)
{
CheckDataEncoding(ref dataEncoding);
CheckEndianOrder(ref endianOrder, true);

if(string.IsNullOrEmpty(targetStr) )
{
WriteVarInt(0);
return;
}

byte[] outputBytes = dataEncoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endianOrder);

int outputBytesCount = outputBytes.Length;
WriteVarInt(outputBytesCount);

BaseStream.Write(outputBytes, 0, outputBytesCount);
}

/** <summary> Writes the specified String into a BinaryStream until Zero (0x00 in Bytes) is Reached. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "dataEncoding"> The Data Encoding. </param>
<param name = "endianOrder"> The endian Order of the Data. </param> */

public void WriteStringUntilZero(string targetStr, Encoding dataEncoding = default, Endian endianOrder = default)
{
CheckDataEncoding(ref dataEncoding);
CheckEndianOrder(ref endianOrder, true);

if(string.IsNullOrEmpty(targetStr) )
{
WriteByte(0x00);
return;
}

byte[] outputBytes = dataEncoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endianOrder);

BaseStream.Write(outputBytes, 0, outputBytes.Length);
WriteByte(0x00);
}

public static implicit operator Stream(BinaryStream a) => a.BaseStream;
}