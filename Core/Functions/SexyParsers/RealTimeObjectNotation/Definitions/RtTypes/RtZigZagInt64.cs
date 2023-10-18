using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 64-bits variant Integer that was Encoded with ZigZag in the RtSystem. </summary>

public class RtZigZagInt64 : RtStruct<long>
{
/// <summary> Creates a new Instance of the <c>RtZigZagInt64</c>. </summary>

public RtZigZagInt64()
{
Identifier = RtTypeIdentifier.ZigZagLong;
}

/** <summary> Creates a new Instance of the <c>RtZigZagInt64</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtZigZagInt64(long sourceValue)
{
Identifier = RtTypeIdentifier.ZigZagLong;
Value = sourceValue;
}

/** <summary> Reads a 64-bits ZigZag Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream)
{
long zigZagInt64Value = inputStream.ReadZigZagLong();
outputStream.WriteNumberValue(zigZagInt64Value);
}

/** <summary> Reads a 64-bits ZigZag Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "inputStream"> The Stream which Contains the JSON Data to be Read. </param>
<param name = "outputStream"> The Stream where the RTON Data will be Written. </param> */

public static void Write(Utf8JsonReader inputStream, BinaryStream outputStream)
{
outputStream.WriteByte( (byte)RtTypeIdentifier.ZigZagLong);
long zigZagInt64Value = inputStream.GetInt64();

outputStream.WriteZigZagLong(zigZagInt64Value);
}

}

}