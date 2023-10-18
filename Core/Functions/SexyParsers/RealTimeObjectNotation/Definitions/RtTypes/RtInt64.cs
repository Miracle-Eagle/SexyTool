using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 64-bits signed Integer in the RtSystem. </summary>

public class RtInt64 : RtStruct<long>
{
/// <summary> Creates a new Instance of the <c>RtInt64</c>. </summary>

public RtInt64()
{
Identifier = RtTypeIdentifier.Long_0;
}

/** <summary> Creates a new Instance of the <c>RtInt64</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtInt64(long sourceValue)
{

if(sourceValue == 0L)
Identifier = RtTypeIdentifier.Long_0;

else
{
Identifier = RtTypeIdentifier.Long;
Value = sourceValue;
}

}

/** <summary> Reads a 64-bits signed Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, ref RtTypeIdentifier sourceID, Endian endianOrder = default)
{
long int64Value = (sourceID == RtTypeIdentifier.Long) ? inputStream.ReadLong(endianOrder) : 0L;
outputStream.WriteNumberValue(int64Value);
}

/** <summary> Reads a 64-bits signed Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The Int64 to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, long targetValue, Endian endianOrder = default)
{

if(targetValue == 0L)
outputStream.WriteByte( (byte)RtTypeIdentifier.Long_0);

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.Long);
outputStream.WriteLong(targetValue, endianOrder);
}

}

}

}