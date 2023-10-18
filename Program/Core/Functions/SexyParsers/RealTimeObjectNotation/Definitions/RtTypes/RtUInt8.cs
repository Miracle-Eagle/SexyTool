using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents an 8-bits unsigned Integer in the RtSystem. </summary>

public class RtUInt8 : RtStruct<byte>
{
/// <summary> Creates a new Instance of the <c>RtUInt8</c>. </summary>

public RtUInt8()
{
Identifier = RtTypeIdentifier.Byte_0;
}

/** <summary> Creates a new Instance of the <c>RtUInt8</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtUInt8(byte sourceValue)
{

if(sourceValue == 0)
Identifier = RtTypeIdentifier.Byte_0;

else
{
Identifier = RtTypeIdentifier.Byte;
Value = sourceValue;
}

}

/** <summary> Reads an 8-bits unsigned Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, ref RtTypeIdentifier sourceID)
{
byte uint8Value = (sourceID == RtTypeIdentifier.Byte) ? inputStream.ReadByte() : (byte)0;
outputStream.WriteNumberValue(uint8Value);
}

/** <summary> Reads an 8-bits unsigned Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The UInt8 to be Written. </param> */

public static void Write(BinaryStream outputStream, byte targetValue)
{

if(targetValue == 0)
outputStream.WriteByte( (byte)RtTypeIdentifier.Byte_0);

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.Byte);
outputStream.WriteByte(targetValue);
}

}

}

}