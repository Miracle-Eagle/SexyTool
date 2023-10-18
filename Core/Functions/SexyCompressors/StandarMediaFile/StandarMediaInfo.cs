using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;

namespace SexyTool.Program.Core.Functions.SexyCompressors.StandarMediaFile
{
/// <summary> Initializes analisis Functions about the Info Contained on a Standar Media File (SMF). </summary>

public struct StandarMediaInfo
{
/** <summary> Sets a Value which Contains Info about the Number of Bytes for each Field of this Class. </summary>
<returns> The Field Size. </returns> */

private const long fieldSize = 4;

/** <summary> Sets a Value which Contains Info about the Size of a Standar Media File (SMF) before Compression. </summary>
<returns> The Size of the File before Compression expressed in Bytes. </returns> */

public byte[] SizeBeforeCompression{ get; set; }

/** <summary> Sets a Value which Contains Info about the CompressionLevel used for Compressing a Standar Media File (SMF). </summary>
<returns> The Compression Flags. </returns> */

public byte[] CompressionFlags{ get; set; }

/** <summary> Sets a Value which Contains Info about the Adler32 Bytes of a Standar Media File (SMF). </summary>
<returns> The Adler32 Bytes. </returns> */

public byte[] Adler32Bytes{ get; set; }

/// <summary> Creates a new Instance of the <c>StandarMediaInfo</c>. </summary>

public StandarMediaInfo()
{
SizeBeforeCompression = new byte[fieldSize];
CompressionFlags = new byte[fieldSize];

Adler32Bytes = new byte[fieldSize];
}

/** <summary> Creates a new Instance of the <c>StandarMediaInfo</c> with the specific SizeInfo. </summary>
<param name = "SizeInfo"> The Bytes this Instance should hold as a 'SizeBeforeCompression' Field. </param> */

public StandarMediaInfo(byte[] SizeBeforeCompression)
{
this.SizeBeforeCompression = SizeBeforeCompression;
}

/** <summary> Creates a new Instance of the <c>StandarMediaInfo</c> with the specific SizeInfo and CompressionFlags. </summary>

<param name = "SizeInfo"> The Bytes this Instance should hold as a 'SizeBeforeCompression' Field. </param>
<param name = "CompressionFlags"> The Bytes this Instance should hold as a 'CompressionFlags' Field. </param> */

public StandarMediaInfo(byte[] SizeBeforeCompression, byte[] CompressionFlags)
{
this.SizeBeforeCompression = SizeBeforeCompression;
this.CompressionFlags = CompressionFlags;
}

/** <summary> Creates a new Instance of the <c>StandarMediaInfo</c> with the specific SizeInfo, CompressionFlags and Adler32Bytes. </summary>

<param name = "SizeBeforeCompression"> The Bytes this Instance should hold as a 'SizeBeforeCompression' Field. </param>
<param name = "CompressionFlags"> The Bytes this Instance should hold as a 'CompressionFlags' Field. </param>
<param name = "Adler32Bytes"> The Bytes this Instance should hold as a 'Adler32Bytes' Field. </param> */

public StandarMediaInfo(byte[] SizeBeforeCompression, byte[] CompressionFlags, byte[] Adler32Bytes)
{
this.SizeBeforeCompression = SizeBeforeCompression;
this.CompressionFlags = CompressionFlags;

this.Adler32Bytes = Adler32Bytes;
}

/** <summary> Creates a new Instance of the <c>StandarMediaInfo</c> with the given Parameters. </summary>

<param name = "targetStream"> The Stream to be Read. </param>
<param name = "compressionLvl"> The CompressionLevel to be Used. </param> */

public StandarMediaInfo(Stream targetStream, CompressionLevel compressionLvl)
{
SizeBeforeCompression = GetSizeInfo(targetStream, 4);
CompressionFlags = GetCompressionFlags(ref compressionLvl);

Adler32Bytes = GetAdler32Bytes(targetStream);
}

/** <summary> Gets a Bytes Array from an Hexadecimal String and Flips its Bytes after Splitting. </summary>

<param name = "targetStream"> The Stream which Contains the Bytes to be Analized. </param>

<returns> The Hexadecimal Bytes Obtained. </returns> */

private static byte[] GetHexBytes(Stream targetStream)
{
string hexString = targetStream.Length.ToString("x2");

if(hexString.Length % 2 != 0)
hexString = "0" + hexString;

byte[] hexBytes = new byte[hexString.Length / 2];

for(int i = 0; i < hexBytes.Length; i++)
{
string hexPair = hexString.Substring(i * 2, 2);
hexBytes[i] = byte.Parse(hexPair, NumberStyles.HexNumber);
}

Array.Reverse(hexBytes);
return hexBytes;
}

/** <summary> Gets the Size of a Stream as an Hexadecimal Bytes Array. </summary>

<param name = "targetStream"> The Stream to be Analized. </param>
<param name = "bytesCount"> The Number of Bytes to be Analized. </param>

<returns> The Size Info. </returns> */

public static byte[] GetSizeInfo(Stream targetStream, int bytesCount) 
{
byte[] hexBytes = GetHexBytes(targetStream);
byte[] sizeInfo = new byte[bytesCount];

if(hexBytes.Length > sizeInfo.Length)
throw new DataMisalignedException("The Number of Hexadecimal Bytes is Higher than the Size of your File.");

for(int i = 0; i < hexBytes.Length; i++)
sizeInfo[i] = hexBytes[i];

return sizeInfo;
}

/** <summary> Gets the Compression Info of a SMF File based on the CompressionLevel used. </summary>

<param name = "compressionLvl"> The CompressionLevel used. </param>

<returns> The Compression Flags. </returns> */

public static byte[] GetCompressionFlags(ref CompressionLevel compressionLvl)
{

ushort compressionFlags = compressionLvl switch
{
CompressionLevel.Fastest => 40056,
CompressionLevel.Optimal => 55928,
_ => 376
};

return BitConverter.GetBytes(compressionFlags);
}

/** <summary> Gets the Checksum of an Array of Bytes by using the Adler32 Algorithm. </summary>

<param name = "inputBytes"> The Bytes where the Checksum will be Obtained from. </param>
<param name = "bytesCount"> The Number of Bytes to be Analized. </param>
<param name = "bytesOffset"> The Offset of the Data stored in the Bytes Array (Default is 0). </param>

<returns> The Checksum of the Bytes. </returns> */

private static uint CalculateChecksum(byte[] inputBytes, int bytesCount, int bytesOffset = 0)
{
#region ====== Constants for the Adler32 Algorithm ======

const int BITS_TO_SHIFT = 16;
const int BYTES_PER_ITERATION = 3800;
const uint MOD_ADLER = 65521;

#endregion

uint checksumValue = 1;
uint sumX = checksumValue & 0xFFFF;

uint sumY = checksumValue >> BITS_TO_SHIFT;
int bytesChecked;

while(bytesCount > 0)
{
bytesChecked = (BYTES_PER_ITERATION > bytesCount) ? bytesCount : BYTES_PER_ITERATION;
bytesCount -= bytesChecked;

while(--bytesChecked >= 0)
{
sumX += (uint)inputBytes[bytesOffset++] & 0xFF;
sumY += sumX;
}

sumX %= MOD_ADLER;
sumY %= MOD_ADLER;
}

checksumValue = (sumY << BITS_TO_SHIFT) | sumX;
return checksumValue;
}

/** <summary> Gets the Adler32 Bytes from a Stream. </summary>

<param name = "targetStream"> The Stream which Contains the Adler32 Bytes to be Analized. </param>

<returns> The Adler32 Bytes Obtained. </returns> */

public static byte[] GetAdler32Bytes(Stream targetStream)
{
byte[] inputBytes = new byte[targetStream.Length];
targetStream.Read(inputBytes);

targetStream.Seek(0, SeekOrigin.Begin);
uint checksumValue = CalculateChecksum(inputBytes, inputBytes.Length);

string hexString = checksumValue.ToString("x2");
int expectedLength = 8;

if(hexString.Length <expectedLength)
Input_Manager.FillString(ref hexString, expectedLength);

else if(hexString.Length > expectedLength)
throw new DataMisalignedException("The Number of Adler32 Bytes is Higher than the Number of Hexadecimal Bytes on your File.");

byte[] adler32Bytes = new byte[expectedLength / 2];

for(int i = 0; i < adler32Bytes.Length; i++)
{
string hexPair = hexString.Substring(i * 2, 2);
adler32Bytes[i] = Convert.ToByte(hexPair, 16);
}

return adler32Bytes;
}

}

}