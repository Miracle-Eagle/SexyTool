using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 32-bits Float-point in the RtSystem. </summary>

public class RtFloat32 : RtStruct<float>
{
/// <summary> Creates a new Instance of the <c>RtFloat32</c>. </summary>

public RtFloat32()
{
Identifier = RtTypeIdentifier.Float_0;
}

/** <summary> Creates a new Instance of the <c>RtFloat32</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtFloat32(float sourceValue)
{

if(sourceValue == 0.0f)
Identifier = RtTypeIdentifier.Float_0;

else
{
Identifier = RtTypeIdentifier.Float;
Value = sourceValue;
}

}

/** <summary> Evaluates the Type of a RTON Float and writes the Value to a JSON File according to its Type. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "sourceStr"> The Float to Evaluate. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

private static void EvaluateValue(BinaryStream outputStream, float sourceValue, Endian endianOrder)
{

if(sourceValue == 0.0f)
outputStream.WriteByte( (byte)RtTypeIdentifier.Float_0);

else if(sourceValue == float.Epsilon)
RtUnicodeString.Write(outputStream, "\u03B5", endianOrder);

else if(sourceValue == float.E)
RtUnicodeString.Write(outputStream, "\u0065", endianOrder);

else if(sourceValue == float.Pi)
RtUnicodeString.Write(outputStream, "\u03C0", endianOrder);

else if(sourceValue == float.Tau)
RtUnicodeString.Write(outputStream, "\u03C4", endianOrder);

else if(sourceValue == float.NegativeInfinity)
RtNativeString.Write(outputStream, "-Infinity", endianOrder);

else if(sourceValue == float.PositiveInfinity)
RtNativeString.Write(outputStream, "Infinity", endianOrder);

else if(sourceValue == float.NaN)
RtNativeString.Write(outputStream, "NaN", endianOrder);

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.Float);
outputStream.WriteFloat(sourceValue, endianOrder);
}

}

/** <summary> Reads a 32-bits Float-point from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, ref RtTypeIdentifier sourceID, Endian endianOrder = default)
{
float float32Value = (sourceID == RtTypeIdentifier.Float) ? inputStream.ReadFloat(endianOrder) : 0.0f;
outputStream.WriteNumberValue(float32Value);
}

/** <summary> Reads a 32-bits Float-point from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The Float32 to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, float targetValue, Endian endianOrder = default) => EvaluateValue(outputStream, targetValue, endianOrder);
}

}