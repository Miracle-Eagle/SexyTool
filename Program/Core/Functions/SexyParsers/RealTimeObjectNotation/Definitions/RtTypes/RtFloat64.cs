using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 64-bits Float-point in the RtSystem. </summary>

public class RtFloat64 : RtStruct<double>
{
/// <summary> Creates a new Instance of the <c>RtFloat64</c>. </summary>

public RtFloat64()
{
Identifier = RtTypeIdentifier.Double_0;
}

/** <summary> Creates a new Instance of the <c>RtFloat64</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtFloat64(double sourceValue)
{

if(sourceValue == 0.0d)
Identifier = RtTypeIdentifier.Double_0;

else
{
Identifier = RtTypeIdentifier.Double;
Value = sourceValue;
}

}

/** <summary> Evaluates the Type of a RTON Double and writes the Value to a JSON File according to its Type. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "sourceStr"> The Double to Evaluate. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

private static void EvaluateValue(BinaryStream outputStream, double sourceValue, Endian endianOrder)
{

if(sourceValue == 0.0d)
outputStream.WriteByte( (byte)RtTypeIdentifier.Double_0);

else if(sourceValue == double.Epsilon)
RtUnicodeString.Write(outputStream, "\u03B5", endianOrder);

else if(sourceValue == double.E)
RtUnicodeString.Write(outputStream, "\u0065", endianOrder);

else if(sourceValue == double.Pi)
RtUnicodeString.Write(outputStream, "\u03C0", endianOrder);

else if(sourceValue == double.Tau)
RtUnicodeString.Write(outputStream, "\u03C4", endianOrder);

else if(sourceValue == double.NegativeInfinity)
RtNativeString.Write(outputStream, "-Infinity", endianOrder);

else if(sourceValue == double.PositiveInfinity)
RtNativeString.Write(outputStream, "Infinity", endianOrder);

else if(sourceValue == double.NaN)
RtNativeString.Write(outputStream, "NaN", endianOrder);

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.Double);
outputStream.WriteDouble(sourceValue, endianOrder);
}

}

/** <summary> Reads a 64-bits Float-point from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, ref RtTypeIdentifier sourceID, Endian endianOrder = default)
{
double float64Value = (sourceID == RtTypeIdentifier.Double) ? inputStream.ReadDouble(endianOrder) : 0.0d;
outputStream.WriteNumberValue(float64Value);
}

/** <summary> Reads a 64-bits Float-point from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The Float64 to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, double targetValue, Endian endianOrder = default) => EvaluateValue(outputStream, targetValue, endianOrder);
}

}