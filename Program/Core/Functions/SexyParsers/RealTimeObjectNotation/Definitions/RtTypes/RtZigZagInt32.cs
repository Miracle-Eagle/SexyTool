using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 32-bits variant Integer that was Encoded with ZigZag in the RtSystem. </summary>

public class RtZigZagInt32 : RtStruct<int>
{
/// <summary> Creates a new Instance of the <c>RtZigZagInt32</c>. </summary>

public RtZigZagInt32()
{
Identifier = RtTypeIdentifier.ZigZagInt;
}

/** <summary> Creates a new Instance of the <c>RtZigZagInt32</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtZigZagInt32(int sourceValue)
{
Identifier = RtTypeIdentifier.ZigZagInt;
Value = sourceValue;
}

/** <summary> Reads a 32-bits ZigZag Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream)
{
int zigZagInt32Value = inputStream.ReadZigZagInt();
outputStream.WriteNumberValue(zigZagInt32Value);
}

/** <summary> Reads a 32-bits ZigZag Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The ZigZagInt32 to be Written. </param> */

public static void Write(BinaryStream outputStream, int targetValue)
{
outputStream.WriteByte( (byte)RtTypeIdentifier.ZigZagInt);
outputStream.WriteZigZagInt(targetValue);
}

}

}