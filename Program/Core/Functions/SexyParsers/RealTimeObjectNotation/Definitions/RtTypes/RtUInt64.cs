using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 64-bits unsigned Integer in the RtSystem. </summary>

public class RtUInt64 : RtStruct<ulong>
{
/// <summary> Creates a new Instance of the <c>RtUInt64</c>. </summary>

public RtUInt64()
{
Identifier = RtTypeIdentifier.ULong_0;
}

/** <summary> Creates a new Instance of the <c>RtUInt64</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtUInt64(ulong sourceValue)
{

if(sourceValue == 0uL)
Identifier = RtTypeIdentifier.ULong_0;

else
{
Identifier = RtTypeIdentifier.ULong;
Value = sourceValue;
}

}

/** <summary> Reads a 64-bits unsigned Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, ref RtTypeIdentifier sourceID, Endian endianOrder = default)
{
ulong uint64Value = (sourceID == RtTypeIdentifier.ULong) ? inputStream.ReadULong(endianOrder) : 0uL;
outputStream.WriteNumberValue(uint64Value);
}

/** <summary> Reads a 64-bits unsigned Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The UInt64 to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, ulong targetValue, Endian endianOrder = default)
{

if(targetValue == 0uL)
outputStream.WriteByte( (byte)RtTypeIdentifier.ULong_0);

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.ULong);
outputStream.WriteULong(targetValue, endianOrder);
}

}

}

}