using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 16-bits unsigned Integer in the RtSystem. </summary>

public class RtUInt16 : RtStruct<ushort>
{
/// <summary> Creates a new Instance of the <c>RtUInt16</c>. </summary>

public RtUInt16()
{
Identifier = RtTypeIdentifier.UShort_0;
}

/** <summary> Creates a new Instance of the <c>RtUInt16</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtUInt16(ushort sourceValue)
{

if(sourceValue == 0)
Identifier = RtTypeIdentifier.UShort_0;

else
{
Identifier = RtTypeIdentifier.UShort;
Value = sourceValue;
}

}

/** <summary> Reads an unsigned 16-bits Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, ref RtTypeIdentifier sourceID, Endian endianOrder = default)
{
ushort uint16Value = (sourceID == RtTypeIdentifier.UShort) ? inputStream.ReadUShort(endianOrder) : (ushort)0;
outputStream.WriteNumberValue(uint16Value);
}

/** <summary> Reads a 16-bits from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "inputStream"> The Stream which Contains the JSON Data to be Read. </param>
<param name = "targetValue"> The Int16 to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, ushort targetValue, Endian endianOrder = default)
{

if(targetValue == 0)
outputStream.WriteByte( (byte)RtTypeIdentifier.UShort_0);

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.UShort);
outputStream.WriteUShort(targetValue, endianOrder);
}

}

}

}