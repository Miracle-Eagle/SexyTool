using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 32-bits unsigned Integer in the RtSystem. </summary>

public class RtUInt32 : RtStruct<uint>
{
/// <summary> Creates a new Instance of the <c>RtUInt32</c>. </summary>

public RtUInt32()
{
Identifier = RtTypeIdentifier.UInt_0;
}

/** <summary> Creates a new Instance of the <c>RtUInt32</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtUInt32(uint sourceValue)
{

if(sourceValue == 0u)
Identifier = RtTypeIdentifier.UInt_0;

else
{
Identifier = RtTypeIdentifier.UInt;
Value = sourceValue;
}

}

/** <summary> Reads a 32-bits unsigned Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, ref RtTypeIdentifier sourceID, Endian endianOrder = default)
{
uint uint32Value = (sourceID == RtTypeIdentifier.UInt) ? inputStream.ReadUInt(endianOrder) : 0u;
outputStream.WriteNumberValue(uint32Value);
}

/** <summary> Reads a 32-bits unsigned Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The UInt32 to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, uint targetValue, Endian endianOrder = default)
{

if(targetValue == 0u)
outputStream.WriteByte( (byte)RtTypeIdentifier.UInt_0);

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.UInt);
outputStream.WriteUInt(targetValue, endianOrder);
}

}

}

}