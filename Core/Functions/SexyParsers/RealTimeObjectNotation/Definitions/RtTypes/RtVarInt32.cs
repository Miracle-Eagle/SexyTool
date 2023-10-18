using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 32-bits variant Integer in the RtSystem. </summary>

public class RtVarInt32 : RtStruct<int>
{
/// <summary> Creates a new Instance of the <c>RtVarInt32</c>. </summary>

public RtVarInt32()
{
Identifier = RtTypeIdentifier.VarInt;
}

/** <summary> Creates a new Instance of the <c>RtVarInt32</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtVarInt32(int sourceValue)
{
Identifier = RtTypeIdentifier.VarInt;
Value = sourceValue;
}

/** <summary> Reads a 32-bits variant Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream)
{
int varInt32Value = inputStream.ReadVarInt();
outputStream.WriteNumberValue(varInt32Value);
}

/** <summary> Reads a 32-bits variant Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The VarInt32 to be Written. </param> */

public static void Write(BinaryStream outputStream, int targetValue)
{
outputStream.WriteByte( (byte)RtTypeIdentifier.VarInt);
outputStream.WriteVarInt(targetValue);
}

}

}