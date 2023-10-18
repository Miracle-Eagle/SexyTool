using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents an unsigned 32-bits variant Integer in the RtSystem. </summary>

public class RtUVarInt32 : RtStruct<uint>
{
/// <summary> Creates a new Instance of the <c>RtUVarInt32</c>. </summary>

public RtUVarInt32()
{
Identifier = RtTypeIdentifier.UVarInt;
}

/** <summary> Creates a new Instance of the <c>RtUVarInt32</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtUVarInt32(uint sourceValue)
{
Identifier = RtTypeIdentifier.UVarInt;
Value = sourceValue;
}

/** <summary> Reads an unsigned 32-bits variant Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream)
{
uint uVarInt32Value = inputStream.ReadUVarInt();
outputStream.WriteNumberValue(uVarInt32Value);
}

/** <summary> Reads an unsigned 32-bits variant Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "inputStream"> The Stream which Contains the JSON Data to be Read. </param>
<param name = "outputStream"> The Stream where the RTON Data will be Written. </param> */

public static void Write(Utf8JsonReader inputStream, BinaryStream outputStream)
{
outputStream.WriteByte( (byte)RtTypeIdentifier.UVarInt);
uint uVarInt32Value = inputStream.GetUInt32();

outputStream.WriteUVarInt(uVarInt32Value);
}

}

}