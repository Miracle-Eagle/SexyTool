using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 64-bits variant Integer in the RtSystem. </summary>

public class RtVarInt64 : RtStruct<long>
{
/// <summary> Creates a new Instance of the <c>RtVarInt64</c>. </summary>

public RtVarInt64()
{
Identifier = RtTypeIdentifier.VarLong;
}

/** <summary> Creates a new Instance of the <c>RtVarInt64</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtVarInt64(long sourceValue)
{
Identifier = RtTypeIdentifier.VarLong;
Value = sourceValue;
}

/** <summary> Reads a 64-bits variant Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream)
{
long varInt64Value = inputStream.ReadVarLong();
outputStream.WriteNumberValue(varInt64Value);
}

/** <summary> Reads a 64-bits variant Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "inputStream"> The Stream which Contains the JSON Data to be Read. </param>
<param name = "outputStream"> The Stream where the RTON Data will be Written. </param> */

public static void Write(Utf8JsonReader inputStream, BinaryStream outputStream)
{
outputStream.WriteByte( (byte)RtTypeIdentifier.VarLong);
long varInt64Value = inputStream.GetInt64();

outputStream.WriteVarLong(varInt64Value);
}

}

}