using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents an 8-bits signed Integer in the RtSystem. </summary>

public class RtInt8 : RtStruct<sbyte>
{
/// <summary> Creates a new Instance of the <c>RtInt8</c>. </summary>

public RtInt8()
{
Identifier = RtTypeIdentifier.SByte_0;
Value = default;
}

/** <summary> Creates a new Instance of the <c>RtInt8</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtInt8(sbyte sourceValue)
{

if(sourceValue == 0)
Identifier = RtTypeIdentifier.SByte_0;

else
{
Identifier = RtTypeIdentifier.SByte;
Value = sourceValue;
}

}

/** <summary> Reads an 8-bits signed Interger from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, ref RtTypeIdentifier sourceID)
{
sbyte int8Value = (sourceID == RtTypeIdentifier.SByte) ? inputStream.ReadSByte() : (sbyte)0;
outputStream.WriteNumberValue(int8Value);
}

/** <summary> Reads an 8-bits signed from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The Int8 to be Written. </param> */

public static void Write(BinaryStream outputStream, sbyte targetValue)
{

if(targetValue == 0)
outputStream.WriteByte( (byte)RtTypeIdentifier.SByte_0);

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.SByte);
outputStream.WriteSByte(targetValue);
}

}

}

}