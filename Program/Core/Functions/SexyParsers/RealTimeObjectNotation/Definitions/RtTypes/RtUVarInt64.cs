using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents an unsigned 64-bits variant Integer in the RtSystem. </summary>

public class RtUVarInt64 : RtStruct<ulong>
{
/// <summary> Creates a new Instance of the <c>RtUVarInt64</c>. </summary>

public RtUVarInt64()
{
Identifier = RtTypeIdentifier.UVarLong;
}

/** <summary> Creates a new Instance of the <c>RtUVarInt64</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtUVarInt64(ulong sourceValue)
{
Identifier = RtTypeIdentifier.UVarLong;
Value = sourceValue;
}

/** <summary> Reads an unsigned 64-bits variant Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream)
{
ulong uVarInt64Value = inputStream.ReadUVarLong();
outputStream.WriteNumberValue(uVarInt64Value);
}

/** <summary> Reads an unsigned 64-bits variant Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "inputStream"> The Stream which Contains the JSON Data to be Read. </param>
<param name = "outputStream"> The Stream where the RTON Data will be Written. </param> */

public static void Write(Utf8JsonReader inputStream, BinaryStream outputStream)
{
outputStream.WriteByte( (byte)RtTypeIdentifier.UVarLong);
ulong uVarInt64Value = inputStream.GetUInt64();

outputStream.WriteUVarLong(uVarInt64Value);
}

}

}