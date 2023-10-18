using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a Binary String in the RtSystem. </summary>

public class RtBinaryString : RtString
{
/** <summary> Store Info about the Encoding of a Binary String. </summary>
<returns> The Binary String Encoding. </returns> */

private static readonly Encoding binaryStrEncoding = Encoding.Latin1;

/** <summary> Store Info about the Format a Binary String should Follow. </summary>
<returns> The Binary String Format. </returns> */

private static readonly string binaryStrFormat = "$BINARY(\"{0}\", {1})";

/** <summary> Creates a new Instance of the <c>RtBinaryString</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtBinaryString(string sourceValue)
{

if(sourceValue == null)
Identifier = RtTypeIdentifier.Null;

else
{
Identifier = RtTypeIdentifier.BinaryString;
Value = sourceValue;

Length = Value.Length;
LengthAfterEncoding = Length * 2;
}

}

/** <summary> Checks if the providen String is in the expected Pattern (BINARY). </summary>

<param name = "targetStr"> The String to be Analized. </param>

<returns> The result from the expression Match. </returns> */

public static Match PerformRegex(string targetStr) => Regex.Match(targetStr, @"\$BINARY\(""(.*?)"", (\d+)\)");

/** <summary> Reads a Binary String from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "isPropertyName"> Determines if the BinaryString should be Written as a PropertyName or not. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, bool isPropertyName)
{
byte binaryRefStringLength = inputStream.ReadByte();
string binaryRefString = inputStream.ReadStringByVarIntLength(binaryStrEncoding);

int binaryRefStringLengthAfterEncoding = binaryRefString.Length;

if(binaryRefStringLengthAfterEncoding < binaryRefStringLength)
{
string binaryStrMismatch = GetLengthMismatchMsg(RtTypeIdentifier.BinaryString, binaryRefString, binaryRefStringLength, binaryRefStringLengthAfterEncoding, inputStream.Position);
Text.PrintWarning(binaryStrMismatch);
}

int binaryOffset = inputStream.ReadVarInt();
string binaryString = string.Format(binaryStrFormat, binaryRefString, binaryOffset);

WriteJsonString(outputStream, binaryString, isPropertyName);
}

/** <summary> Reads a Binary String from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "sourceRegex"> The Regex of the Binary String. </param> */

public static void Write(BinaryStream outputStream, Match sourceRegex)
{
outputStream.WriteByte( (byte)RtTypeIdentifier.BinaryString);
string binaryRefString = sourceRegex.Groups[1].ToString();

byte binaryRefStringLength = (byte)(binaryRefString.Length / 4);
outputStream.WriteByte(binaryRefStringLength);

outputStream.WriteStringByVarIntLength(binaryRefString, binaryStrEncoding);
int binaryOffset = int.Parse(sourceRegex.Groups[2].ToString() );

outputStream.WriteVarInt(binaryOffset);
}

}

}