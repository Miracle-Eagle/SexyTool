using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 16-bits signed Integer in the RtSystem. </summary>

public class RtInt16 : RtStruct<short>
{
/// <summary> Creates a new Instance of the <c>RtInt16</c>. </summary>

public RtInt16()
{
Identifier = RtTypeIdentifier.Short_0;
}

/** <summary> Creates a new Instance of the <c>RtInt16</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtInt16(short sourceValue)
{

if(sourceValue == 0)
Identifier = RtTypeIdentifier.Short_0;

else
{
Identifier = RtTypeIdentifier.Short;
Value = sourceValue;
}

}

/** <summary> Reads a 16-bits signed Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, ref RtTypeIdentifier sourceID, Endian endianOrder = default)
{
short int16Value = (sourceID == RtTypeIdentifier.Short) ? inputStream.ReadShort(endianOrder) : (short)0;
outputStream.WriteNumberValue(int16Value);
}

/** <summary> Reads a 16-bits signed Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "inputStream"> The Stream which Contains the JSON Data to be Read. </param>
<param name = "targetValue"> The Int16 to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, short targetValue, Endian endianOrder = default)
{

if(targetValue == 0)
outputStream.WriteByte( (byte)RtTypeIdentifier.Short_0);

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.Short);
outputStream.WriteShort(targetValue, endianOrder);
}

}

}

}