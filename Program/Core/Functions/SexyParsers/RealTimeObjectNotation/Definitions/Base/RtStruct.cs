using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base
{
/// <summary> Represents a Data Structure in the RtSystem. </summary>

public abstract class RtStruct<T> : RtBaseType<T> where T : struct
{
/** <summary> Checks if a Number is a Decimal or mot. </summary>

<param name = "sourceData"> The Data to be Analized. </param>

<returns> <b>true</b> if the Number is a Decimal Value; otherwise, returns <b>false</b> </returns> */

private static bool NumberIsDecimal(JsonElement sourceData) => sourceData.GetRawText().IndexOf('.') > -1;

/** <summary> Checks if a Value is a Single-Precision or a Double-Precision Point and writes the Value to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The Value to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

protected static void CheckFloatPoint(BinaryStream outputStream, double targetValue, Endian endianOrder = default)
{

if(targetValue <= float.MaxValue)
RtFloat32.Write(outputStream, (float)targetValue, endianOrder);

else
RtFloat64.Write(outputStream, targetValue, endianOrder);

}

/** <summary> Checks the Type of a Integer (either Signed or Unsigned) and writes the Value to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The Value to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

protected static void CheckUnsignedInteger(BinaryStream outputStream, double targetValue, Endian endianOrder = default)
{

if(targetValue >= 16384 && targetValue <= short.MaxValue)
RtInt16.Write(outputStream, (short)targetValue, endianOrder);

else if(targetValue > short.MaxValue && targetValue <= ushort.MaxValue)
RtUInt16.Write(outputStream, (ushort)targetValue, endianOrder);

else if(targetValue > ushort.MaxValue && targetValue <= int.MaxValue)
RtInt32.Write(outputStream, (int)targetValue, endianOrder);

else if(targetValue > int.MaxValue && targetValue <= uint.MaxValue)
RtUInt32.Write(outputStream, (uint)targetValue, endianOrder);

else if(targetValue > uint.MaxValue && targetValue <= long.MaxValue)
RtInt64.Write(outputStream, (long)targetValue, endianOrder);

else if(targetValue > long.MaxValue && targetValue <= ulong.MaxValue)
RtUInt64.Write(outputStream, (ulong)targetValue, endianOrder);

else
RtVarInt32.Write(outputStream, (int)targetValue);

}

/** <summary> Checks if the Type of a signed Integer and writes the Value to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The Value to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

protected static void CheckSignedInteger(BinaryStream outputStream, double targetValue, Endian endianOrder = default)
{

if(targetValue < short.MinValue && targetValue >= short.MinValue)
RtInt16.Write(outputStream, (short)targetValue, endianOrder);

else if(targetValue < short.MinValue && targetValue >= int.MinValue)
RtInt32.Write(outputStream, (int)targetValue, endianOrder);

else if(targetValue < int.MinValue && targetValue >= long.MinValue)
RtInt64.Write(outputStream, (long)targetValue, endianOrder);

else
RtZigZagInt32.Write(outputStream, (int)targetValue);

}

/** <summary> Evaluates the Type of a JSON Number and writes the Value to a RTON File according to its Type. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "sourceData"> The JSON Data that contains Value to Evaluate. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void EvaluateNumericValue(BinaryStream outputStream, JsonElement sourceData, Endian endianOrder = default)
{
double inputValue = sourceData.GetDouble();

if(NumberIsDecimal(sourceData) )
CheckFloatPoint(outputStream, inputValue, endianOrder);

else if(inputValue <= ulong.MaxValue)
CheckUnsignedInteger(outputStream, inputValue, endianOrder);

else
CheckSignedInteger(outputStream, inputValue, endianOrder);

}

}

}