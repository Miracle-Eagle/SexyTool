using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes;
using System.Text;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base
{
/// <summary> Represents a String in the RtSystem. </summary>

public abstract class RtString : RtBaseType<string>
{
/** <summary> Gets or Sets a Value which Contains Info about the Length of the String in the RtSystem. </summary>
<returns> The Length of the String. </returns> */

public int Length{ get; set; }

/** <summary> Gets or Sets a Value which Contains Info about the Length of a String after Encoding. </summary>
<returns> The Length of the String in Bytes. </returns> */

public int LengthAfterEncoding{ get; set; }

/** <summary> Checks if the Providen String is NativeString or not. </summary>

<param name = "targetStr"> The String to be Analized. </param>

<returns> <b>true</b> if the String is a NativeString (UTF-8 on Linux and UTF-16 on Windows); otherwise, returns <b>false</b> </returns> */

private static bool IsNativeString(string targetStr)
{
Encoding nativeEncoding = Encoding.Default;
byte[] targetBytes = nativeEncoding.GetBytes(targetStr);

string decodedString = nativeEncoding.GetString(targetBytes);
return targetStr.Equals(decodedString);
}

/** <summary> Evaluates the Type of a JSON String and writes the String to a RTON File according to its Type. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "sourceStr"> The JSON String to Evaluate. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void EvaluateStringType(BinaryStream outputStream, string sourceStr, Endian endianOrder = default)
{
var binaryStrMatch = RtBinaryString.PerformRegex(sourceStr);
var rtIDStrMatch = RtIDString.PerformRegex(sourceStr);

if(sourceStr == null)
outputStream.WriteByte( (byte)RtTypeIdentifier.Null);

else if(sourceStr == "\u0065")
RtFloat32.Write(outputStream, float.E, endianOrder);

else if(sourceStr == "\u03B5")
RtFloat32.Write(outputStream, float.Epsilon, endianOrder);

else if(sourceStr == "\u03C0")
RtFloat32.Write(outputStream, float.Pi, endianOrder);

else if(sourceStr == "\u03C4")
RtFloat32.Write(outputStream, float.Tau, endianOrder);

else if(sourceStr == "-Infinity")
RtFloat32.Write(outputStream, float.NegativeInfinity, endianOrder);

else if(sourceStr == "Infinity")
RtFloat32.Write(outputStream, float.PositiveInfinity, endianOrder);

else if(sourceStr == "NaN")
RtFloat32.Write(outputStream, float.NaN, endianOrder);

else if(IsNativeString(sourceStr) )
RtNativeString.Write(outputStream, sourceStr, endianOrder);

else if(rtIDStrMatch.Success)
RtIDString.Write(outputStream, rtIDStrMatch);

else if(binaryStrMatch.Success)
RtBinaryString.Write(outputStream, binaryStrMatch);

else
RtUnicodeString.Write(outputStream, sourceStr, endianOrder);

}

/** <summary> Gets a Message used for alerting the User about a String Length Mismatch. </summary>

<param name = "stringType"> The Type of String. </param>
<param name = "stringValue"> The String that was Red. </param>
<param name = "stringLength"> The Length of the String (in Characters). </param>
<param name = "stringLengthAfterEncoding"> The Length of the String after Encoding (in Bytes). </param>
<param name = "currentPos"> The Current Position of the File. </param>

<returns> The Mismatch Message. </returns> */

protected static string GetLengthMismatchMsg(RtTypeIdentifier stringType, string stringValue, int stringLength, int stringLengthAfterEncoding, long currentPos)
{
string msgFormat = "The {0} read (\"{(1}\" Type) exceeds the Length expected in Bytes.\nThe {0} that was Read: \"{2}\" (Length: {3} chars - Expected Length: {4} bytes).\n\nValue found at Address: {5:x8}";
return string.Format(msgFormat, stringType, (byte)stringType, stringValue, stringLength, stringLengthAfterEncoding, currentPos);
}

/** <summary> Gets a Message used for alerting the User about a RtID String Length Mismatch. </summary>

<param name = "stringType"> The Type of the String. </param>
<param name = "rtIDFlags"> The Type of the RtID String. </param>
<param name = "stringValue"> The String that was Red. </param>
<param name = "stringLength"> The Length of the String (in Characters). </param>
<param name = "stringLengthAfterEncoding"> The Length of the String after Encoding (in Bytes). </param>
<param name = "currentPos"> The Current Position of the File. </param>

<returns> The RtID Mismatch Message. </returns> */

protected static string GetRtIDLengthMismatchMsg(RtIDType rtIDFlags, string stringValue, int stringLength, int stringLengthAfterEncoding, long currentPos)
{
string msgFormat = "The {2}{0} read (\"{1}\" Type - \"{3}\" SubType) exceeds the Length expected in Bytes.\nThe {2}{0} that was Read: \"{4}\" (Length: {5} chars - Expected Length: {6} bytes).\n\nValue found at Address: {7:x8}";
RtTypeIdentifier stringType = RtTypeIdentifier.IDString;

return string.Format(msgFormat, stringType, (byte)stringType, rtIDFlags, (byte)rtIDFlags, stringValue, stringLength, stringLengthAfterEncoding, currentPos);
}

/** <summary> Writes a String to a JSON File with the Specified Options. </summary>

<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "targetStr"> The String to be Written. </param>
<param name = "isPropertyName"> Determines if the JsonString should be Written as a PropertyName or not. </param> */

protected static void WriteJsonString(Utf8JsonWriter outputStream, string targetStr, bool isPropertyName)
{

if(isPropertyName)
outputStream.WritePropertyName(targetStr);

else
outputStream.WriteStringValue(targetStr);

}

}

}